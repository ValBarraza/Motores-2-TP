using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class HUDHelper : EditorWindow
{
    private string searchTools;
    private Object _focusObject;
    private GameObject target;
    private List<string> _myTools = new List<string> {"texto", "fuente", "color", "boton", "minimapa", "tamaño", "camara", "canvas", "sprite" };
    //private Dictionary<string, string> _windowOption= new Dictionary<string, string>();
    private List<string> ventanas = new List<string> { "boton", "texto", "mapa" };
    private Dictionary<string, List<string>> _auxi = new Dictionary<string, List<string>>();
    private Dictionary<string, string> _auxi2 = new Dictionary<string,string>();


    [MenuItem("HUD/Helper")]
    static void myWindow()
    {
        ((HUDHelper)GetWindow(typeof(HUDHelper))).Show();
    }

    private void OnGUI()
    {
        minSize = new Vector2(525, 400);

        GUILayout.Label("BUSCADOR DE HERRAMIENTA", EditorStyles.boldLabel);

        searcherHelper();
        _focusObject = EditorGUILayout.ObjectField(_focusObject, typeof(Object), true);


        GUILayout.Label("¿Desea aprender mas del HUD Generator?", EditorStyles.boldLabel);

        //boton que abre la ventana de botones
        Rect rectCreateButton = EditorGUILayout.BeginHorizontal("Button");
        if (GUI.Button(rectCreateButton, GUIContent.none))
        {
            ((ButtonGenerator)GetWindow(typeof(ButtonGenerator))).Show();
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


    private void searcherHelper()
    {
        /*_windowOption.Add("texto","generadorboton");
        _windowOption.Add("texto", "generadortexto");
        _windowOption.Add("fuente", "generadortexto");
        _windowOption.Add("color", "generadorboton");
        _windowOption.Add("color", "generadortexto");
        _windowOption.Add("minimapa", "generadormapa");
        _windowOption.Add("tamaño", "generadorboton");
        _windowOption.Add("tamaño", "generadortexto");
        _windowOption.Add("tamaño", "generadormapa");
        _windowOption.Add("camara", "generadormapa");
        _windowOption.Add("canvas", "generadorboton");
        _windowOption.Add("canvas", "generadortexto");
        _windowOption.Add("canvas", "generadormapa");
        _windowOption.Add("sprite","generadorboton");*/

        _auxi2.Add("texto",ventanas[0]+ventanas[1]);
        



        var searching = searchTools;
        searchTools = EditorGUILayout.TextField(searching);

        
        foreach (var item in _myTools)
        {
            if (item == searchTools)
            {
                EditorGUILayout.LabelField("opciones de " + item+" se encuentran en windows de"+_auxi2[item]);
               
            }
        }

        /*if (searching!=searchTools)
        {
            foreach (var item in _myTools)
            {

                if (item == searchTools)
                {
                    Debug.Log(item);
                }
            }
        }*/
        

        
    }
    
}

