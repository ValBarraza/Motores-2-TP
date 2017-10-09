using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class TextGenerator : EditorWindow
{
    public string _text;
    public Sprite image;
    private Font _font;
    private int _objectsCount;
    private Text _dinamicText;

    [MenuItem("HUD/Text Generator")]
    static void CreateWindow()
    {
        ((TextGenerator)GetWindow(typeof(TextGenerator))).Show();
    }
    private void OnGUI()
    {
        Repaint();

        StaticText();

        EditorGUILayout.Space();

        DinamicText();
        
    }
    void StaticText() {
        EditorGUILayout.LabelField("Textos Estáticos", EditorStyles.boldLabel);

        _font = (Font)EditorGUILayout.ObjectField("Fuente", _font, typeof(Font), true);
        _text = EditorGUILayout.TextField("Texto(texto)", _text);
        if (_text != null) EditorGUILayout.HelpBox("Hay algo", MessageType.Info);
        else EditorGUILayout.HelpBox("no hay nada", MessageType.Info);
        image = (Sprite)EditorGUILayout.ObjectField("Representación", image, typeof(Sprite), true);
    }
    void DinamicText() { 
        //Como hacer que el texto se adapte al tamaño de la ventana?
        //Como vincular un contador creado por el usuario? 

        EditorGUILayout.LabelField("Textos Dinámicos", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("Arrastre el texto y el contador que quiera que se vincule al mismo a sus respectivos campos", EditorStyles.label);

        //_objectsCount = ()EditorGUILayout.ObjectField("Contador", _objectsCount, typeof(), true);
        _dinamicText = (Text)EditorGUILayout.ObjectField("Texto(UI)", _dinamicText, typeof(Text), true);

        //_dinamicText.text = "" + _objectsCount;
    } 
}
