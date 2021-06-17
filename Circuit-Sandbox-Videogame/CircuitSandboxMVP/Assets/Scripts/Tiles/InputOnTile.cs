using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class InputOnTile : InputTile
{
    public InputOnTile() : base(true){}

    #if UNITY_EDITOR
    [MenuItem("Assets/Create/InputOnTile")]
    public static void CreateInputOnTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save InputOnTile", "New InputOnTile", "Asset", "Save InputOnTile", "Assets");
        if (path == "")
            return;
    AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<InputOnTile>(), path);
    }
    #endif
}
