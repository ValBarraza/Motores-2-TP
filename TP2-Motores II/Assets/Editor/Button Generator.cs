using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;


public class ButtonGenerator : EditorWindow
{
    private Main _target;
    public Button buttonObject;
    public RectTransform buttonRec;
    public Image buttonImage;
    public Sprite buttonSprite;
    public Font fontObject;
    public Text buttonTextUI;
    public string buttonText;
    public Vector3 buttonPos;
    public int fontSize;
    public Canvas theCanvas;



    [MenuItem("HUD/Buttons Generator")]
    static void CreateWindow()
    {
        ((ButtonGenerator)GetWindow(typeof(ButtonGenerator))).Show();
    }

    void OnEnable()
    {
        theCanvas = FindObjectOfType<Canvas>();
    }


    private void OnGUI()
    {
        EditorGUILayout.Space();



        buttonRec = (RectTransform)EditorGUILayout.ObjectField("Rect: ", buttonRec, typeof(RectTransform), false);
        //buttonObject = (Button)EditorGUILayout.ObjectField("Button: ", buttonObject, typeof(Button), false);
        //buttonImage = (Image)EditorGUILayout.ObjectField("Image: ", buttonImage, typeof(Image), false);
        buttonSprite = (Sprite)EditorGUILayout.ObjectField("Sprite: ", buttonSprite, typeof(Sprite), true);
        buttonPos = EditorGUILayout.Vector3Field("Position: ", buttonPos);

        buttonTextUI = (Text)EditorGUILayout.ObjectField("UI Text", buttonTextUI, typeof(Text), false);
        buttonText = EditorGUILayout.TextField("Text: ", buttonText);
        fontObject = (Font)EditorGUILayout.ObjectField("Font: ", fontObject, typeof(Font), false);
        fontSize = EditorGUILayout.IntField("Text Size: ", fontSize);






        if (GUILayout.Button("Build"))
        {
            TheButton();
        }


    }

    public void TheButton()
    {
        var buttonNew = new GameObject("Button");

        buttonNew.AddComponent<CanvasRenderer>();
        buttonRec = buttonNew.AddComponent<RectTransform>();
        //buttonImage = buttonNew.AddComponent<Image>();
        buttonNew.AddComponent<Image>();
        buttonNew.GetComponent<Image>().preserveAspect = true;
        buttonNew.AddComponent<Button>();
        buttonNew.GetComponent<RectTransform>().position = new Vector3(buttonPos.x, buttonPos.y, buttonPos.z);
        buttonNew.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 50);

        buttonSprite = buttonNew.GetComponent<Image>().sprite;
        buttonNew.transform.SetParent(theCanvas.transform);


        //var buttonOnPlace = Instantiate(buttonNew, buttonPos, Quaternion.identity);



        //var buttonTxtNew = new GameObject("Text");




    }



}

