using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class MiniMapCurrent : MonoBehaviour {


    GameObject[] TagsOfMiniMap = new GameObject[] { };
    public Dictionary<RawImage, Camera> MapsAndCam = new Dictionary<RawImage, Camera>();
    private List<Camera> camerasInScene = new List<Camera>();
    private List<RawImage> mapsInScene = new List<RawImage>();


    public string nombreCamara;
    public int alturaCamara;
    public int rangoVision;


    public string nombreMiniMapa;
    public Vector2 posicionMiniMap;
    public Vector2 TamanoMiniMap;


    private void Start()
    {
        SearchElementsOnScene();
       
    }


    public void SearchElementsOnScene()
    {
        Debug.Log("start");
        TagsOfMiniMap = GameObject.FindGameObjectsWithTag("MiniMap");

           MapsAndCam.Clear();
     
            for (int i = 0; i < TagsOfMiniMap.Length; i++)
            {
                if (TagsOfMiniMap[i] != null)
                {
                    if (TagsOfMiniMap[i].GetComponent<Camera>() != null)
                    {
                        camerasInScene.Add(TagsOfMiniMap[i].GetComponent<Camera>());


                    }


                    if (TagsOfMiniMap[i].GetComponent<RawImage>() != null)
                    {
                    mapsInScene.Add(TagsOfMiniMap[i].GetComponent<RawImage>());

                    }

                }

            }

        if (camerasInScene.Count > 0)
        {
            for (int i = 0; i < camerasInScene.Count; i++)
            {
                MapsAndCam.Clear();
                MapsAndCam.Add(mapsInScene[i], camerasInScene[i]);
            
       
            }

        }

       


    }
}
