using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[Serializable]
public class TestEvent : ScriptableObject 
{
    //public UnityEvent testE;
    public Button.ButtonClickedEvent eventoClick;

    /*void Start()
    {
        if (testE == null)
            testE = new UnityEvent();

        testE.AddListener(ShowAction);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && testE != null)
        {
            testE.Invoke();
        }
    }

    void ShowAction()
    {
        Debug.Log("Click!!!!!");
    }*/
}

