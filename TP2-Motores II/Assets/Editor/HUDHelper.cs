﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class HUDHelper : EditorWindow
{
    private string searching;
    private string searchTools;
    private Object _focusObject;
    private GameObject target;
    private List<string> _myTools = new List<string> {"texto", "fuente", "color", "boton", "minimapa", "tamaño", "camara", "canvas", "sprite" };
    private List<string> ventanas = new List<string> { "genboton", "gentexto", "genmapa" };
    private Dictionary<string, List<string>> _auxi1 = new Dictionary<string, List<string>>();
    //private Dictionary<string, string> _auxi2 = new Dictionary<string, string>();
    public bool startSearch = false;



    [MenuItem("HUD/Helper")]
    static void myWindow()
    {
        ((HUDHelper)GetWindow(typeof(HUDHelper))).Show();
    }

    private void Oneable()
    {
        /*if (startSearch == true)
        {
            Debug.Log("entre");
            _auxi2.Add("texto", "SARASA");
           
            Debug.Log(startSearch);
        }*/

        
        
    }
     
    private void OnGUI()
    {
        minSize = new Vector2(525, 400);
       
        GUILayout.Label("BUSCADOR DE HERRAMIENTA", EditorStyles.boldLabel);

        searching = searchTools;
        searchTools = EditorGUILayout.TextField(searching);
        if (searching!= searchTools)
        {
            foreach (var item in _myTools)
            {
                if (item == searchTools)
                {
                    Debug.Log(startSearch);
                    startSearch = true;
                    //_auxi2.Add("texto", "generador de texto");
                    //_auxi2.Add("texto", "casa");
                    //Debug.Log(_auxi2[_auxi2.Keys.ElementAt(1)]);
                    List<string> text = new List<string>() { "generadorboton", "generadortexto" };
                    List<string> color = new List<string>() { "generadorboton", "generadortexto" };
                    List<string> size = new List<string>() { "generadorboton", "generadortexto", "generadormapa" };
                    List<string> canvas = new List<string>() { "generadorboton", "generadortexto", "generadormapa" };
                    List<string> font = new List<string>() { "generadortexto" };
                    List<string> minimap = new List<string>() { "generadormapa" }; 
                    List<string> camara = new List<string>() { "generadormapa" };
                    List<string> sprite = new List<string>() { "generadorboton" };
                    _auxi1.Add("texto",text);
                    _auxi1.Add("color",color);
                    _auxi1.Add("tamaño",size);
                    _auxi1.Add("canvas",canvas);
                    _auxi1.Add("fuente",font);
                    _auxi1.Add("minimapa",minimap);
                    _auxi1.Add("camara",camara);
                    _auxi1.Add("sprite",sprite);

                }
            }
            
        }

        if (startSearch && searchTools.Length>0)
        {
            //GUILayout.Label("opciones de " + searchTools + " se encuentran en windows de "+ _auxi2[_auxi2.Keys.ElementAt(0)]);
            foreach (var item in _auxi1[searchTools])
            {
                GUILayout.Label("opciones de " + searchTools + " se encuentran en windows de " + item);
            }
            //GUILayout.Label("opciones de " + searchTools + " se encuentran en windows de " + _auxi1[searchTools]);

        }
        else
        {
            startSearch = false;
        }
        //_focusObject = EditorGUILayout.ObjectField(_focusObject, typeof(Object), true);


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

        //_auxi2.Add("texto", "SARASA");
        //Debug.Log(startSearch);
        /*foreach (var item in _myTools)
        {
            if (item == searchTools)
            {
                EditorGUILayout.LabelField("opciones de " + item + " se encuentran en windows de");
                startSearch = true;
            }
        }*/

    }


}

