using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class TrueGateTile : Tile
{
    public AllSprites sprites;
    public override void GetTileData(Vector3Int location, ITilemap tilemap, ref TileData tileData)
    {
        HashSet<Vector3Int> borders = new HashSet<Vector3Int>();
        if (!Circuit.circuitComponents.TryGetValue(location, out CircuitComponent component)) {
            return;
        }
        foreach (CircuitComponent neighbor in Circuit.circuitComponents[location].ins)
        {
            borders.Add(neighbor.location);
        }

        int mask = 0;

        if (borders.Contains(new Vector3Int(-1, 0, 0) + location)) {
            mask += 1;
            // si el de la izquierda esta prendido
            mask += Circuit.circuitComponents[new Vector3Int(-1, 0, 0) + location].on ? 8 : 0;
        }
        else if (borders.Contains(new Vector3Int(-2, 0, 0) + location)) {
            mask += 1;
            // si el de 2 a la izquierda esta prendido
            mask += Circuit.circuitComponents[new Vector3Int(-2, 0, 0) + location].on ? 8 : 0;
        }

        if (borders.Contains(new Vector3Int(0, 1, 0) + location)) {
            mask += 2;
            // si el de arriba esta prendido
            mask += Circuit.circuitComponents[new Vector3Int(0, 1, 0) + location].on ? 16 : 0;
        }

        if (borders.Contains(new Vector3Int(0, -1, 0) + location)) {
            mask += 4;
            // si el de abajo esta prendido
            mask += Circuit.circuitComponents[new Vector3Int(0, -1, 0) + location].on ? 32 : 0;
        }

        int index = GetIndex((byte)mask);
        if (index >= 0 && index < sprites.twoWiresSprites.Length)
        {
            tileData.sprite = sprites.twoWiresSprites[index];
        }
        else
        {
            Debug.LogWarning("Not enough sprites in TrueGateTile instance, index " + index);
        }
    }
    private int GetIndex(byte mask)
    {
        switch(mask)
        {
            case 0: return 0;
            case 1: return 1;
            case 2: return 0;
            case 3: return 1;
            case 4: return 0;
            case 5: return 2;
            case 6: return 0;
            case 7: return 0;
            case 9: return 7;
            case 11: return 7;
            case 13: return 10;
            case 18: return 3;
            case 19: return 6;
            case 22: return 3;
            case 27: return 8;
            case 36: return 4;
            case 37: return 9;
            case 38: return 4;
            case 45: return 11;
            case 54: return 5;
            default: return -1;
        }
    }
}