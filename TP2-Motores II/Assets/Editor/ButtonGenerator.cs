using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;


public class ButtonGenerator : EditorWindow
{
    //private TestEvent _target;
    public TestEvent data;

    public Button buttonObject;
    public Sprite buttonSprite;
    public Vector2 buttonSize;
    public Vector3 buttonPos;

    public string buttonText;
    public Font fontObject;
    public int fontSize;
    public Color textBcolor;

    public Canvas theCanvas;
    public bool onButton;//La dejo solo xq Guille lo pidió

    [MenuItem("HUD/Buttons Generator")]//BORRAR AL FINAL!!!!
    static void CreateWindow()
    {
        ((ButtonGenerator)GetWindow(typeof(ButtonGenerator))).Show();
    }

    void OnEnable()
    {
        //_target = new TestEvent();

        theCanvas = FindObjectOfType<Canvas>();

        Fix();
    }


    private void OnGUI()
    {
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("1ª Imagen, tamaño y posición", EditorStyles.boldLabel); //Oraginzar tmb esto!
        //buttonObject = (Button)EditorGUILayout.ObjectField("Button: ", buttonObject, typeof(Button), false);
        buttonSprite = (Sprite)EditorGUILayout.ObjectField("Imagen (Sprite):", buttonSprite, typeof(Sprite), true);
        buttonPos = EditorGUILayout.Vector3Field("Posición:", buttonPos);
        buttonSize = EditorGUILayout.Vector2Field("Tamaño:", buttonSize);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("2ª Texto", EditorStyles.boldLabel);
        buttonText = EditorGUILayout.TextField("Texto: ", buttonText, GUILayout.Height(50));
        fontObject = (Font)EditorGUILayout.ObjectField("Fuente: ", fontObject, typeof(Font), false);
        fontSize = EditorGUILayout.IntField("Tamaño: ", fontSize);
        textBcolor = EditorGUILayout.ColorField("Color: ", textBcolor);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("3ª La acción/función que necesites:", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        
        //Dentro de la fc On Click!!
        
        EditorGUILayout.EndHorizontal();

        if(GUILayout.Button("Crea Tu Acción"))//Organizar mejor todo!
        {
            TestEvent asset = CreateInstance<TestEvent>();
            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath("Assets/" + typeof(TestEvent).ToString() + ".asset");
            AssetDatabase.CreateAsset(asset, assetPathAndName);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
        }

        data = (TestEvent)EditorGUILayout.ObjectField("Acción a ejecutar: ", data, typeof(TestEvent), false);
        if (data != null && buttonObject != null)
        {
            buttonObject.onClick = data.eventoClick;
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        if (GUILayout.Button("Listo!!! Ahora Construye!"))
        {
            TheButton();
        }

    }

    public void TheButton()
    {
        var buttonNew = new GameObject("Button");

        buttonNew.AddComponent<CanvasRenderer>();
        buttonNew.AddComponent<RectTransform>();
        buttonNew.AddComponent<Image>();
        buttonNew.GetComponent<Image>().preserveAspect = true;//Capaz?
        buttonNew.GetComponent<Image>().sprite = buttonSprite;
        //buttonNew.GetComponent<Image>().fillMethod =;
        //buttonNew.GetComponent<Image>().fillCenter = true;
        buttonNew.AddComponent<Button>();
        buttonNew.GetComponent<RectTransform>().position = new Vector3(buttonPos.x, buttonPos.y, buttonPos.z);
        buttonNew.GetComponent<RectTransform>().sizeDelta = new Vector2(buttonSize.x, buttonSize.y);

        buttonNew.transform.SetParent(theCanvas.transform);

        var txtBNew = new GameObject("TextB");

        txtBNew.AddComponent<RectTransform>();
        txtBNew.AddComponent<CanvasRenderer>();
        txtBNew.AddComponent<Text>();
        txtBNew.GetComponent<Text>().text = buttonText;
        //txtBNew.GetComponent<Text>().fontStyle //popup de estilos 
        txtBNew.GetComponent<Text>().fontSize = fontSize;
        txtBNew.GetComponent<Text>().color = textBcolor;
        txtBNew.GetComponent<RectTransform>().position = new Vector3(buttonPos.x, buttonPos.y, buttonPos.z);
        txtBNew.GetComponent<RectTransform>().sizeDelta = new Vector2(buttonSize.x, buttonSize.y);
        txtBNew.GetComponent<Text>().alignment = TextAnchor.MiddleCenter; // mostrar opciones, capaz?

        txtBNew.transform.SetParent(buttonNew.transform);
        
        buttonObject = buttonNew.GetComponent<Button>();
    
    }

    public void Fix()
    {
        buttonSize = new Vector2(150, 60);
        buttonText = "Nuevo Texto";
        fontSize = 14;
        textBcolor = Color.black;
    }

}

