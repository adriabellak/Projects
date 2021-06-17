using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PlaceholderBufferTile : Tile
{
    public override void RefreshTile(Vector3Int location, ITilemap tilemap)
        {
            if(tilemap.GetTile(location))
            {
                Circuit.AddComponent(location, new PlaceholderBuffer(location, tilemap));
            }
            else
            {
                Circuit.RemoveComponent(location);
            }
            tilemap.RefreshTile(location);

            if(Circuit.circuitComponents.ContainsKey(location))
            {
                CircuitComponent component = Circuit.circuitComponents[location];
                foreach(CircuitComponent inComponent in component.ins)
                {
                    tilemap.RefreshTile(inComponent.location);
                }
                foreach(CircuitComponent outComponent in component.outs)
                {
                    tilemap.RefreshTile(outComponent.location);
                }
            }
        }

    #if UNITY_EDITOR
    [MenuItem("Assets/Create/PlaceholderBufferTile")]
    public static void CreatePlaceholderBufferTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save PlaceholderBufferTile", "New PlaceholderBufferTile", "Asset", "Save PlaceholderBufferTile", "Assets");
        if (path == "")
            return;
    AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<PlaceholderBufferTile>(), path);
    }
    #endif
}