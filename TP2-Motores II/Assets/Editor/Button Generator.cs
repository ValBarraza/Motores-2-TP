using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class ButtonGenerator : EditorWindow
{
    public Button buttonObject;
    public Font fontObject;
    //public Text buttonText;
    public string buttonText;
    public Vector3 buttonPos;
    public int fontSize;

    [MenuItem("HUD/Buttons Generator")]
    static void CreateWindow()
    {
        ((ButtonGenerator)GetWindow(typeof(ButtonGenerator))).Show();
    }

    void OnEnable()
    {
        //GetComponent<Text>();
    }


    private void OnGUI()
    {
        EditorGUILayout.Space();
        buttonObject = (Button)EditorGUILayout.ObjectField("Button: ", buttonObject, typeof(Button), false);
        buttonText = EditorGUILayout.TextField("Text: ", buttonText);
        fontObject = (Font)EditorGUILayout.ObjectField("Font: ", fontObject, typeof(Font), false);
        fontSize = EditorGUILayout.IntField("Text Size: ", fontSize);
        buttonPos = EditorGUILayout.Vector3Field("Position: ", buttonPos);
    }


}
