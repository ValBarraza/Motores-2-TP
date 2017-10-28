using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
//Configuración del elemento
public class TextElementConfig : EditorWindow {

    private GameObject _element;


    private Font _font;
    private string _text;
    private int _size;

    public static void CreatedWindow()
    {
        GetWindow<TextElementConfig>();
    }
    private void OnGUI()
    {
        _font = (Font)EditorGUILayout.ObjectField("Fuente", _font, typeof(Font), true);
        _size = EditorGUILayout.IntField("Tamaño del texto", _size);
        _text = EditorGUILayout.TextField("Texto(texto)", _text);
    }

    TextElementConfig Config()
     {
         var component = _element.GetComponent<Text>();
         component.text = _text;
          component.font = _font;
           component.fontSize = _size;
    
            return this;
     }

    public void TextElementConfigMethod(GameObject element)
    {
       _element = element;

    }
}
