using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[Serializable]
public class TestEvent : ScriptableObject 
{
    public Button.ButtonClickedEvent eventoClick;
}

