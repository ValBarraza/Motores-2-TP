using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class TextElementConfig : EditorWindow {

    private GameObject _element;

    private Vector2 _ofMin;
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
        _ofMin = EditorGUILayout.Vector2Field("OfsetMin", _ofMin);

        if(GUILayout.Button("Realizar Cambios")) {
            Config();
        }
    }

    public void Config()
     {
        var component = _element.GetComponent<Text>();
        component.text = _text;
        component.font = _font;
        component.fontSize = _size;

        var pos = _element.GetComponent<RectTransform>();
        pos.offsetMin = _ofMin;
     }    
    public void TextElementConfigMethod(GameObject element) {
        _element = element;
    } 
}
