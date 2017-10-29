using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;


public class ButtonGenerator : EditorWindow
{
    public Sprite buttonSprite;
    public bool preserveImage;

    public Vector3 buttonPos;

    public Vector2 buttonSize;
    public int widthSize;
    public int lengthSize;

    public string buttonText;
    public Font fontObject;
    public int fontSize;
    public Color textBcolor;

    public TestEvent data;
    public bool onButton1;
    public bool onButton2;
    public Button buttonObject;
    public Vector2 scrollinstr;

    private Canvas canvasMain;
    private Canvas _customCanvas;
    private GameObject _canvas;
    private string _canvasName;
    private bool _showCustomCanvas = false;
    private bool _searchCanvas = true;
    private bool _createCanvas = false;


    [MenuItem("HUD/Buttons Generator")]
    static void CreateWindow()
    {
        ((ButtonGenerator)GetWindow(typeof(ButtonGenerator))).Show();
    }

    void OnEnable()
    {
        if (canvasMain == null)
        {
            canvasMain = FindObjectOfType<Canvas>();
        }

        Fix();
    }


    private void OnGUI()
    {
        EditorGUILayout.Space();

        SetCanvas();
       
        EditorGUILayout.LabelField("1ª Imagen: ", EditorStyles.boldLabel);
        buttonSprite = (Sprite)EditorGUILayout.ObjectField("Imagen (Sprite):", buttonSprite, typeof(Sprite), true);
        preserveImage = EditorGUILayout.Toggle("Conservar la forma", preserveImage);
        if (preserveImage == false)
        {
            EditorGUILayout.HelpBox("La imagen podría deformarse", MessageType.Warning);
        }


        EditorGUILayout.Space();

        EditorGUILayout.LabelField("2ª Posición: ", EditorStyles.boldLabel);
        buttonPos = EditorGUILayout.Vector3Field("Posición:", buttonPos);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("3ª Tamaño: ", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        lengthSize = EditorGUILayout.IntField("Largo: ", lengthSize);
        widthSize = EditorGUILayout.IntField("Alto: ", widthSize);
        EditorGUILayout.EndHorizontal();
        if (preserveImage == true)
        {
            EditorGUILayout.HelpBox("Te recomiendo que el tamaño sea parecido a la imagen\nDe lo contrario podría quedar espacios transparentes que se activarian con el click", MessageType.Info);
        }

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("4ª Texto", EditorStyles.boldLabel);
        buttonText = EditorGUILayout.TextField("Texto: ", buttonText, GUILayout.Height(50));
        fontObject = (Font)EditorGUILayout.ObjectField("Fuente: ", fontObject, typeof(Font), false);
        if (fontObject == null)
        {
            EditorGUILayout.HelpBox("Si no eliges una fuente por defecto la letra es Arial", MessageType.Warning);
        }
        fontSize = EditorGUILayout.IntField("Tamaño: ", fontSize);
        if (fontSize <= 0)
        {
            fontSize = 1;
        }
        if (fontSize >0 && fontSize <= 5)
        {
            EditorGUILayout.HelpBox("La letra es tan pequeña que es probable que no sea legible", MessageType.Warning);
        }
        textBcolor = EditorGUILayout.ColorField("Color: ", textBcolor);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("5ª La acción/función que necesites:", EditorStyles.boldLabel);
        if (GUILayout.Button("Crea Tu Acción"))
        {
            TestEvent asset = CreateInstance<TestEvent>();
            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath("Assets/" + typeof(TestEvent).ToString() + ".asset");
            AssetDatabase.CreateAsset(asset, assetPathAndName);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
            onButton1 = true;    
        }
        
        if(onButton1 == true)
        {
            EditorGUILayout.BeginVertical(GUILayout.Height(120));
            EditorGUILayout.HelpBox("Sigue las instrucciones", MessageType.Info);
            scrollinstr = EditorGUILayout.BeginScrollView(scrollinstr, true, true);
            GUILayout.Label("1_ Ve a la carpeta Assets y haz click en TESTEVENT\n2_ Crea un prefab con el script de la accion deseada en Assets\n3_ Haz click en el signo +\n4_ Arrastra y Tira el prefab con la acción dentro de RUNTIME ONLY\n5_ Ahora en NO FUNCTION, busca tu script y dentro de el, tu función\n6_ Una vez completo el TESTEVENT, arrastralo hasta ACCIÓN A EJECUTAR: y tiralo ahí");
            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
        }
        
        data = (TestEvent)EditorGUILayout.ObjectField("Acción a ejecutar: ", data, typeof(TestEvent), false);
        if (data == null)
        {
            EditorGUILayout.HelpBox("No hay acción designada", MessageType.Error);
        }
        if (data != null && buttonObject != null)
        {
            buttonObject.onClick = data.eventoClick;
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        if (data != null)
        {
            if (GUILayout.Button("Listo!!! Ahora Construye!"))
            {
                TheButton();
                onButton2 = true;
            }
        }
        if (onButton2 == true)
        {
            EditorGUILayout.HelpBox("Recuerda que si hay alguna modificación que decides cambiar a último momento\nlo puedes hacer desde el inspector!", MessageType.Info);
        }
    }

    public void TheButton()
    {
        var buttonNew = new GameObject("Button");

        buttonNew.AddComponent<CanvasRenderer>();
        buttonNew.AddComponent<RectTransform>();
        buttonNew.AddComponent<Image>();
        buttonNew.GetComponent<Image>().preserveAspect = preserveImage;
        buttonNew.GetComponent<Image>().sprite = buttonSprite;
        //buttonNew.GetComponent<Image>().fillMethod =;
        //buttonNew.GetComponent<Image>().fillCenter = true;
        buttonNew.AddComponent<Button>();
        buttonNew.GetComponent<RectTransform>().position = new Vector3(buttonPos.x, buttonPos.y, buttonPos.z);
        buttonNew.GetComponent<RectTransform>().sizeDelta = new Vector2(lengthSize, widthSize);

        buttonNew.transform.SetParent(canvasMain.transform);

        var txtBNew = new GameObject("TextB");

        txtBNew.AddComponent<RectTransform>();
        txtBNew.AddComponent<CanvasRenderer>();
        txtBNew.AddComponent<Text>();
        txtBNew.GetComponent<Text>().text = buttonText;
        //txtBNew.GetComponent<Text>().fontStyle //popup de estilos 
        txtBNew.GetComponent<Text>().fontSize = fontSize;
        txtBNew.GetComponent<Text>().color = textBcolor;
        txtBNew.GetComponent<RectTransform>().position = new Vector3(buttonPos.x, buttonPos.y, buttonPos.z);
        txtBNew.GetComponent<RectTransform>().sizeDelta = new Vector2(lengthSize, widthSize);
        txtBNew.GetComponent<Text>().alignment = TextAnchor.MiddleCenter; // mostrar opciones, capaz?

        txtBNew.transform.SetParent(buttonNew.transform);

        buttonObject = buttonNew.GetComponent<Button>();

    }

    private void SetCanvas()
    {
        canvasMain = (Canvas)EditorGUILayout.ObjectField("Canvas actual", canvasMain, typeof(Canvas), false);

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        _searchCanvas = EditorGUILayout.Toggle("Buscar Canvas", _searchCanvas);
        _createCanvas = EditorGUILayout.Toggle("Crear Canvas", _createCanvas);
        _showCustomCanvas = EditorGUILayout.Toggle("Arrastrar Canvas", _showCustomCanvas);

        EditorGUILayout.EndHorizontal();

        if (_showCustomCanvas)
        {
            _searchCanvas = false;
            _createCanvas = false;
            _customCanvas = (Canvas)EditorGUILayout.ObjectField("Canvas principal", _customCanvas, typeof(Canvas), true);
            canvasMain = _customCanvas;
        }

        else if (_createCanvas)
        {
            _searchCanvas = false;
            _showCustomCanvas = false;
            _canvasName = EditorGUILayout.TextField("Nombre Canvas", _canvasName);
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
                EditorGUILayout.HelpBox("Hay un Canvas en CANVAS ACTUAL", MessageType.Error);
            }
        }

    }

    public void Fix()
    {
        lengthSize = 160;
        widthSize = 30;
        buttonText = "Nuevo Texto";
        fontSize = 14;
        textBcolor = Color.black;
        preserveImage = true;
        onButton1 = false;
        onButton2 = false;
    }

}



