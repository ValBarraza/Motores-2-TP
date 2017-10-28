using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    //public Button.ButtonClickedEvent eventoClick;
    

    // Use this for initialization
    void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
	   /*if (Input.GetKeyDown(KeyCode.Space))
        {
            eventoClick.AddListener(Test);
            Debug.Log("add");

         
            Debug.Log(eventoClick.GetPersistentEventCount());
        }	*/
	}

    public void Test()
    {
        print("CLICK");
    }
}
