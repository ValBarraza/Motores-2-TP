using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Tools))]
public class ToolsObject:Editor {


    public Rect _windowRect = new Rect(20,20,100,200);
    public Rect _xRect = new Rect(0, 40, 200, 150);
    public Tools _target;

    public bool selectX;
    public bool selectY;
    public bool selectZ;

    public float rotationObjet;

    private void OnEnable()
    {
        _target = (Tools)target;
       
    }
    private void OnSceneGUI()
    {
        // windowsRect = GUILayout.Window(0, windowsRect, WindowOfMiniMapData, _target.name);
        _windowRect = GUILayout.Window(0, _windowRect, WindowTools, "Tools");

        if (selectX)
        {
            Handles.color = new Color(0, 1, 1, 1f);
            Handles.DrawLine(_target.transform.position, _target.transform.position + _target.transform.forward * 10);
            Handles.color = new Color(1, 0, 0, 0.5f);
            Handles.DrawLine(_target.transform.position, _target.transform.position + -Vector3.forward * 10);
            Handles.color = new Color(1, 0, 0, 0.5f);
            Handles.DrawLine(_target.transform.position, _target.transform.position + Vector3.forward * 10);
            Handles.color = new Color(1, 0, 0, 0.5f);
            Handles.DrawLine(_target.transform.position, _target.transform.position + Vector3.up * 10);
            Handles.color = new Color(1, 0, 0, 0.5f);
            Handles.DrawLine(_target.transform.position, _target.transform.position + -Vector3.up * 10);


            Handles.color = new Color(1, 0, 0, 0.1f);
            float aux = _target.transform.rotation.x * Mathf.Rad2Deg*2;
            // Handles.DrawSolidDisc(_target.transform.position, _target.transform.right, aux);
               Handles.color = new Color(1, 0, 0, 0.5f);
                Handles.DrawSolidArc(_target.transform.position, Vector3.right, Vector3.forward, aux, 4);

            float strength = 50 * -_target.transform.localRotation.x;
            Vector3 begin = _target.transform.localPosition;
            Vector3 end = _target.transform.localPosition + - new Vector3(_target.transform.localPosition.x, 0,0) + Vector3.forward * strength;


            Handles.DrawBezier(begin,end, begin +_target.transform.forward * strength/2, begin + _target.transform.forward * strength, new Color(1, 0, 0, 1),null, 5);

        }

        else if (selectY)
        {
            Handles.color = new Color(0, 1, 0, 0.1f);
            Handles.DrawSolidDisc(_target.transform.position, _target.transform.up, _target.transform.localScale.x * 1.1f);
        }

        else if (selectZ)
        {
            Handles.color = new Color(0, 0, 1, 0.1f);
            Handles.DrawSolidDisc(_target.transform.position, _target.transform.forward, _target.transform.localScale.x * 1.1f);
        }

        //Debug.Log(Vector3.Cross(new Vector3(0, _target.transform.rotation.y, 0), new Vector3(0, 0, _target.transform.rotation.z)));


    }

    void WindowTools(int ID)
    {
        EditorGUILayout.BeginHorizontal();


        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("X", EditorStyles.boldLabel,GUILayout.Width(15));
        selectX = EditorGUILayout.Toggle(selectX);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Y", EditorStyles.boldLabel, GUILayout.Width(15));
        selectY = EditorGUILayout.Toggle(selectY);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Z", EditorStyles.boldLabel, GUILayout.Width(15));
        selectZ = EditorGUILayout.Toggle(selectZ);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndHorizontal();

        if (selectX)
        {
            Handles.DrawSolidRectangleWithOutline(_xRect, new Color(1, 0, 0, 0.1f), new Color(1, 0, 0, 1));
            selectY = false;
            selectZ = false;
        }


        else if (selectY)
        {
            Handles.DrawSolidRectangleWithOutline(_xRect, new Color(0, 1, 0, 0.1f), new Color(0, 1, 0, 1));
            selectX = false;
            selectZ = false;
        }


        else if (selectZ)
        {
            Handles.DrawSolidRectangleWithOutline(_xRect, new Color(0, 0, 1, 0.1f), new Color(0, 0, 1, 1));
            selectY = false;
            selectX = false;
        }




        EditorGUILayout.LabelField("Rotation", EditorStyles.boldLabel);
        EditorGUILayout.Space();


        EditorGUILayout.BeginHorizontal();

       
        if (GUILayout.Button("1°",GUILayout.Width(40)))
        {
            if(selectX)
            _target.RotationObjet(1,"x");

            if (selectY)
                _target.RotationObjet(1, "y");

            if (selectZ)
                _target.RotationObjet(1, "z");
        }
        if (GUILayout.Button("45°", GUILayout.Width(40)))
        {
            if (selectX)
                _target.RotationObjet(45, "x");

            if (selectY)
                _target.RotationObjet(45, "y");

            if (selectZ)
                _target.RotationObjet(45, "z");
        }
        if (GUILayout.Button("90°", GUILayout.Width(40)))
        {
            if (selectX)
                _target.RotationObjet(90, "x");

            if (selectY)
                _target.RotationObjet(90, "y");

            if (selectZ)
                _target.RotationObjet(90, "z");
        }
        if (GUILayout.Button("180°", GUILayout.Width(40)))
        {
            if (selectX)
                _target.RotationObjet(180, "x");

            if (selectY)
                _target.RotationObjet(180, "y");

            if (selectZ)
                _target.RotationObjet(180, "z");
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();


        if (GUILayout.Button("-1°", GUILayout.Width(40)))
        {
            if (selectX)
                _target.RotationObjet(-1, "x");

            if (selectY)
                _target.RotationObjet(-1, "y");

            if (selectZ)
                _target.RotationObjet(-1, "z");
        }
        if (GUILayout.Button("-45°", GUILayout.Width(40)))
        {
            if (selectX)
                _target.RotationObjet(-45, "x");

            if (selectY)
                _target.RotationObjet(-45, "y");

            if (selectZ)
                _target.RotationObjet(-45, "z");
        }
        if (GUILayout.Button("-90°", GUILayout.Width(40)))
        {
            if (selectX)
                _target.RotationObjet(-90, "x");

            if (selectY)
                _target.RotationObjet(-90, "y");

            if (selectZ)
                _target.RotationObjet(-90, "z");
        }
        if (GUILayout.Button("-180°", GUILayout.Width(40)))
        {
            if (selectX)
                _target.RotationObjet(-180, "x");

            if (selectY)
                _target.RotationObjet(-180, "y");

            if (selectZ)
                _target.RotationObjet(-180, "z");
        }
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Position", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("X+"))
        {

        }
        if (GUILayout.Button("Y+"))
        {

        }
        if (GUILayout.Button("Z+"))
        {

        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.LabelField("Scale", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("X+"))
        {

        }
        if (GUILayout.Button("Y+"))
        {

        }
        if (GUILayout.Button("Z+"))
        {

        }
        EditorGUILayout.EndHorizontal();
    }
}
