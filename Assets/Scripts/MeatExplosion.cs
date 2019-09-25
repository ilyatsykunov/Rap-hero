/// <summary>
/// Creates a visual effect of flesh flying into the camera. 
/// </summary>
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatExplosion : MonoBehaviour {

    public Vector3 position;
    public bool isActive;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (isActive)
        {
            transform.Translate(position * Time.deltaTime * 0.8f, Space.World);
        }
        
    }
}
