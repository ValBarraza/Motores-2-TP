using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;


public class ButtonGenerator : EditorWindow
{
    private TestEvent _target;
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
    public bool onButton;

    /*public delegate void ClickOn();
    public ClickOn delegate1;*/
    //public GameObject onClick;



    [MenuItem("HUD/Buttons Generator")]//BORRAR AL FINAL!!!!
    static void CreateWindow()
    {
        ((ButtonGenerator)GetWindow(typeof(ButtonGenerator))).Show();
    }

    void OnEnable()
    {
        //_target = new TestEvent();

        theCanvas = FindObjectOfType<Canvas>();

        /*if (buttonObject != null)
        {
            Button.ButtonClickedEvent var = buttonObject.GetComponent<Button>().onClick;
            var.AddListener(TheFunction);
            Debug.Log("add Listener");
        }*/

        onButton = false;

        Fix();


    }


    private void OnGUI()
    {
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("1ª Image and position", EditorStyles.boldLabel);
        //buttonObject = (Button)EditorGUILayout.ObjectField("Button: ", buttonObject, typeof(Button), false);
        buttonSprite = (Sprite)EditorGUILayout.ObjectField("Sprite: ", buttonSprite, typeof(Sprite), true);
        buttonPos = EditorGUILayout.Vector3Field("Position: ", buttonPos);
        buttonSize = EditorGUILayout.Vector2Field("Size: ", buttonSize);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("2ª Text", EditorStyles.boldLabel);
        buttonText = EditorGUILayout.TextField("Text: ", buttonText, GUILayout.Height(50));
        fontObject = (Font)EditorGUILayout.ObjectField("Font: ", fontObject, typeof(Font), false);
        fontSize = EditorGUILayout.IntField("Text Size: ", fontSize);
        textBcolor = EditorGUILayout.ColorField("Text Color: ", textBcolor);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("3ª The Action you want:", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        
        //Dentro de la fc On Click!!
        
        EditorGUILayout.EndHorizontal();

        //Button.ButtonClickedEvent var = buttonObject.GetComponent<Button>().onClick;

        //EditorGUILayout.PropertyField(data.FindProperty("testE"), new GUIContent("Funcáaaaá"));

        if (GUILayout.Button("Build"))
        {
            TheButton();
            onButton = true;
            
        }

        if(onButton == true)
        {
            Button.ButtonClickedEvent var = buttonObject.GetComponent<Button>().onClick;
            var.AddListener(TheFunction);
            Debug.Log("add Listener");
        }

        if(GUILayout.Button("Event on click scriptek"))
        {
            TestEvent asset = CreateInstance<TestEvent>();
            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath("Assets/" + typeof(TestEvent).ToString() + ".asset");
            AssetDatabase.CreateAsset(asset, assetPathAndName);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
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
        Button.ButtonClickedEvent var = buttonNew.GetComponent<Button>().onClick;
        var.AddListener(TheFunction);
        
        buttonObject = buttonNew.GetComponent<Button>();

        //buttonNew.GetComponent<Button>().onClick.AddListener(TheFunction);
        //buttonNew.GetComponent<Button>().onClick.AddListener(() => { TheFunction(); });
        //buttonNew.GetComponent<Button>().onClick.Invoke();
       
    }



    public void TheFunction()
    {
        Debug.Log("Click!!");

    }

    public void Fix()
    {
        buttonSize = new Vector2(150, 60);
        buttonText = "New Text";
        fontSize = 14;
        textBcolor = Color.black;
    }

}

