using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class HUDGenerator : EditorWindow {

    [MenuItem("HUD/HUD Generator")]
    static void openWindow()
    {
        GetWindow<HUDGenerator>();
    }

    private void OnGUI()
    {

    }
}
