using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class WorldController : MonoBehaviour {

    public int activeTile;
    public int rowsGone;
    public int currentRow;

    [SerializeField]
    private float top;
    [SerializeField]
    private float bottom;
    public float activePos;
    public float speed;
    public GameObject connection;

    [SerializeField]
    private int songLength;

    public bool doubleKey;
    public List<Tile> activeTiles;
    [HideInInspector]
    public GameObject[] rows;
    [HideInInspector]
    public int activeRow;

    [HideInInspector]
    public bool isPlaying;
    private AudioSource audio;
    private float currentTime;
    private float addTime;

    // Use this for initialization
    void Start () {
        PopulateMap();
	}
	
	// Update is called once per frame
	void Update () {
        Play();
    }
    void Play()
    {
        if(activeTiles.Count > 0)
        {
            MoveTiles();
            if(rowsGone >= -Mathf.RoundToInt(activePos))
            {
                if (isPlaying == true)
                {
                    addTime += Time.deltaTime;
                }
                activeTile = activeTiles[rowsGone + Mathf.RoundToInt(activePos)].pos;
                if (activeTile == 0)
                {
                    if (IsHold() == true)
                    {
                        if (Input.GetKey(KeyCode.Alpha1))
                        {
                            PlaySound();
                        }
                    }
                    else
                    {
                        if (Input.GetKeyDown(KeyCode.Alpha1))
                        {
                            PlaySound();
                        }
                    }
                }
                if (activeTile == 1)
                {
                    if (IsHold() == true)
                    {
                        if (Input.GetKey(KeyCode.Alpha2))
                        {
                            PlaySound();
                        }
                    }
                    else
                    {
                        if (Input.GetKeyDown(KeyCode.Alpha2))
                        {
                            PlaySound();
                        }
                    }
                }
                if (activeTile == 2)
                {
                    if (IsHold() == true)
                    {
                        if (Input.GetKey(KeyCode.Alpha3))
                        {
                            PlaySound();
                        }
                    }
                    else
                    {
                        if (Input.GetKeyDown(KeyCode.Alpha3))
                        {
                            PlaySound();
                        }
                    }
                }
            }

        }
    }
    void MoveTiles()
    {
        for(int i = 0; i < rows.Length; i++)
        {
            if (rows[i].transform.position.z <= activePos && rows[i].transform.position.z > activePos - 0.1f)
            {
                SetActiveRow(i);
            }
            if (rows[i].transform.position.z <= bottom)
            {
                ResetRow(i);
            }
        }
    }
    void PopulateMap()
    {
        int rowLength = rows[0].GetComponent<RowController>().tiles.Length;
        activeTiles = new List<Tile>();
        audio = gameObject.GetComponent<AudioSource>();
        //songLength = Mathf.RoundToInt(audio.clip.length);
        for (int i = 0; i < songLength; i++)
        {
            int pos = Random.Range(0, rowLength);
            Tile tileObj = new Tile(pos);
            if (i > 0 && activeTiles[i - 1].pos == pos)
            {
                activeTiles[i - 1].next = true;
                tileObj.prev = true;
            }
            activeTiles.Add(tileObj);
        }
        for(int i = 0; i < rows.Length; i++)
        {
            rows[i].GetComponent<RowController>().Disable();
            rows[i].GetComponent<RowController>().Spawn();
        }
    }
    void ResetRow(int row)
    {
        rowsGone++;
        rows[row].GetComponent<RowController>().SetTiles(activeTiles[rowsGone].pos, activeTiles[rowsGone].prev, activeTiles[rowsGone].next);
        rows[row].transform.position = new Vector3(rows[row].transform.position.x, rows[row].transform.position.y, top);
        rows[row].GetComponent<RowController>().Enable();
        StopSound();
    }
    void SetActiveRow(int newActive)
    {
        activeRow = newActive;
    }
    bool IsHold()
    {
        if (activeTiles[rowsGone + Mathf.RoundToInt(activePos)].prev == true || activeTiles[rowsGone + Mathf.RoundToInt(activePos)].next == true)
        {
            return true;
        }
        return false;
    }
    public void PlaySound()
    {
        if (isPlaying == false)
        {
            currentTime = currentTime + addTime;
            audio.time = currentTime;
            audio.Play();
            addTime = 0f;
            rows[activeRow].GetComponent<RowController>().tiles[activeTiles[rowsGone + Mathf.RoundToInt(activePos)].pos].GetComponent<MeshRenderer>().material.color = Color.grey;
            isPlaying = true;
        }
    }
    public void StopSound()
    {
        audio.Stop();
        isPlaying = false;
    }
}
