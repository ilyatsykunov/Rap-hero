using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class TileController : MonoBehaviour {

    public float top;
    public float bottom;
    public float speed;

    public int rowNumber;
    public GameObject[] rows;
    public bool hold;
    public bool doubleKey;
    public List<int> activeTiles;

    public float activePos;
    public GameObject activeTile;
    public string sActiveTile;
    public Material grey;
    public int tileCount;

    public bool isPlaying;
    private AudioSource audio;
    private float currentTime;
    private float addTime;

    // Use this for initialization
    void Start () {
        audio = gameObject.GetComponent<AudioSource>();
        rowNumber = rows.Length;

    }
	
	// Update is called once per frame
	void Update () {
        if(activeTile != null)
        {
            sActiveTile = activeTile.name;
            if (isPlaying == true)
            {
                addTime += Time.deltaTime;
            }

            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100.0f))
                {
                    if (hit.collider.gameObject == activeTile && isPlaying == false)
                    {
                        hit.collider.gameObject.GetComponent<MeshRenderer>().material = grey;
                        PlaySound();
                    }
                }
            }
            if (sActiveTile == "1")
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    PlaySound();
                }
            }
            else if (sActiveTile == "2")
            {
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    PlaySound();
                }
            }
            else if (sActiveTile == "3")
            {
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    PlaySound();
                }
            }
        }

    }
    public void RemoveTile()
    {
        activeTiles.Remove(activeTiles[0]);
    }
    public void AddTile(int tile)
    {
        activeTiles.Add(tile);
    }
    bool IsHold(int tile)
    {
        if (activeTiles.Contains(tile))
        {
            if (activeTiles[0] == activeTiles[1])
            {
                return true;
            }
        }
        return false;
    }
    public void PlaySound()
    {
        if(isPlaying == false)
        {
            tileCount++;
            currentTime = currentTime + addTime;
            audio.time = currentTime;
            audio.Play();
            addTime = 0f;
            activeTile.GetComponent<MeshRenderer>().material = grey;
            isPlaying = true;
        }
    }
    public void StopSound()
    {
        audio.Stop();
        isPlaying = false;
    }

}
