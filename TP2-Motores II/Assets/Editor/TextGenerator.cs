using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

//Que pueda crear un texto en el canvas y que pueda personalizarlo lo más posible desde la ventana
//BACKGROUND DE TEXTO //MARCO ------- 

public class TextGenerator : EditorWindow
{
    private Text _textToGenerateStatic;
    private Font _font;
    private Sprite _representationImage;
    private Sprite _background;
    private string _text;
    bool _showStaticPersonalization;

    private int _objectsCount;
    private Text _dinamicText;


    private GameObject _textObjetc;

    Canvas _canvas;

    [MenuItem("HUD/Text Generator")]
    static void CreateWindow()
    {
        ((TextGenerator)GetWindow(typeof(TextGenerator))).Show();
    }

    private void OnEnable()
    {
        if(_canvas == null)
        _canvas = FindObjectOfType<Canvas>();
    }

    private void OnGUI()
    {
        Repaint();

        StaticText();
        DinamicText();       
    }
    #region Static
    void StaticText() {
        EditorGUILayout.LabelField("Textos Estáticos", EditorStyles.boldLabel);

        //Esto--------------
        _canvas = (Canvas)EditorGUILayout.ObjectField("Canvas", _canvas, typeof(Canvas), true);
        _textToGenerateStatic = (Text)EditorGUILayout.ObjectField("aay dios", _textToGenerateStatic, typeof(Text), true);
        //------------------

        _font = (Font)EditorGUILayout.ObjectField("Fuente", _font, typeof(Font), true);
        _text = EditorGUILayout.TextField("Texto(texto)", _text);        

        _showStaticPersonalization = EditorGUILayout.Foldout(_showStaticPersonalization, "Personalizacion (opcional)");
        if (_showStaticPersonalization)
        {
            StaticPersonalization();
        }



      


            if (GUILayout.Button("Generar Texto")) {

           // GenerateStaticText();

                if (_font != null || _text != null)
                 {
                GenerateStaticText(); //si funciona descomentar esto
                 }
               
        }
        else if(_font == null)
            EditorGUILayout.HelpBox("Debes agregar una fuente/font", MessageType.Error);
    }
    void StaticPersonalization() {
        _representationImage = (Sprite)EditorGUILayout.ObjectField("Representación", _representationImage, typeof(Sprite), true);
        _background = (Sprite)EditorGUILayout.ObjectField("Background de texto", _background, typeof(Sprite), true);
        //Marco
    }
    void GenerateStaticText() {
        //Creo un GameObjetc en la escena
        _textObjetc = new GameObject();

        //Le agrego los componentes de TEXT
        _textObjetc.AddComponent<RectTransform>();
        _textObjetc.AddComponent<CanvasRenderer>();
        _textObjetc.AddComponent<Text>();

        //Le seteo el padre
        _textObjetc.transform.SetParent(_canvas.transform);

        //Lo igualo a tus variables
        _textToGenerateStatic = _textObjetc.GetComponent<Text>();
        _textToGenerateStatic.font = _font;
        _textToGenerateStatic.text = _text;
        _textObjetc.name = _text + " (static text)";


        //Text txt = Instantiate(_textToGenerateStatic);
        //_textToGenerateStatic.transform.SetParent(_canvas.transform);
    }




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
