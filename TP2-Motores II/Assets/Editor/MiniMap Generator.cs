using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class MiniMapGenerator : EditorWindow {

    //CANVAS
    private Canvas canvasMain;
    private Canvas _customCanvas;
    private GameObject _canvas;
    private string _canvasName;

    //CAM
    private Camera _cam;
    private int _highCam;
    private float fow;
    private string _nameCam;
    private GameObject _targetToFollow;
    private List<Camera> _camerasInScene = new List<Camera>();

    //SELECT CAM
    private Camera _SelectCam;
    private int _SelecthighCam;
    private float _Selectfow;
    private string _SelectnameCam;
    private GameObject _SelecttargetToFollow;



    //TEXTURE
    private RenderTexture _RTexture;
    private int _highRenderTexture;
    private int _widthRenderTexture;
    private bool _changeSideRTexture = false;
    private List<RenderTexture> _texturesInScene = new List<RenderTexture>();

    //MAP
    private RawImage _miniMap;
    private int _highMap;
    private int _widthMap;
    private float _positionXmap;
    private float _positionYmap;
    private string _nameMap;
    private List<RawImage> _mapsInScene = new List<RawImage>();


    //ORGANIZADOR SECCIONES
    private bool _showStep1;
    private bool _showStep2;
    private bool _showCustomCanvas = false;
    private bool _searchCanvas = true;
    private bool _createCanvas = false;

    private Texture2D trest;
    private TextureImporter trestas;
   
   
   



    [MenuItem("HUD/MiniMap Generator")]
    public static void OpenWindow()
    {
        GetWindow<MiniMapGenerator>();
    }
   

    private void OnEnable()
    {
       if(canvasMain == null && !_showCustomCanvas)
        {
            canvasMain = FindObjectOfType<Canvas>();
        }
    }

    private void OnGUI()
    {
        //GUI.DrawTexture(GUILayoutUtility.GetRect(335, 241), (Texture2D)Resources.Load("advice"));
        //TextureImporterType.Sprite;
        // trest.tex
       
        trestas = (TextureImporter)EditorGUILayout.ObjectField("imagen", trestas, typeof(TextureImporter), true);
        if (trestas != null)
        trestas.textureType = TextureImporterType.Sprite;







        Intro();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        SetCanvas();

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        Step1();
        Step2();


        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();


        SetOfCamerasCreated();

    }


    //SECCIONES
   


    void Intro()
    {
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        GUI.DrawTexture(GUILayoutUtility.GetRect(75, 200), (Texture2D)Resources.Load("minimap"));
        EditorGUILayout.LabelField("Welcome to the MINI MAP GENERATOR", EditorStyles.centeredGreyMiniLabel);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.LabelField("here you will create a basic minimap with a few simple steps");
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
    }
    void Step1()
    {

        EditorGUILayout.LabelField("1° step: Create a Camera", EditorStyles.boldLabel);
        _showStep1 = EditorGUILayout.Foldout(_showStep1, "Camera Creator");
        if (_showStep1)
        {
            //NOMBRE
            EditorGUILayout.BeginHorizontal();
            _nameCam = EditorGUILayout.TextField("name Camera ", _nameCam, GUILayout.Width(400));
            EditorGUILayout.EndHorizontal();

            //ALTURA
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("✦ set a high for the minimap's Camera: ", GUILayout.Width(300f));
            _highCam = EditorGUILayout.IntField(_highCam, GUILayout.Width(50f));
            EditorGUILayout.EndHorizontal();
            if (_highCam <= 0)
                EditorGUILayout.HelpBox("the high is 0", MessageType.Warning);

            //VISTA
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("✦ set camera's range view: ", GUILayout.Width(300f));
            fow = EditorGUILayout.FloatField(fow, GUILayout.Width(50f));
            EditorGUILayout.EndHorizontal();

            //TARGET
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("✦ set a target to follow ", GUILayout.Width(300f));
            _targetToFollow = (GameObject)EditorGUILayout.ObjectField(_targetToFollow, typeof(GameObject), true);
            EditorGUILayout.EndHorizontal();




            //SET

            
            

            if (fow <= 0)
            {
                fow = 0;
                EditorGUILayout.HelpBox("the range of view can be 0 or less", MessageType.Error);
            }


            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("✦ push the button for add a Camera-follow to target", GUILayout.Width(300f));
            if (GUILayout.Button("Add Camera",GUILayout.Width(250)))
            {
                SetOfCam(_nameCam);
                if (_targetToFollow != null)
                {
                    _cam.transform.SetParent(_targetToFollow.transform);
                    _targetToFollow.layer = LayerMask.NameToLayer("Minimap");
                }
                    
                    

            }
            EditorGUILayout.EndHorizontal();
        }

        if (_cam != null)
        {
            if (fow > 0)
            {
                _cam.GetComponent<Camera>().fieldOfView = fow;
            }
            else
            {
                fow = 1f;
            }

            _cam.transform.position = new Vector3(_targetToFollow.transform.position.x, _highCam, _targetToFollow.transform.position.z);
        }
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
    }
    void Step2()
    {
        EditorGUILayout.LabelField("2° step: MiniMap (image)", EditorStyles.boldLabel);
        _showStep2 = EditorGUILayout.Foldout(_showStep2, "Camera Creator");
        if (_showStep2)
        {
            EditorGUILayout.BeginHorizontal();
            _nameMap = EditorGUILayout.TextField("✦ name Minimap ", _nameMap, GUILayout.Width(400));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("✦ Size of map ", GUILayout.Width(100));
            EditorGUILayout.LabelField(" high: ", EditorStyles.miniBoldLabel, GUILayout.Width(30));
            _highMap = EditorGUILayout.IntField(_highMap, GUILayout.Width(50));

            EditorGUILayout.LabelField(" width: ", EditorStyles.miniBoldLabel, GUILayout.Width(35));
            _widthMap = EditorGUILayout.IntField(_widthMap, GUILayout.Width(50));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("✦ position of map ", GUILayout.Width(100));
            EditorGUILayout.LabelField(" position X: ", EditorStyles.miniBoldLabel, GUILayout.Width(60));
            _positionXmap = EditorGUILayout.FloatField(_positionXmap, GUILayout.Width(50));

            EditorGUILayout.LabelField(" position Y: ", EditorStyles.miniBoldLabel, GUILayout.Width(60));
            _positionYmap = EditorGUILayout.FloatField(_positionYmap, GUILayout.Width(50));
            EditorGUILayout.EndHorizontal();


            if (_highMap <= 0 || _widthMap <= 0)
            {
                EditorGUILayout.HelpBox("sizes of the map render can't be 0", MessageType.Error);
            }

            _changeSideRTexture = EditorGUILayout.Toggle("custom side texture Render", _changeSideRTexture);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("✦ push the button for add a MiniMap to Canvas", GUILayout.Width(300f));
            if (GUILayout.Button("Add MiniMap", GUILayout.Width(250)))
            {

                SetOfMiniMap(_nameMap);
                SetRenderTexture(_nameMap);
               
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            RenderTextureGUI();
        }
    }

    //RECONFIGURACION
    void SetOfCamerasCreated()
    {
        EditorGUILayout.LabelField("Cameras created", EditorStyles.boldLabel);
        _SelectCam = (Camera)EditorGUILayout.ObjectField("Current Camera", _SelectCam, typeof(Camera), true);
        _miniMap = (RawImage)EditorGUILayout.ObjectField("Its MiniMap", _miniMap, typeof(RawImage), true);
        
  
        EditorGUILayout.Space();

        _SelecthighCam = EditorGUILayout.IntField("altura Camara", _SelecthighCam );
        _SelectnameCam = EditorGUILayout.TextField("nombre Camara ", _SelectnameCam);
        _SelecttargetToFollow = (GameObject)EditorGUILayout.ObjectField("Target de la Camara", _SelecttargetToFollow, typeof(GameObject), true);
        _Selectfow = EditorGUILayout.FloatField("Rango de Vision", _Selectfow);

        if (_SelectCam != null)
        {
            _SelectnameCam = _SelectCam.name;
            _SelecthighCam = (int)_SelectCam.transform.position.y;
            _Selectfow = _SelectCam.fieldOfView;
            //_SelecttargetToFollow = _SelectCam.GetComponentInParent<GameObject>();
        }
        


        EditorGUILayout.LabelField("Cameras in scene: ", EditorStyles.miniBoldLabel);
        for (int i = 0; i < _camerasInScene.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.ObjectField(_camerasInScene[i].name, _camerasInScene[i], typeof(Camera), true);
            EditorGUILayout.ObjectField(_mapsInScene[i].name, _mapsInScene[i], typeof(RawImage), true);

            if (GUILayout.Button("select"))
            {
                _SelectCam = _camerasInScene[i];
      
                _miniMap = _mapsInScene[i];
            }
               

            if (GUILayout.Button("delete"))
            {
                DestroyImmediate(_camerasInScene[i].gameObject);
                DestroyImmediate(_mapsInScene[i].gameObject);
                AssetDatabase.DeleteAsset("Assets/Images/" + _texturesInScene[i].name + ".renderTexture");
                _camerasInScene.RemoveAt(i);
                _texturesInScene.RemoveAt(i);
                if (_mapsInScene[i] != null)
                _mapsInScene.RemoveAt(i);
            }
                
                

            EditorGUILayout.EndHorizontal();
        }


        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
    }

//CONFIGURACION RENDER TEXTURE
    void SetRenderTexture(string nameRender)
    {
        RenderTexture renderTexture = new RenderTexture(255, 255, 1);
        AssetDatabase.CreateAsset(renderTexture, "Assets/Images/" + nameRender + ".renderTexture");
        _RTexture = renderTexture;
        _texturesInScene.Add(_RTexture);
        for (int i = 0; i < _texturesInScene.Count; i++)
        {
            _mapsInScene[i].GetComponent<RawImage>().texture = _texturesInScene[i];
            _camerasInScene[i].GetComponent<Camera>().targetTexture = _texturesInScene[i];

            //_miniMap.GetComponent<RawImage>().texture = renderTexture;
            //cam.GetComponent<Camera>().targetTexture = renderTexture;
        }
        

        if (_highRenderTexture > 0 || _widthRenderTexture > 0)
        {
            if (_changeSideRTexture){
                RenderTexture renderTexturecustom = new RenderTexture(_highRenderTexture, _widthRenderTexture, 1);
                AssetDatabase.CreateAsset(renderTexturecustom, "Assets/Images/" + nameRender + ".renderTexture");
                _RTexture = renderTexturecustom;
                _texturesInScene.Add(_RTexture);
                _miniMap.GetComponent<RawImage>().texture = renderTexturecustom;
                _cam.GetComponent<Camera>().targetTexture = renderTexturecustom;
            }
           
        }
    }
    void RenderTextureGUI()
    {

        _RTexture = (RenderTexture)EditorGUILayout.ObjectField("view(minimap in scene)", _RTexture, typeof(RenderTexture), true, GUILayout.Width(250));
        
        if (_changeSideRTexture)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(" size texture ", GUILayout.Width(100));
            _highRenderTexture = EditorGUILayout.IntField(_highRenderTexture, GUILayout.Width(50));
            EditorGUILayout.LabelField(" x ", GUILayout.Width(25));
            _widthRenderTexture = EditorGUILayout.IntField(_widthRenderTexture, GUILayout.Width(50));
            EditorGUILayout.EndHorizontal();
            if (_highRenderTexture <= 0 || _widthRenderTexture <= 0)
            {
                EditorGUILayout.HelpBox("sizes of the texture render can't be 0", MessageType.Error);
            }
        }
    }

 //CONFIGURACION DEL CANVAS
    void SetCanvas()
    {
        canvasMain = (Canvas)EditorGUILayout.ObjectField("Canvas actual", canvasMain, typeof(Canvas), false);

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        _searchCanvas = EditorGUILayout.Toggle("Buscar Canvas", _searchCanvas);
        _createCanvas = EditorGUILayout.Toggle("Crear Canvas", _createCanvas);
        _showCustomCanvas = EditorGUILayout.Toggle("Arrastrar Canvas", _showCustomCanvas);
        
    
        EditorGUILayout.EndHorizontal();

        //DARLE UN FIELD AL USUARIO PARA QUE TIRE EL CANVAS QUE QUIERA
        if (_showCustomCanvas)
        {
            _searchCanvas = false;
            _createCanvas = false;
            _customCanvas = (Canvas)EditorGUILayout.ObjectField("Canvas principal", _customCanvas, typeof(Canvas), true);
            canvasMain = _customCanvas;
        }

        //CREAR UN CANVAS

        else if(_createCanvas)
        {

            _searchCanvas = false;
            _showCustomCanvas = false;
            _canvasName = EditorGUILayout.TextField("Nombre Canvas", _canvasName) ;
            if (GUILayout.Button("Agregar", GUILayout.Width(250)) && FindObjectOfType<Canvas>() == null)
            {
                _canvas = new GameObject();
                _canvas.AddComponent<RectTransform>();
                _canvas.AddComponent<Canvas>();
                _canvas.AddComponent<CanvasScaler>();
                _canvas.AddComponent<GraphicRaycaster>();
                _canvas.transform.position = new Vector3(227.5f, 128f, _canvas.transform.position.z);
                _canvas.GetComponent<RectTransform>().sizeDelta = new Vector2(455, 256);
                _canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
                canvasMain = _canvas.GetComponent<Canvas>();
                _canvas.name = _canvasName;
            }
            else if (FindObjectOfType<Canvas>() != null)
            {
                
                EditorGUILayout.HelpBox("Ya hay un Canvas creado con el nombre:  " + FindObjectOfType<Canvas>().name, MessageType.Info);
            }
        }


        //BUSCAR CANVAS EN LA ESCENA
        else if (_searchCanvas)
        {
        
            _showCustomCanvas = false;
            _createCanvas = false;
            if (canvasMain == null)
            {
                  
                canvasMain = FindObjectOfType<Canvas>();
                if (FindObjectOfType<Canvas>() == null)
                {
                    EditorGUILayout.HelpBox("No se encontró ningun Canvas Creado en la jerarquia", MessageType.Error);
                }


            }
            else  
            {
                EditorGUILayout.HelpBox("Hay un Canvas en CANVAS ACTUAL" , MessageType.Error);
            }


        }
       
    }
 

    //CONFIGURACION DE LA CAM
    void SetOfCam(string name)
    {
        GameObject cam;
        cam = new GameObject();
        cam.AddComponent<Camera>();
        
        // cam.transform.SetParent(canvasMain.transform);
        _cam = cam.GetComponent<Camera>();
        cam.transform.Rotate(90, 0, 0);
        cam.transform.position = new Vector3(0, 1500f, 0);
        _camerasInScene.Add(cam.GetComponent<Camera>());
        cam.GetComponent<Camera>().cullingMask = 1 << LayerMask.NameToLayer("Minimap");
       // Debug.Log(LayerMask.NameToLayer("Minimap"));
        
        cam.name = name;


        GameObject _imageCont;
        _imageCont = new GameObject();
        _imageCont.AddComponent<RectTransform>();
        _imageCont.AddComponent<CanvasRenderer>();
        _imageCont.AddComponent<RawImage>();
        _imageCont.transform.SetParent(canvasMain.transform);
        _miniMap = _imageCont.GetComponent<RawImage>();
        _mapsInScene.Add(_miniMap);


    }

//CONFIGURACION DEL MINI MAP
   void SetOfMiniMap(string name)
    {


              GameObject _imageCont;
            _imageCont = new GameObject();
            _imageCont.AddComponent<RectTransform>();
            _imageCont.AddComponent<CanvasRenderer>();
            _imageCont.AddComponent<RawImage>();
            _imageCont.transform.SetParent(canvasMain.transform);
        //_imageCont.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _widthMap);
          // _imageCont.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _widthMap);
           _imageCont.GetComponent<RectTransform>().sizeDelta = new Vector2(_widthMap, _highMap);
            _imageCont.transform.position = new Vector3(_positionXmap, _positionYmap, _imageCont.transform.position.z);
        _imageCont.name = name;
        _miniMap = _imageCont.GetComponent<RawImage>();
        DestroyImmediate(_mapsInScene[_mapsInScene.Count - 1].gameObject);
        // _mapsInScene.RemoveAt(0);
        // _mapsInScene.Insert(0, _miniMap);
        _mapsInScene[_mapsInScene.Count - 1] = _imageCont.GetComponent<RawImage>();
        for (int i = 0; i < _mapsInScene.Count; i++)
        {
            Debug.Log(_mapsInScene[i].name +""+""+ i);
        }
        


    }
}
