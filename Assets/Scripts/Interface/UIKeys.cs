using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIKeys : MonoBehaviour {

    public Image[] keyImages;
    Color32 black;
    Color32 red;

	// Use this for initialization
	void Start () {
        black = new Color32(0, 0, 0, 150);
        red = new Color32(255, 0, 0, 150);
    }
	
	// Update is called once per frame
	void Update () {
        CheckInput();
	}
    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            keyImages[0].color = red;
        }
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            keyImages[0].color = black;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            keyImages[1].color = red;
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            keyImages[1].color = black;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            keyImages[2].color = red;
        }
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            keyImages[2].color = black;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            keyImages[3].color = red;
        }
        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            keyImages[3].color = black;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            keyImages[4].color = red;
        }
        if (Input.GetKeyUp(KeyCode.Alpha5))
        {
            keyImages[4].color = black;
        }
    }
}
