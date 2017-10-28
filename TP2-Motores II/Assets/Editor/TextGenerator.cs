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
    private List<GameObject> _textList;
    private GameObject _textElement;

    private Text _textToGenerateStatic;
    //Personalización
    private Color _color;
    private Vector2 _ofMax;
    private Vector2 _ofMin;
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

    Canvas _canvas;

    [MenuItem("HUD/Text Generator")]
    public static void CreateWindow()
    {
        GetWindow<TextGenerator>();
        
    }

    private void OnEnable()
    {
        _textList = new List<GameObject>();
        if(_canvas == null)
        _canvas = FindObjectOfType<Canvas>();
    }

    void OnGUI()
    {
        Repaint();
        ButtonConfig();
        StaticText();
        DinamicText();
        ShowList();

        if(GUILayout.Button("ABRIR OTRA VENTANITA"))
        {
            //TextoElementConfig.CreateWindow();
            // MiniMapGenerator.OpenWindow();
            TextElementConfig.CreatedWindow();
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
        //Marco
    }
    void GenerateStaticText() {
        //Creo un GameObjetc en la escena
        _textElement = new GameObject();

        //Le agrego los componentes de TEXT
        _textElement.AddComponent<RectTransform>();
        _textElement.AddComponent<CanvasRenderer>();
        _textElement.AddComponent<Text>();

        //AddToList(_textObjetc);

        //Le seteo el padre
        _textElement.transform.SetParent(_canvas.transform);

        _textToGenerateStatic = _textElement.GetComponent<Text>();
       // _textToGenerateStatic.color = _color;
        _textToGenerateStatic.font = _font;
        _textToGenerateStatic.text = _text;
        _textToGenerateStatic.fontSize = _fontSize;
        _textElement.name = _text + " (static text)";

        var pos = _textElement.GetComponent<RectTransform>();
        pos.offsetMax = _ofMax;
        pos.offsetMin = _ofMin;
        pos.anchoredPosition = _pivotePos;

        //_textList.Add(_textElement);
    }
    #region ListMethods
    void ShowList() {

        EditorGUILayout.LabelField("Lista", EditorStyles.boldLabel);
        _showList = EditorGUILayout.Foldout(_showList, "Mostrar lista");
        if (_showList)
        {
            EditorGUILayout.BeginVertical();
            foreach (RectTransform item in _canvas.GetComponentInChildren<RectTransform>())
            {
                string name = item.name;
                EditorGUILayout.ObjectField(name, item, typeof(RectTransform), false);

                var txt = item.GetComponent<Text>();
                if(txt != null) {
                    _textList.Add(item.gameObject);

                    ButtonEliminate(item.gameObject);
                    ButtonConfig();                
                }              
            }
            //EditorGUILayout.EndVertical();
        }
    }
    void ButtonEliminate(GameObject element) {
        if (GUILayout.Button("Eliminar")) {
            //_textList.Remove(element);         //COMO DESTRUIR EL ELEMENTO?
            element.SetActive(!element.activeSelf);
        }
    }
    void ButtonConfig() {
        Rect rectOpenNew = EditorGUILayout.BeginHorizontal("Button");
        if (GUI.Button(rectOpenNew, GUIContent.none))
            TextGenerator.CreateWindow();
        GUILayout.Label("Abrir la otra ventana");
        EditorGUILayout.EndHorizontal();
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
