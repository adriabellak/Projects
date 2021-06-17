using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class InputOffTile : InputTile
{
    public InputOffTile() : base(false){}

    #if UNITY_EDITOR
    [MenuItem("Assets/Create/InputOffTile")]
    public static void CreateInputOffTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save InputOffTile", "New InputOffTile", "Asset", "Save InputOffTile", "Assets");
        if (path == "")
            return;
    AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<InputOffTile>(), path);
    }
    #endif
}