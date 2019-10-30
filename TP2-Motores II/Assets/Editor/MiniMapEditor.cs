using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;


[CustomEditor(typeof(MiniMapCurrent))]
public class MiniMapEditor : Editor {

    private MiniMapCurrent _target;
    private Camera cam;
    private Rect windowsRect = new Rect(20, 20, 300, 100);
    public MiniMapGenerator miniMapGenerator;
    private GameObject targetCam;
   
    private Vector3[] verts = new Vector3[4];

    private void OnEnable()
    {
        _target = (MiniMapCurrent)target;
        RectTransform aux = _target.GetComponent<RectTransform>();
        _target.TamanoMiniMap = aux.sizeDelta;
        _target.SearchElementsOnScene();
        cam = _target.MapsAndCam[_target.GetComponent<RawImage>()];
        _target.posicionMiniMap = _target.transform.position;
        targetCam = cam.transform.parent.gameObject;
        
        verts[0] = new Vector3(targetCam.transform.position.x + cam.fieldOfView / 2 - cam.transform.position.y / 4, 0, targetCam.transform.position.z + cam.fieldOfView / 2 - cam.transform.position.y/4);
        verts[1] = new Vector3(targetCam.transform.position.x + cam.fieldOfView / 2 - cam.transform.position.y / 4, 0, targetCam.transform.position.z - cam.fieldOfView/2 + cam.transform.position.y / 4);
        verts[2] = new Vector3(targetCam.transform.position.x - cam.fieldOfView / 2 + cam.transform.position.y/4, 0, targetCam.transform.position.z - cam.fieldOfView/2 + cam.transform.position.y / 4);
        verts[3] = new Vector3(targetCam.transform.position.x - cam.fieldOfView / 2 + cam.transform.position.y/4, 0, targetCam.transform.position.z + cam.fieldOfView/ 2 - cam.transform.position.y / 4);
        Repaint();
        

       
    }

    private void OnSceneGUI()
    {

    

        Handles.BeginGUI();
        
        windowsRect = GUILayout.Window(0, windowsRect, WindowOfMiniMapData, _target.name);
        
        Handles.EndGUI();

        Handles.FreeMoveHandle(cam.transform.position, Quaternion.identity,10 ,Vector3.one, Handles.RectangleHandleCap);
       
        Handles.DrawLine(cam.transform.position, targetCam.transform.position);
        for (int i = 0; i < verts.Length; i++)
        {
            Handles.DrawLine(verts[i], cam.transform.position);
        }
        Handles.DrawSolidRectangleWithOutline(verts, new Color(1,1,1,0.2f),new Color(0,0,0,1));

        //Gizmos.DrawIcon(cam.transform.position, "camera");
    }

    
    void WindowOfMiniMapData(int ID)
    {

        EditorGUILayout.LabelField("Camara", EditorStyles.boldLabel);
        cam = (Camera)EditorGUILayout.ObjectField("camara", cam, typeof(Camera), false);
        targetCam = (GameObject)EditorGUILayout.ObjectField("target", targetCam, typeof(GameObject), false);

        _target.alturaCamara = EditorGUILayout.IntField("altura",(int)cam.transform.position.y);
        _target.rangoVision = EditorGUILayout.IntField("vision",(int)cam.fieldOfView);

        EditorGUILayout.LabelField("MiniMap", EditorStyles.boldLabel);
        _target.posicionMiniMap = EditorGUILayout.Vector2Field("posicion", _target.posicionMiniMap);
        _target.TamanoMiniMap = EditorGUILayout.Vector2Field("tamano", _target.TamanoMiniMap);
       
        Repaint();

    }
}
