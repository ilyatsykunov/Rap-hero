/// <summary>
/// Contols everything related to the user inteface:
///     Generation of buttons
///     Interactions on click
///     Interactions on hover
/// </summary>
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class UIController : MonoBehaviour {

    public AudioClip[] tracks;
    public GameObject btnPrefab;
    public GameObject contentHolder;

    public GameObject activeTrack;
    public List<GameObject> buttons = new List<GameObject>();

    public GameObject MenuHolder;

	// Use this for initialization
	void Start () {
        DisplayTracks();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EndGame();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (GameObject.Find("Main").GetComponent<WorldController>().isGameActive == false)
            {
                StartGame();
            }
        }
    }
    //Begins the game on click of button
    public void StartGame()
    {
        CameraController cc = Camera.main.GetComponent<CameraController>();
        StartCoroutine(cc.MakeShake(0.05f, 0.05f));
        float random = Random.Range(0.6f, 0.9f);
        cc.StartCoroutine("LessGlitch", random);
        MenuHolder.SetActive(false);
        GameObject.Find("Main").GetComponent<WorldController>().ActivateGame();
    }
    //Resets the game and opens menu
    public void EndGame()
    {
        MenuHolder.SetActive(true);
        GameObject.Find("Main").GetComponent<WorldController>().DeactivateGame();
    }
    //Exits to desktop
    public void ExitGame()
    {
        Application.Quit();
    }
    //Generates a button for each song that is in the library
    void DisplayTracks()
    {
        for(int i = 0; i < tracks.Length; i++)
        {
            GameObject btn = Instantiate(btnPrefab, contentHolder.transform);
            btn.GetComponentInChildren<Text>().text = tracks[i].name;
            btn.GetComponent<UIButton>().assignedClip = tracks[i];
            btn.GetComponent<UIButton>().uic = gameObject.GetComponent<UIController>();
            buttons.Add(btn);
        }
        activeTrack = buttons[0];
    }
    //Makes the panel with track buttons expand in width on hover
    public void ExpandTrackPanel()
    {
        foreach (GameObject btn in buttons)
        {
            if (btn == activeTrack)
            {
                StartCoroutine(IncreaseIE(btn, new Vector2(200f, 80f)));
            }
            else
            {
                StartCoroutine(IncreaseIE(btn, new Vector2(200f, 50f)));
            }
        }
    }
    //Makes the panel with track buttons shrink in width on hover
    public void ShrinkTrackPanel()
    {
        foreach (GameObject btn in buttons)
        {
            if (btn == activeTrack)
            {
                StartCoroutine(ShrinkIE(btn, new Vector2(200f, 80f)));
            }
            else
            {
                StartCoroutine(ShrinkIE(btn, new Vector2(50f, 50f)));
            }
        }
    }
    //Gradually increases the width of panel with track buttons
    public IEnumerator IncreaseIE(GameObject go, Vector2 targetSize)
    {
        if (go.GetComponent<RectTransform>().sizeDelta != targetSize) {
            float x = go.GetComponent<RectTransform>().sizeDelta.x;
            if (x > targetSize.x)
            {
                x += 5f;
            }
            float y = go.GetComponent<RectTransform>().sizeDelta.y;
            if (y > targetSize.y)
            {
                y += 5f;
            }
            go.GetComponent<RectTransform>().sizeDelta = new Vector2(x, y);
            yield return new WaitForSeconds(0.05f);
            if (go.GetComponent<RectTransform>().sizeDelta.x < targetSize.x || go.GetComponent<RectTransform>().sizeDelta.x < targetSize.x)
            {
                StartCoroutine(ShrinkIE(go, targetSize));
            }
        }

    }
    //Gradually decreases the width of panel with track buttons
    public IEnumerator ShrinkIE(GameObject go, Vector2 targetSize)
    {
        if (go.GetComponent<RectTransform>().sizeDelta != targetSize)
        {
            float x = go.GetComponent<RectTransform>().sizeDelta.x;
            if (x > targetSize.x)
            {
                x -= 5f;
            }
            float y = go.GetComponent<RectTransform>().sizeDelta.y;
            if (y > targetSize.y)
            {
                y -= 5f;
            }
            go.GetComponent<RectTransform>().sizeDelta = new Vector2(x, y);
            yield return new WaitForSeconds(0.05f);
            if (go.GetComponent<RectTransform>().sizeDelta.x > targetSize.x || go.GetComponent<RectTransform>().sizeDelta.x > targetSize.x)
            {
                StartCoroutine(ShrinkIE(go, targetSize));
            }

        }

    }
}
