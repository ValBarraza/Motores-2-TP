using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MiniMapGenerator : EditorWindow {


    private Camera _cam;
    private RenderTexture _map;
    private float _high;

    [MenuItem("HUD/miniMap")]
    static void OpenWindow()
    {
        GetWindow<MiniMapGenerator>();
    }

    private void OnGUI()
    {
        EditorGUILayout.ObjectField("view(camera)", _cam, typeof(Camera), true);
        EditorGUILayout.ObjectField("view(minimap in scene)", _cam, typeof(RenderTexture), true);
    }
}
