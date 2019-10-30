using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public static class MiniMapScriptableObject{
  

    public static void CreatedAssetConfigMiniMap<T>(string path, string name) where T : ScriptableObject
    {
        T asset = ScriptableObject.CreateInstance<T>();
        string pathOfAsset = AssetDatabase.GenerateUniqueAssetPath("Assets/"+path +"/"+ name+ ".asset");
        AssetDatabase.CreateAsset(asset, pathOfAsset);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }

}
