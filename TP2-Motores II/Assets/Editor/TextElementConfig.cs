using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class TextElementConfig : EditorWindow {

    private GameObject _element;

    private Text _componentText;
    private RectTransform _componentRectTrans;

    private Vector2 _ofMin;
    private Font _font;
    private string _text;
    private int _size;

    public static void CreatedWindow() {
        GetWindow<TextElementConfig>();
    }

    private void OnGUI()
    {
        Debug.Log(_element);
        if (_element != null) if (GUILayout.Button("Actualizar")) GetFieldsOfElement();

        Fields();

        if (GUILayout.Button("Realizar Cambios")) MakeChanges();
    }
    void Fields()
    {
        _font = (Font)EditorGUILayout.ObjectField("Fuente", _font, typeof(Font), true);
        _size = EditorGUILayout.IntField("Tamaño del texto", _size);
        _text = EditorGUILayout.TextField("Texto(texto)", _text);
        _ofMin = EditorGUILayout.Vector2Field("OfsetMin", _ofMin);
    }
    void GetFieldsOfElement()
    {
        _componentText = _element.GetComponent<Text>();
        _text = _componentText.text;
        _font = _componentText.font;
        _size = _componentText.fontSize;

        _componentRectTrans = _element.GetComponent<RectTransform>();
        _ofMin = _componentRectTrans.offsetMin;
    }
    void MakeChanges()
    {
        _componentText.text = _text;
        _componentText.font = _font;
        _componentText.fontSize = _size;

        _componentRectTrans.offsetMin = _ofMin;
    }    
    public void TextElementConfigMethod(GameObject element)
    {
        _element = element;
    } 
}
