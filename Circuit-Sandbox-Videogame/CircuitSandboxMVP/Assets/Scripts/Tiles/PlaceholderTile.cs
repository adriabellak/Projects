using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PlaceholderTile : Tile
{
    public AllSprites sprites;
    public override void RefreshTile(Vector3Int location, ITilemap tilemap)
    {
        Tilemap realTilemap = tilemap.GetComponent<Tilemap>();

        TileBase previousTile;
        Circuit.RemoveComponent(location);
        tilemap.RefreshTile(location);
        if(tilemap.GetTile<PlaceholderTile>(location))
        {
            TruePlaceholderTile placeholderGate = ScriptableObject.CreateInstance<TruePlaceholderTile>();
            placeholderGate.sprites = sprites;

            previousTile = tilemap.GetTile(location + new Vector3Int(-1, 0, 0));
            placeholderGate.replacedTile = previousTile;
            realTilemap.SetTile(location + new Vector3Int(-1, 0, 0), placeholderGate);
        }
        else
        {
            CircuitComponent component = Circuit.circuitComponents[location + new Vector3Int(-1, 0, 0)];
            if(component is PlaceholderGate)
            {
                LogicGate gate = (LogicGate)component;
                realTilemap.SetTile(location + new Vector3Int(-1, 0, 0), gate.previousTile);
            }
        }
    }

    #if UNITY_EDITOR
    [MenuItem("Assets/Create/PlaceholderTile")]
    public static void CreateAndTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save PlaceholderTile", "New PlaceholderTile", "Asset", "Save PlaceholderTile", "Assets");
        if (path == "")
            return;
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<PlaceholderTile>(), path);
    }
    #endif
}