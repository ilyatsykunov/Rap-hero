using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour {

    public VideoClip[] allVideos;
    public Material white;
    public Material black;

    private VideoPlayer vp;

	// Use this for initialization
	void Start () {
        vp = gameObject.GetComponent<VideoPlayer>();
        StartCoroutine("PlayVideos");
    }
	
	// Update is called once per frame
	public void Switch(bool switchOn)
    {
        if (switchOn)
        {
            gameObject.GetComponent<MeshRenderer>().material = white;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material = black;
        }
    }
    VideoClip ChooseVideo()
    {
        int rVideo = Random.Range(0, allVideos.Length);
        vp.playbackSpeed = Random.Range(0.5f, 1.5f);
        vp.time = 1f;
        return allVideos[rVideo];
    }
    IEnumerator PlayVideos()
    {
        vp.clip = ChooseVideo();
        float random = Random.Range(0.5f, (float)(vp.clip.length));
        yield return new WaitForSeconds(random);
        StartCoroutine("PlayVideos");
    }
    public void SlowVideo(float speed)
    {
        vp.playbackSpeed = speed;
    }
}
