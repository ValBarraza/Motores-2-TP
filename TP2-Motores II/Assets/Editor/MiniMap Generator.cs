using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.IO;
public class MiniMapGenerator : EditorWindow {


    Vector2 scrollBarPosition;
    GameObject[] TagsOfMiniMap = new GameObject[] { };
    public  Dictionary<RawImage,Camera> MapsAndCam = new Dictionary<RawImage,Camera>();
    

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
    public List<Camera> _camerasInScene = new List<Camera>();

    //SELECT CAM
    private Camera _SelectCam;
    private int _SelecthighCam;
    private float _Selectfow;
    private string _SelectnameCam;
    private GameObject _SelecttargetToFollow;
    private bool _assignValuesToCam;



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
    public List<RawImage> _mapsInScene = new List<RawImage>();

    //SELECT MINI MAP
    private RawImage _SelectminiMap;
    private string _selectNametMiniMap;
    private int _selectHighMap;
    private int _selectWidthMap;
    private float _selectPositionXmap;
    private float _selectPositionYmap;
    private string _selectNameMap;


    //ORGANIZADOR SECCIONES
    private bool _showStep1;
    private bool _showStep2;
    private bool _showCustomCanvas = false;
    private bool _searchCanvas = true;
    private bool _createCanvas = false;


    private Texture2D trest;
    private TextureImporter trestas;
   
   //SCRIPTABLE OBJECT
   private string namePath;
   private string nameMiniMapConfig;
   private MiniMapConig config;
    private bool activeConfigAdvance;
    private bool loadConfig;
    private bool saveConig;




    [MenuItem("HUD/MiniMap Generator")]
    public static void OpenWindow()
    {
        GetWindow<MiniMapGenerator>();
    }


 

    private void OnEnable()
    {
   
        TagsOfMiniMap = GameObject.FindGameObjectsWithTag("MiniMap");


        if (_camerasInScene.Count == 0)
        {
            for (int i = 0; i < TagsOfMiniMap.Length; i++)
            {
                if (TagsOfMiniMap[i] != null)
                {
                    if (TagsOfMiniMap[i].GetComponent<Camera>() != null)
                    {
                        _camerasInScene.Add(TagsOfMiniMap[i].GetComponent<Camera>());
                        RenderTexture aux = TagsOfMiniMap[i].GetComponent<Camera>().targetTexture;
                        _texturesInScene.Add(aux);
                        
                    }
                        

                    if (TagsOfMiniMap[i].GetComponent<RawImage>() != null)
                    {
                        _mapsInScene.Add(TagsOfMiniMap[i].GetComponent<RawImage>());
                    

                    }

                   



                }
            
 

            }
        }

        if (_camerasInScene.Count > 0)
        {
            for (int i = 0; i < _camerasInScene.Count; i++)
            {
                MapsAndCam.Add(_mapsInScene[i], _camerasInScene[i]);
                //Debug.Log(MapsAndCam[_mapsInScene[i]].name);
            }

        }








        if (canvasMain == null && !_showCustomCanvas)
        {
            canvasMain = FindObjectOfType<Canvas>();
        }


    }

    

    private void OnGUI()
    {
        //GUI.DrawTexture(GUILayoutUtility.GetRect(335, 241), (Texture2D)Resources.Load("advice"));
        //TextureImporterType.Sprite;
        // trest.tex




        scrollBarPosition = EditorGUILayout.BeginScrollView(scrollBarPosition);

     


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
        EditorGUILayout.EndScrollView();

    }


    //SECCIONES
   

    void Intro()
    {
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        GUI.DrawTexture(GUILayoutUtility.GetRect(75, 200), (Texture2D)Resources.Load("minimap"));
        EditorGUILayout.LabelField("Bienvenido al generador de MiniMapas", EditorStyles.centeredGreyMiniLabel);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.LabelField("Aqui podras generar un simple MiniMapa en pocos pasos");
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
    }
    void Step1()
    {

        EditorGUILayout.LabelField("1° PASO: Crea una camara", EditorStyles.boldLabel);
        _showStep1 = EditorGUILayout.Foldout(_showStep1, "CREADOR DE CAMARA");
        if (_showStep1)
        {
            //NOMBRE
            EditorGUILayout.BeginHorizontal();
            _nameCam = EditorGUILayout.TextField("Nombre de la Camara", _nameCam, GUILayout.Width(400));
            EditorGUILayout.EndHorizontal();

            //ALTURA
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("✦ Asigna una altura a la Camara: ", GUILayout.Width(300f));
            _highCam = EditorGUILayout.IntField(_highCam, GUILayout.Width(50f));
            EditorGUILayout.EndHorizontal();
            if (_highCam <= 0)
                EditorGUILayout.HelpBox("CUIDADO! La altura es CERO", MessageType.Warning);

            //VISTA
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("✦ Asigna un rango de vision: ", GUILayout.Width(300f));
            fow = EditorGUILayout.FloatField(fow, GUILayout.Width(50f));
            EditorGUILayout.EndHorizontal();

            //TARGET
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("✦ Asigna un target ", GUILayout.Width(300f));
            _targetToFollow = (GameObject)EditorGUILayout.ObjectField(_targetToFollow, typeof(GameObject), true);
            EditorGUILayout.EndHorizontal();




            //SET

            
            

            if (fow <= 0)
            {
                fow = 0;
                EditorGUILayout.HelpBox("El Rango de Vision no puede ser cero o menos", MessageType.Error);
            }


            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("✦ Presiona el boton para agregar la camara al target", GUILayout.Width(300f));
            if (GUILayout.Button("Agregar Camara",GUILayout.Width(250)))
            {
                SetOfCam(_nameCam);
                if (_targetToFollow != null)
                {
                    _cam.transform.SetParent(_targetToFollow.transform);
                    _targetToFollow.layer = LayerMask.NameToLayer("Minimap");
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

            }
            EditorGUILayout.EndHorizontal();
        }

      
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
    }
    void Step2()
    {
        EditorGUILayout.LabelField("2° PASO: agregando MiniMapa", EditorStyles.boldLabel);
        _showStep2 = EditorGUILayout.Foldout(_showStep2, "CREADOR MINIMAPA ");
        if (_showStep2)
        {
            EditorGUILayout.BeginHorizontal();
            _nameMap = EditorGUILayout.TextField("✦ Nombre MiniMapa ", _nameMap, GUILayout.Width(400));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("✦ Tamaño del MiniMapa ", GUILayout.Width(100));
            EditorGUILayout.LabelField(" Altura: ", EditorStyles.miniBoldLabel, GUILayout.Width(30));
            _highMap = EditorGUILayout.IntField(_highMap, GUILayout.Width(50));

            EditorGUILayout.LabelField(" Ancho: ", EditorStyles.miniBoldLabel, GUILayout.Width(35));
            _widthMap = EditorGUILayout.IntField(_widthMap, GUILayout.Width(50));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("✦ Posicion del MiniMapa ", GUILayout.Width(100));
            EditorGUILayout.LabelField(" Pos en X: ", EditorStyles.miniBoldLabel, GUILayout.Width(60));
            _positionXmap = EditorGUILayout.FloatField(_positionXmap, GUILayout.Width(50));

            EditorGUILayout.LabelField(" Pos en Y: ", EditorStyles.miniBoldLabel, GUILayout.Width(60));
            _positionYmap = EditorGUILayout.FloatField(_positionYmap, GUILayout.Width(50));
            EditorGUILayout.EndHorizontal();


            if (_highMap <= 0 || _widthMap <= 0)
            {
                EditorGUILayout.HelpBox("¡Los tamaños del mapa no pueden ser CERO o menor!", MessageType.Error);
            }

            _changeSideRTexture = EditorGUILayout.Toggle("AVANZADO: customizar tamaño del Texture Render", _changeSideRTexture);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("✦ Presiona el boton para agregar un MiniMapa", GUILayout.Width(300f));
            if (GUILayout.Button("Agregar MiniMapa", GUILayout.Width(250)))
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
        EditorGUILayout.LabelField("CAMARAS Y MINIMAPS EN ESCENA", EditorStyles.boldLabel);


        
        _SelectCam = (Camera)EditorGUILayout.ObjectField("Camara selecionada", _SelectCam, typeof(Camera), false);
        _SelectminiMap = (RawImage)EditorGUILayout.ObjectField("Mapa correspondiente", _SelectminiMap, typeof(RawImage), false);




        EditorGUILayout.LabelField("Cameras en escena", EditorStyles.boldLabel);
        //EditorGUILayout.LabelField("Cameras in scene: ", EditorStyles.miniBoldLabel);
        for (int i = 0; i < _camerasInScene.Count; i++)
        {

            if (_camerasInScene[i] != null && _mapsInScene[i] != null)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.ObjectField(_camerasInScene[i].name, _camerasInScene[i], typeof(Camera), false);
                EditorGUILayout.ObjectField(_mapsInScene[i].name, _mapsInScene[i], typeof(RawImage), false);

                if (GUILayout.Button("Seleccionar"))
                {
                    _SelectCam = _camerasInScene[i];

                    _SelectminiMap = _mapsInScene[i];
                    _assignValuesToCam = false;
                }


                if (GUILayout.Button("Borrar"))
                {
                    DestroyImmediate(_camerasInScene[i].gameObject);
                    DestroyImmediate(_mapsInScene[i].gameObject);
                    AssetDatabase.DeleteAsset("Assets/Images/" + _texturesInScene[i].name + ".renderTexture");
                    _camerasInScene.RemoveAt(i);
                    _texturesInScene.RemoveAt(i);
                    //if (_mapsInScene[i] != null)
                    _mapsInScene.RemoveAt(i);
                }



                EditorGUILayout.EndHorizontal();

            }


        }


        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Camara", EditorStyles.boldLabel);
        _SelectnameCam = EditorGUILayout.TextField("nombre Camara ", _SelectnameCam);
        _SelecthighCam = EditorGUILayout.IntField("altura Camara", _SelecthighCam );  
        _Selectfow = EditorGUILayout.FloatField("Rango de Vision", _Selectfow);
        _SelecttargetToFollow = (GameObject)EditorGUILayout.ObjectField("Target de la Camara", _SelecttargetToFollow, typeof(GameObject), true);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Minimap", EditorStyles.boldLabel);
        _selectNametMiniMap = EditorGUILayout.TextField("nombre MiniMapa ", _selectNametMiniMap);
        EditorGUILayout.BeginHorizontal();
        
        EditorGUILayout.LabelField("Posicion del MiniMapa", GUILayout.Width(100));
        EditorGUILayout.LabelField("Posicion en X:", EditorStyles.miniBoldLabel, GUILayout.Width(60));
        _selectPositionXmap = EditorGUILayout.FloatField(_selectPositionXmap, GUILayout.Width(50));

        EditorGUILayout.LabelField("Posicion en Y:", EditorStyles.miniBoldLabel, GUILayout.Width(60));
        _selectPositionYmap = EditorGUILayout.FloatField(_selectPositionYmap, GUILayout.Width(50));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Tamaño del mapa ", GUILayout.Width(100));
        EditorGUILayout.LabelField(" Alto: ", EditorStyles.miniBoldLabel, GUILayout.Width(30));
        _selectHighMap = EditorGUILayout.IntField(_selectHighMap, GUILayout.Width(50));

        EditorGUILayout.LabelField(" Ancho: ", EditorStyles.miniBoldLabel, GUILayout.Width(35));
        _selectWidthMap = EditorGUILayout.IntField(_selectWidthMap, GUILayout.Width(50));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("AVANZADO: ", EditorStyles.boldLabel);
        activeConfigAdvance = EditorGUILayout.Toggle("Cargar o Guardar config del miniMap", activeConfigAdvance,GUILayout.Width(400));

        EditorGUILayout.Space();
        if (activeConfigAdvance)
        {
            loadConfig = EditorGUILayout.Foldout(loadConfig, "Cargar Config");
            if (loadConfig)
            {
                EditorGUILayout.BeginHorizontal();
                config = (MiniMapConig)EditorGUILayout.ObjectField("configuracion", config, typeof(MiniMapConig), true);

                if (GUILayout.Button("Cargar Config"))
                {
                    if (config != null)
                    {
                        if (_SelectminiMap != null)
                        {

                            _SelectnameCam = config.nombreCamara;
                            _SelecthighCam = config.alturaCamara;
                            _Selectfow = config.rangoVision;
                            _selectPositionXmap = config.posicionMiniMap.x;
                            _selectPositionYmap = config.posicionMiniMap.x;
                            _selectHighMap = (int)config.TamanoMiniMap.y;
                            _selectWidthMap = (int)config.TamanoMiniMap.x;
                            _selectNametMiniMap = config.nombreMiniMapa;
                        }
                    }
                }
                EditorGUILayout.EndHorizontal();
            }


            EditorGUILayout.Space();
            EditorGUILayout.Space();

            saveConig = EditorGUILayout.Foldout(saveConig, "Guardar Config");
            if (saveConig)
            {

                namePath = EditorGUILayout.TextField("nombre Carpeta", namePath);
                nameMiniMapConfig = EditorGUILayout.TextField("nombre config", nameMiniMapConfig);


                if (GUILayout.Button("Guartdar Config"))
                {

                    if (_SelectminiMap != null)
                    {

                        if (namePath == null)
                        {
                            MiniMapScriptableObject.CreatedAssetConfigMiniMap<MiniMapConig>("", nameMiniMapConfig);
                        }

                        else if (File.Exists("Assets/"+namePath) || Directory.Exists("Assets/" + namePath))
                        {
                            MiniMapScriptableObject.CreatedAssetConfigMiniMap<MiniMapConig>(namePath, nameMiniMapConfig);

                            MiniMapConig aux = (MiniMapConig)AssetDatabase.LoadAssetAtPath("Assets/" + namePath + "/" + nameMiniMapConfig + ".asset", typeof(MiniMapConig));
                            aux.nombreCamara = _SelectCam.name;
                            aux.alturaCamara = (int)_SelectCam.transform.position.y;
                            aux.rangoVision = (int)_SelectCam.fieldOfView;
                            aux.nombreMiniMapa = _SelectminiMap.name;
                            aux.posicionMiniMap = new Vector2(_selectPositionXmap, _selectPositionYmap);
                            aux.TamanoMiniMap = new Vector2(_selectWidthMap, _selectHighMap);
                            AssetDatabase.SaveAssets();
                            AssetDatabase.Refresh();
                        }

                        else
                        {

                            string createdNewFolder = AssetDatabase.CreateFolder("Assets", namePath);
                            string newFolder = AssetDatabase.GUIDToAssetPath(createdNewFolder);
                            MiniMapScriptableObject.CreatedAssetConfigMiniMap<MiniMapConig>(namePath, nameMiniMapConfig);

                            MiniMapConig aux = (MiniMapConig)AssetDatabase.LoadAssetAtPath("Assets/" + namePath + "/" + nameMiniMapConfig + ".asset", typeof(MiniMapConig));
                            aux.nombreCamara = _SelectCam.name;
                            aux.alturaCamara = (int)_SelectCam.transform.position.y;
                            aux.rangoVision = (int)_SelectCam.fieldOfView;
                            aux.nombreMiniMapa = _SelectminiMap.name;
                            aux.posicionMiniMap = new Vector2(_selectPositionXmap,_selectPositionYmap);
                            aux.TamanoMiniMap = new Vector2(_selectWidthMap, _selectHighMap);
                            AssetDatabase.SaveAssets();
                            AssetDatabase.Refresh();



                        }


                    }
                    
                }
            }
                
          

           
        }

   

        if (_assignValuesToCam && _SelectCam != null && _SelectminiMap !=null)
        {
            _SelectCam.name = _SelectnameCam;
            _SelectCam.transform.position = new Vector3(_SelecttargetToFollow.transform.position.x, _SelecthighCam, _SelecttargetToFollow.transform.position.z);
            _SelectCam.fieldOfView =_Selectfow;
            _SelectminiMap.transform.position = new Vector3(_selectPositionXmap, _selectPositionYmap, _SelectminiMap.transform.position.z);
            _SelectminiMap.GetComponent<RectTransform>().sizeDelta = new Vector2(_selectWidthMap, _selectHighMap);
            _SelectminiMap.name = _selectNametMiniMap;
            _SelectCam.transform.SetParent(_SelecttargetToFollow.transform);
           

        }

        if (_SelectCam != null && !_assignValuesToCam)
        {
            _SelectnameCam = _SelectCam.name;
            //_SelectCam.name =_SelectnameCam;
            _SelecthighCam = (int)_SelectCam.transform.position.y;
      
            _SelecttargetToFollow = _SelectCam.transform.parent.gameObject;
            //_SelecthighCam = (int)_SelectCam.transform.position.y;

            if (_Selectfow > 0)
                _Selectfow = _SelectCam.fieldOfView;
            else
            {
                _Selectfow = 1;
            }



            if (_SelectminiMap != null)
            {
                _selectPositionXmap = _SelectminiMap.transform.position.x;
                _selectPositionYmap = _SelectminiMap.transform.position.y;
                _selectHighMap = (int)_SelectminiMap.GetComponent<RectTransform>().sizeDelta.y;
                _selectWidthMap = (int)_SelectminiMap.GetComponent<RectTransform>().sizeDelta.x;
                _selectNametMiniMap = _SelectminiMap.name;
            }


            _assignValuesToCam = true;
        }

      if (_SelectCam == null)
        {
            _SelectnameCam = "";
            _SelecthighCam = 0;
            _SelecttargetToFollow = null;
            _Selectfow = 1;

            _selectPositionXmap = 0;
            _selectPositionYmap = 0;
            _selectHighMap = 0;
            _selectWidthMap = 0;
            _selectNametMiniMap = "";

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
        _texturesInScene.Add(renderTexture);
        for (int i = 0; i < _texturesInScene.Count; i++)
        {
            if (_texturesInScene[i] != null)
            {
                _mapsInScene[i].GetComponent<RawImage>().texture = _texturesInScene[i];
                _camerasInScene[i].GetComponent<Camera>().targetTexture = _texturesInScene[i];
            }
          
             



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
        cam.tag = "MiniMap";
        _camerasInScene.Add(cam.GetComponent<Camera>());

        //ENFOCARSE EN UN UNICO LAYER. HACER UNA OPCION APARTE
        //cam.GetComponent<Camera>().cullingMask = 1 << LayerMask.NameToLayer("Minimap");
              
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
           _imageCont.tag = "MiniMap";
           _imageCont.name = name;
        _miniMap = _imageCont.GetComponent<RawImage>();
        DestroyImmediate(_mapsInScene[_mapsInScene.Count - 1].gameObject);
        // _mapsInScene.RemoveAt(0);
        // _mapsInScene.Insert(0, _miniMap);
        _mapsInScene[_mapsInScene.Count - 1] = _imageCont.GetComponent<RawImage>();
        for (int i = 0; i < _mapsInScene.Count; i++)
        {
           // Debug.Log(_mapsInScene[i].name +""+""+ i);
        }
        


    }
}
