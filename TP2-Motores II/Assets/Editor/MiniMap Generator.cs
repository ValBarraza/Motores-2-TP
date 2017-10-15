using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class MiniMapGenerator : EditorWindow {

    
    private Camera _cam;
    private RenderTexture _map;
    private Sprite _imageTexture;
    private float _high;


    private GameObject cam;
    private GameObject _canvas;
    private GameObject texture;



    [MenuItem("HUD/miniMap")]
    static void OpenWindow()
    {
        GetWindow<MiniMapGenerator>();
    }

    private void OnGUI()
    {
        _cam = (Camera)EditorGUILayout.ObjectField("view(camera)", _cam, typeof(Camera), true);
        _map = (RenderTexture)EditorGUILayout.ObjectField("view(minimap in scene)", _map, typeof(RenderTexture), true);

        if (GUILayout.Button("add canvas"))
        {
            _canvas = new GameObject();
            _canvas.AddComponent<RectTransform>();
            _canvas.AddComponent<Canvas>();
            _canvas.AddComponent<CanvasScaler>();
            _canvas.AddComponent<GraphicRaycaster>();
            _canvas.transform.position = new Vector3(227.5f,128f, _canvas.transform.position.z);
            _canvas.GetComponent<RectTransform>().sizeDelta = new Vector2(455,256);
            _canvas.name = "Main Canvas";

        }


        if (GUILayout.Button("add camera and MiniMap"))
        {
            cam = new GameObject();
            texture = new GameObject();
            texture.AddComponent<RectTransform>();
            texture.AddComponent<CanvasRenderer>();
            texture.AddComponent<RawImage>();
            cam.AddComponent<Camera>();
            cam.transform.SetParent(_canvas.transform);
            texture.transform.SetParent(_canvas.transform);
            cam.name = "Cam";
            texture.name = "miniMap";
        }
        //


        if (GUILayout.Button("add Render Texture"))
        {
            RenderTexture renderTexture = new RenderTexture(100,100,1);
          
            AssetDatabase.CreateAsset(renderTexture, "Assets/MyTextureNew.renderTexture");
             //= _map;

            texture.GetComponent<RawImage>().texture = renderTexture;
            cam.GetComponent<Camera>().targetTexture = renderTexture;
        }
        //cam.GetComponent<Camera>().targetTexture = _map;


    }
}
