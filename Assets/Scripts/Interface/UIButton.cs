/// <summary>
/// Controller for music selection button
/// </summary>
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class UIButton : MonoBehaviour {

    public AudioClip assignedClip;
    public UIController uic;
    public WorldController wc;
    public CameraController cc;

    //Assigns the chosen song as the active song and creates visual camera effects
    public void OnClick()
    {
        wc = GameObject.Find("Main").GetComponent<WorldController>();
        uic.activeTrack = gameObject;
        wc.AssignAudio(assignedClip);
        uic.ExpandTrackPanel();

        cc = Camera.main.GetComponent<CameraController>();
        StartCoroutine(cc.MakeShake(0.05f, 0.05f));
        float random = Random.Range(0.6f, 0.9f);
        cc.StartCoroutine("LessGlitch", random);
    }
}
