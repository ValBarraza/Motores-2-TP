using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

//Que pueda crear un texto en el canvas y que pueda personalizarlo lo más posible desde la ventana
//BACKGROUND DE TEXTO //MARCO ------- 
//Ver como hacer un por defecto;

public class TextGenerator : EditorWindow
{
    #region CanvasFields
    private GameObject _canvas;
    private Canvas canvasMain;
    private Canvas _customCanvas;

    private string _canvasName;

    bool _searchCanvas;
    bool _createCanvas;
    bool _showCustomCanvas;
    #endregion

    private List<GameObject> _textList;
    private GameObject _textElement;
    private TextElementConfig _classElement;

    private Text _textToGenerateStatic;
    //Personalización
    private Color _color;
    private Vector2 _ofMax;
    private Vector2 _ofMin = new Vector2(-120,-120);
    private Vector2 _pivotePos;
    private Font _font;
    private Sprite _representationImage;
    private Sprite _background;
    private int _fontSize;
    private string _text;
    //FoldOuts
    bool _showPersStatic;
    bool _showPersSizeRect;
    bool _showList;
    //Lista
    List<GameObject> _txtList;

    private int _objectsCount;
    private Text _dinamicText;

    //Canvas _canvas;

    [MenuItem("HUD/Text Generator")]
    public static void CreateWindow()
    {
        GetWindow<TextGenerator>();
    }

    private void OnEnable()
    {
        _textList = new List<GameObject>();
    }

    void OnGUI()
    {
        
        Repaint();

        if(canvasMain == null) SetCanvas();
        StaticText();
        DinamicText();
        if(canvasMain != null) ShowList();
    }
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
                EditorGUILayout.HelpBox("Hay un Canvas en CANVAS ACTUAL", MessageType.Error);
            }


        }

    }

    #region Static
    void StaticText() {
        EditorGUILayout.LabelField("Textos Estáticos", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("1º Fuente, tamaño, texto", EditorStyles.boldLabel);
        _font = (Font)EditorGUILayout.ObjectField("Fuente", _font, typeof(Font), true);
        _fontSize = EditorGUILayout.IntField("Tamaño del texto", _fontSize);
        _text = EditorGUILayout.TextField("Texto(texto)", _text);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("2º Color (opcional)", EditorStyles.boldLabel);
        SetColor();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("3º Posición pivot (opcional)", EditorStyles.boldLabel);
        PivotePositionRect();
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("4º Tamaño de rect (No opcional)", EditorStyles.boldLabel);
        _showPersSizeRect = EditorGUILayout.Foldout(_showPersSizeRect, "Modificar tamaño del rect");
        if (_showPersSizeRect) {
            SizeRect();
        }

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("5º Más personalización (opcional)", EditorStyles.boldLabel);
        _showPersStatic = EditorGUILayout.Foldout(_showPersStatic, "Personalizacion (opcional)");
        if (_showPersStatic) {
            StaticPersonalization();
        }

        if (_font == null && _text == null)
        {
            EditorGUILayout.HelpBox("Debes agregar una fuente/font y un texto", MessageType.Error);
        }
        else if(_font != null)
        {
            if (GUILayout.Button("Generar Texto"))
            {
                GenerateStaticText();
            }
        }
    }
    #region Personalization

    void SetColor() {
        _color = EditorGUILayout.ColorField("Color de texto", _color);
    }
    void PivotePositionRect() {
        _pivotePos = EditorGUILayout.Vector2Field("Pivote del rect", _pivotePos);
    }
    void SizeRect() {
        EditorGUILayout.LabelField("Especificar la posicón de la esquina iferior izquierda del rect", EditorStyles.boldLabel);
        _ofMin = EditorGUILayout.Vector2Field("OfssetMin", _ofMin);
        EditorGUILayout.LabelField("Especificar la posicón de la esquina superior derecha del rect", EditorStyles.boldLabel);
        _ofMax = EditorGUILayout.Vector2Field("OfssetMax", _ofMax);
    }
    void StaticPersonalization() {
        _representationImage = (Sprite)EditorGUILayout.ObjectField("Representación", _representationImage, typeof(Sprite), true);
        _background = (Sprite)EditorGUILayout.ObjectField("Background de texto", _background, typeof(Sprite), true);
    }

    #endregion
    void GenerateStaticText() {
        _textElement = new GameObject();

        _textElement.AddComponent<RectTransform>();
        _textElement.AddComponent<CanvasRenderer>();
        _textElement.AddComponent<Text>();

        _textElement.transform.SetParent(canvasMain.transform);

        _textToGenerateStatic = _textElement.GetComponent<Text>();

        _textToGenerateStatic.font = _font;
        _textToGenerateStatic.text = _text;
        _textToGenerateStatic.fontSize = _fontSize;
        _textElement.name = _text + " (static text)";

        var pos = _textElement.GetComponent<RectTransform>();
        pos.offsetMax = _ofMax;
        pos.offsetMin = _ofMin;
        pos.anchoredPosition = _pivotePos;
    }
    #region ListMethods
    void ShowList() {
        EditorGUILayout.LabelField("Lista", EditorStyles.boldLabel);

        _showList = EditorGUILayout.Foldout(_showList, "Mostrar lista");
        if (_showList)
        {
            EditorGUILayout.BeginVertical();
            foreach (RectTransform item in canvasMain.GetComponentInChildren<RectTransform>())
            {
                string name = item.name;
                EditorGUILayout.ObjectField(name, item, typeof(RectTransform), false);

                var txt = item.GetComponent<Text>();
                if (txt != null)
                {
                    _textList.Add(item.gameObject);

                    ButtonEliminate(item.gameObject);
                    ButtonConfig(item.gameObject);
                }
            }
        }
    }
    void ButtonEliminate(GameObject element) {
        if (GUILayout.Button("Eliminar")) {
            //_textList.Remove(element);         //COMO DESTRUIR EL ELEMENTO?
            element.SetActive(!element.activeSelf);
        }
    }
    void ButtonConfig(GameObject e) {
        if(GUILayout.Button("Abrir configuración")) {
            TextElementConfig.CreatedWindow();
            var _classConfig = (TextElementConfig)GetWindow(typeof(TextElementConfig));
            _classConfig.TextElementConfigMethod(e);
        }
    }
    #endregion

    #endregion
    #region Dinamics

    void DinamicText() {
        //Como hacer que el texto se adapte al tamaño de la ventana?
        //Como vincular un contador creado por el usuario? 

        EditorGUILayout.LabelField("Textos Dinámicos", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("Arrastre el texto y el contador que quiera que se vincule al mismo a sus respectivos campos", EditorStyles.label);

        //_objectsCount = ()EditorGUILayout.ObjectField("Contador", _objectsCount, typeof(), true);
        _dinamicText = (Text)EditorGUILayout.ObjectField("Texto(UI)", _dinamicText, typeof(Text), true);

        //_dinamicText.text = "" + _objectsCount;
    }

    #endregion
}
