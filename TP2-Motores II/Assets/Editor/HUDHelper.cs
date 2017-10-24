using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class HUDHelper : EditorWindow
{
    //private bool _currentPopUp;
    private ButtonGenerator _isCreated;
    private bool _hideText;

    private GameObject target;

    [MenuItem("HUD/Helper")]
    static void myWindow()
    {
        ((HUDHelper)GetWindow(typeof(HUDHelper))).Show();
    }

    private void OnGUI()
    {
        minSize = new Vector2(380, 200);

        GUILayout.Label("¿Desea aprender mas del HUD Generator?", EditorStyles.boldLabel);

        //boton que abre la ventana de botones
        Rect rectCreateButton = EditorGUILayout.BeginHorizontal("Button");
        if (GUI.Button(rectCreateButton, GUIContent.none))
        {
            ((ButtonGenerator)GetWindow(typeof(ButtonGenerator))).Show();
            /*if (_isCreated != null)
            {
               _hideText= _isCreated.onButton;
            }*/
            
        }
        GUILayout.Label("Creador de botones");
        EditorGUILayout.EndHorizontal();
        
        GUILayout.Label("En la ventana de Button Generator puedes ver las siguiente opciones:", EditorStyles.boldLabel);
        GUILayout.Label("Al derecho de Sprite debes seleccionar el tipo de boton que quieres crear.");
        GUILayout.Label("En Position pones el lugar en el que quieres que aparezca.");
        GUILayout.Label("Size: dimensiones de ancho(X) y alto(Y).");
        GUILayout.Label("Text: nombre que quieres que el botón muestre.");
        GUILayout.Label("Font: tipografia del texto del botón.");
        GUILayout.Label("Text Color: es para elegir el color.");

        GUILayout.Label("Una vez hecho todo esto solo debes dar clic al botón Build para crear el botón.");

        //quiero tener acceso a las variables de los otros windows editors
        /*if (_isCreated.onClick != true)
        {
            GUILayout.Label("En la ventana de Button Generator puedes ver las siguiente opciones:", EditorStyles.boldLabel);
            GUILayout.Label("Al derecho de Sprite debes seleccionar el tipo de boton que quieres crear.");
            GUILayout.Label("En Position pones el lugar en el que quieres que aparezca.");
            GUILayout.Label("Size: dimensiones de ancho(X) y alto(Y).");
            GUILayout.Label("Text: nombre que quieres que el botón muestre.");
            GUILayout.Label("Font: tipografia del texto del botón.");
            GUILayout.Label("Text Color: es para elegir el color.");

            GUILayout.Label("Una vez hecho todo esto solo debes dar clic al botón Build para crear el botón.");
        }*/


        //boton que abre la ventana de textos
        Rect rectTextGenerator = EditorGUILayout.BeginHorizontal("Button");
        if (GUI.Button(rectTextGenerator, GUIContent.none))
        {
            ((TextGenerator)GetWindow(typeof(TextGenerator))).Show();
        }
        GUILayout.Label("Create a text");
        EditorGUILayout.EndHorizontal();

        GUILayout.Label("Canvas: el canvas del texto a modificar.");
        GUILayout.Label("aay dios: el texto que quieras editar.");
        GUILayout.Label("Fuente: la tipografia para el texto.");
        
        //boton que abre la ventana del minimapa
        Rect rectMinimMap = EditorGUILayout.BeginHorizontal("Button");
        if (GUI.Button(rectMinimMap, GUIContent.none))
        {
            ((MiniMapGenerator)GetWindow(typeof(MiniMapGenerator))).Show();
        }
        GUILayout.Label("Create a minimap");
        EditorGUILayout.EndHorizontal();

        GUILayout.Label("View: selecciona la camara de la escena.");
        GUILayout.Label("View Scene: selecciona la forma forma que quieres que tenga tu minimapa en la escena.");
        GUILayout.Label("Size texture: tamaño de la textura de tu minimapa.");
        GUILayout.Label("Size map: tamaño real del mapa a escalar.");


        

    }
    
}

