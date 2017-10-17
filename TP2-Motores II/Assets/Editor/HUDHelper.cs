using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class HUDHelper : EditorWindow
{
    private bool _groupToggle;
    private bool _okay;
    private bool _yesButton;
    private bool _yesText;
    private bool _yesMinimap;
    private bool isOPen;

    [MenuItem("HUDHelper/Helper")]
    static void myWindow()
    {
        ((HUDHelper)GetWindow(typeof(HUDHelper))).Show();
    }

    private void OnGUI()
    {
        minSize =new Vector2(380, 200);

        GUILayout.Label("Do you wanna learn more about this HUD Generator?", EditorStyles.boldLabel);


        Rect rectYes = EditorGUILayout.BeginHorizontal("Button");
        if (GUI.Button(rectYes, GUIContent.none))
            _groupToggle = true;
        GUILayout.Label("Yes");
        EditorGUILayout.EndHorizontal();

        Rect rectNO = EditorGUILayout.BeginHorizontal("Button");
        if (GUI.Button(rectNO, GUIContent.none))
            _okay = true;
        GUILayout.Label("No");
        EditorGUILayout.EndHorizontal();

        if (_okay== true)
        {
            GUILayout.Label(":c okay", EditorStyles.boldLabel);
        }

        if (_groupToggle == true)
        {
            GUILayout.Label("Let me help you kid", EditorStyles.boldLabel);
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("In Button option you can find diferent style of button");
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("In Text write the text you want in it douh");
            EditorGUILayout.EndHorizontal();

            GUILayout.Label("do you wanna try to create one?", EditorStyles.boldLabel);
            if (isOPen == false)
            {
                Rect rectYesToButton = EditorGUILayout.BeginHorizontal("Button");
                if (GUI.Button(rectYesToButton, GUIContent.none))
                {
                    _yesButton = true;
                    isOPen = true;
                }
                GUILayout.Label("yes");
                EditorGUILayout.EndHorizontal();
            }
            /*Rect rectYesToButton = EditorGUILayout.BeginVertical("Button");
            if (GUI.Button(rectYesToButton, GUIContent.none))
            {
                _yesButton = true;
                isOPen = true;
            }
            GUILayout.Label("yes");
            EditorGUILayout.EndVertical();*/

            if (_yesButton == true)
            {
                //boton que abre la ventana de botones
                Rect rectCreateButton = EditorGUILayout.BeginHorizontal("Button");
                if (GUI.Button(rectCreateButton, GUIContent.none))
                {
                    ((ButtonGenerator)GetWindow(typeof(ButtonGenerator))).Show();
                }
                GUILayout.Label("Create a button");
                EditorGUILayout.EndHorizontal();
            }

            if (_yesText == true)
            {

            }

            //boton que abre la ventana de textos
            Rect rectTextGenerator = EditorGUILayout.BeginHorizontal("Button");
            if (GUI.Button(rectTextGenerator, GUIContent.none))
            {
                ((TextGenerator)GetWindow(typeof(TextGenerator))).Show();
            }
            GUILayout.Label("Create a text");
            EditorGUILayout.EndHorizontal();

            //boton que abre la ventana del minimapa
            Rect rectMinimMap = EditorGUILayout.BeginHorizontal("Button");
            if (GUI.Button(rectMinimMap, GUIContent.none))
            {
                ((MiniMapGenerator)GetWindow(typeof(MiniMapGenerator))).Show();
            }
            GUILayout.Label("Create a minimap");
            EditorGUILayout.EndHorizontal();
        }

        


    }
}

