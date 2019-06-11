using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public int score;
    public float scoreRate;
    private int scoreRateTotal;
    private float scoreRateSeconds;

    public Text scoreUI;

    public CameraController cc;
    public VideoController vc;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GetScoreRate(5f);
	}
    void GetScoreRate(float time)
    {
        if (scoreRateSeconds <= Time.deltaTime)
        {
            scoreRate = scoreRateTotal / time;
            scoreRateSeconds = time;
            scoreRateTotal = 0;
        }
        scoreRateSeconds -= Time.deltaTime;
    }
    public void AddScore(float distance)
    {
        int newScore = (int)(distance / 4);
        score += newScore;
        scoreRateTotal += newScore;
        scoreUI.text = score.ToString();
    }
}
