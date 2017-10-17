using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class MiniMapGenerator : EditorWindow {

    //CANVAS
    private Canvas canvasMain;
    private GameObject _canvas;

    //CAM
    private Camera _cam;
    private GameObject cam;
    private int _highCam;
    private float fow;

    



    //TEXTURE
    private RenderTexture _RTexture;
    private int _highRenderTexture;
    private int _widthRenderTexture;

    //MAP
    private GameObject _imageCont;
    private int _highMap;
    private int _widthMap;



   
   



    [MenuItem("HUD/miniMap")]
    static void OpenWindow()
    {
        GetWindow<MiniMapGenerator>();
    }

    private void OnEnable()
    {
        if (canvasMain == null)
        {
            canvasMain = FindObjectOfType<Canvas>();

        }
    }

    private void OnGUI()
    {
        _cam = (Camera)EditorGUILayout.ObjectField("view(camera)", _cam, typeof(Camera), true);
        _RTexture = (RenderTexture)EditorGUILayout.ObjectField("view(minimap in scene)", _RTexture, typeof(RenderTexture), true);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(" size texture ");
        _highRenderTexture = EditorGUILayout.IntField(_highRenderTexture);
        EditorGUILayout.LabelField(" x ");
        _widthRenderTexture = EditorGUILayout.IntField(_widthRenderTexture);
        EditorGUILayout.EndHorizontal();
        if (_highRenderTexture <= 0 || _widthRenderTexture <= 0)
        {
            EditorGUILayout.HelpBox("sizes of the texture render can't be 0", MessageType.Error);
        }


        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(" size map ");
        _highMap = EditorGUILayout.IntField(_highMap);
        EditorGUILayout.LabelField(" x ");
        _widthMap = EditorGUILayout.IntField(_widthMap);
        EditorGUILayout.EndHorizontal();
        if (_highMap <= 0 || _widthMap <= 0)
        {
            EditorGUILayout.HelpBox("sizes of the map render can't be 0", MessageType.Error);
        }


        EditorGUILayout.LabelField(" high camera ");
        _highCam = EditorGUILayout.IntField(_highCam);
        // _cam.GetComponent<Camera>().fieldOfView = fow;
        _cam.transform.position = new Vector3(0, _highCam, 0);
        if (GUILayout.Button("add camera and MiniMap"))
        {
            SetOfCam();
            SetOfMiniMap();
        }

        SetCanvas();



        if (GUILayout.Button("add Render Texture"))
        {
            SetRenderTexture();
        }

      

    }


//CONFIGURACION RENDER TEXTURE
    void SetRenderTexture()
    {
        if (_highRenderTexture > 0 || _widthRenderTexture > 0)
        {
            RenderTexture renderTexture = new RenderTexture(_highRenderTexture, _widthRenderTexture, 1);
            AssetDatabase.CreateAsset(renderTexture, "Assets/MyTextureNew.renderTexture");
            _imageCont.GetComponent<RawImage>().texture = renderTexture;
            cam.GetComponent<Camera>().targetTexture = renderTexture;
            _RTexture = renderTexture;
        }
    }

 //CONFIGURACION DEL CANVAS
    void SetCanvas()
    {
        if (GUILayout.Button("add canvas") && FindObjectOfType<Canvas>() == null)
        {
            _canvas = new GameObject();
            _canvas.AddComponent<RectTransform>();
            _canvas.AddComponent<Canvas>();
            _canvas.AddComponent<CanvasScaler>();
            _canvas.AddComponent<GraphicRaycaster>();
            _canvas.transform.position = new Vector3(227.5f, 128f, _canvas.transform.position.z);
            _canvas.GetComponent<RectTransform>().sizeDelta = new Vector2(455, 256);
            canvasMain = _canvas.AddComponent<Canvas>();
            _canvas.name = "Main Canvas";
        }
        else if (FindObjectOfType<Canvas>() != null)
        {

            EditorGUILayout.HelpBox("there's a canvas created", MessageType.Info);
        }
    }

 //CONFIGURACION DE LA CAM
   void SetOfCam()
    {
        cam = new GameObject();
        cam.AddComponent<Camera>();
        // cam.transform.SetParent(canvasMain.transform);
        _cam = cam.GetComponent<Camera>();
        cam.transform.Rotate(90, 0, 0);
        cam.transform.position = new Vector3(0, 1500f, 0);
        cam.name = "Cam";

    }

//CONFIGURACION DEL MINI MAP
   void SetOfMiniMap()
    {
       
        
            
            _imageCont = new GameObject();
            _imageCont.AddComponent<RectTransform>();
            _imageCont.AddComponent<CanvasRenderer>();
            _imageCont.AddComponent<RawImage>();
            _imageCont.transform.SetParent(canvasMain.transform);
            _imageCont.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _widthMap);
            _imageCont.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _highMap);
            _imageCont.name = "miniMap";
        
    }
}
