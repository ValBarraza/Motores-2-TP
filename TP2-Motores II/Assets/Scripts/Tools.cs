using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools : MonoBehaviour {

	// Use this for initialization
	
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RotationObjet(float degrees, string axis)
    {

        if(axis == "x")
        transform.Rotate(new Vector3(Mathf.Round (transform.rotation.x + degrees), transform.rotation.y, transform.rotation.z));

        if (axis == "y")
            transform.Rotate(new Vector3(transform.rotation.x, Mathf.Round(transform.rotation.y + degrees), transform.rotation.z));

        if (axis == "z")
            transform.Rotate(new Vector3(transform.rotation.x, transform.rotation.z, Mathf.Round(transform.rotation.z + degrees)));
    }

}
