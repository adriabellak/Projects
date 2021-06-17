using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class TruePlaceholderTile : TrueGateTile
{
    public TileBase replacedTile;

    public override void RefreshTile(Vector3Int location, ITilemap tilemap)
    {
        if(tilemap.GetTile(location))
        {
            PlaceholderGate gate = new PlaceholderGate(location, tilemap);
            Circuit.AddComponent(location, gate);
            gate.previousTile = replacedTile;
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
    [MenuItem("Assets/Create/TruePlaceholderTile")]
    public static void CreateAndTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save TruePlaceholderTile", "New TruePlaceholderTile", "Asset", "Save TruePlaceholderTile", "Assets");
        if (path == "")
            return;
    AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<TruePlaceholderTile>(), path);
    }
    #endif
}