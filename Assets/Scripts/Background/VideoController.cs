using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoController : MonoBehaviour {

    public int numOfAnimations;
    private Animator animator;

	// Use this for initialization
	void Start () {
        animator = gameObject.GetComponent<Animator>();
        StartCoroutine("PlayVideos");
    }
	
	// Update is called once per frame
	public void Switch(bool switchOn)
    {
        if (switchOn)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.black;
        }
    }
    IEnumerator PlayVideos()
    {
        int rAnim = Random.Range(1, numOfAnimations);
        animator.SetTrigger(rAnim.ToString());
        float random = Random.Range(0.5f, (float)animator.GetCurrentAnimatorStateInfo(0).length);
        yield return new WaitForSeconds(random);
        StartCoroutine("PlayVideos");
    }
    public void SlowVideo(float speed)
    {
        //vp.playbackSpeed = speed;
    }
}
