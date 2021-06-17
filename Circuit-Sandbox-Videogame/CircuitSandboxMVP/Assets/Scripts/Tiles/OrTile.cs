using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class OrTile : Tile
{
    public AllSprites sprites;
    public override void RefreshTile(Vector3Int location, ITilemap tilemap)
    {
        Tilemap realTilemap = tilemap.GetComponent<Tilemap>();

        TileBase previousTile;
        Circuit.RemoveComponent(location);
        tilemap.RefreshTile(location);
        if(tilemap.GetTile<OrTile>(location))
        {
            TrueOrTile orGate = ScriptableObject.CreateInstance<TrueOrTile>();
            orGate.sprites = sprites;

            previousTile = tilemap.GetTile(location + new Vector3Int(-1, 0, 0));
            orGate.replacedTile = previousTile;
            realTilemap.SetTile(location + new Vector3Int(-1, 0, 0), orGate);
        }
        else
        {
            CircuitComponent component = Circuit.circuitComponents[location + new Vector3Int(-1, 0, 0)];
            if(component is OrGate)
            {
                LogicGate gate = (LogicGate)component;
                realTilemap.SetTile(location + new Vector3Int(-1, 0, 0), gate.previousTile);
            }
        }
    }

    #if UNITY_EDITOR
    [MenuItem("Assets/Create/OrTile")]
    public static void CreateAndTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save OrTile", "New OrTile", "Asset", "Save OrTile", "Assets");
        if (path == "")
            return;
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<OrTile>(), path);
    }
    #endif
}