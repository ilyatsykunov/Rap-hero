/// <summary>
/// General controller for running the game, it is responsible for: 
///     Initial world generation
///     Moving tiles
///     Checking user input 
///     Playing sound
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class WorldController : MonoBehaviour {

    [SerializeField]
    private float top;
    [SerializeField]
    private float bottom;
    public float activePos;
    public float speed;
    public GameObject emptyObj;
    public GameObject connection;
    public GameObject fullSprite;
    public GameObject[] sawObjects;

    [SerializeField]
    private int songLength;

    public List<Tile> activeTiles;
    private int activeTile;
    private bool activatedTile;
    public List<Tile> activeTiles2;
    private int activeTile2;
    private bool activatedTile2;

    private bool miss;

    [SerializeField]
    private int rowsGone;
    public bool doubleKey;

    public GameObject[] rows;
    [HideInInspector]
    public int activeRow;

    public bool isGameActive;

    public float totalAudioLength;
    public float audioSourceLength;

    //Audio
    [HideInInspector]
    public bool failedPlay;
    [HideInInspector]
    public bool isPlaying;
    [HideInInspector]
    public AudioSource audioSource;
    private float currentTime;
    private float addTime;

    public CameraController cc;
    public PlayerController pc;
    public VideoController vc;
    public UIController uic;

    // Use this for initialization
    void Start() {
        GameStart();
    }

    // Update is called once per frame
    void Update() {
        if (isGameActive)
        {
            Play();
        }
    }
    //Initial world generation, also used to restart the game
    void GameStart()
    {
        activeTiles = new List<Tile>();
        activeTiles2 = new List<Tile>();
        PopulateMap();
        for (int i = 0; i < rows.Length; i++)
        {
            rows[i].GetComponent<RowController>().Disable();
            rows[i].GetComponent<RowController>().Spawn();
        }
        vc.Switch(false);
        foreach(GameObject saw in sawObjects)
        {
            saw.GetComponent<SawController>().isActive = true;
        }
    }
    //Transition between main menu and playing the game
    public void ActivateGame()
    {
        isGameActive = true;
        vc.Switch(true);
        foreach (GameObject saw in sawObjects)
        {
            saw.GetComponent<SawController>().isActive = false;
        }
    }
    //Stops the game and opens main menu
    public void DeactivateGame()
    {
        if (isGameActive)
        {
            isGameActive = false;
            vc.Switch(false);
            StopSound();
        }
    }
    //Main game loop
    void Play()
    {
        if (activeTiles.Count > 0)
        {
            MoveTiles();
            if (rowsGone >= -Mathf.RoundToInt(activePos))
            {
                if (isPlaying == true)
                {
                    addTime += Time.deltaTime;
                }
                activeTile = activeTiles[rowsGone + Mathf.RoundToInt(activePos)].pos;
                activeTile2 = activeTiles2[rowsGone + Mathf.RoundToInt(activePos)].pos;
                if (activeTile == activeTile2)
                {
                    if(!activatedTile && !activatedTile2)
                    {
                        if (CheckInput(activeTiles))
                        {
                            KeyHit(activeTiles);
                            activatedTile = true;
                            activatedTile2 = true;
                        }
                    }

                }
                else
                {
                    if (!activatedTile)
                    {
                        activatedTile = CheckInput(activeTiles);
                    }
                    if (!activatedTile2)
                    {
                        activatedTile2 = CheckInput(activeTiles2);
                    }
                    if (activatedTile && activatedTile2)
                    {
                        KeyHit(activeTiles);
                    }
                }
            }
        }
    }
    //Check user input against the required input
    bool CheckInput(List<Tile> tiles)
    {
        int currentActiveTile = tiles[rowsGone + Mathf.RoundToInt(activePos)].pos;
        if (currentActiveTile == 0)
        {
            if (IsHold(tiles) == true)
            {
                if (Input.GetKeyUp(KeyCode.Alpha1))
                {
                    StopSound();
                }
                if (Input.GetKey(KeyCode.Alpha1))
                {
                    return true;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    return true;
                }
            }
        }
        if (currentActiveTile == 1)
        {
            if (IsHold(tiles) == true)
            {
                if (Input.GetKeyUp(KeyCode.Alpha2))
                {
                    StopSound();
                }
                if (Input.GetKey(KeyCode.Alpha2))
                {
                    return true;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    return true;
                }
            }
        }
        if (currentActiveTile == 2)
        {
            if (IsHold(tiles) == true)
            {
                if (Input.GetKeyUp(KeyCode.Alpha3))
                {
                    StopSound();
                }
                if (Input.GetKey(KeyCode.Alpha3))
                {
                    return true;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    return true;
                }

            }
        }
        if (currentActiveTile == 3)
        {
            if (IsHold(tiles) == true)
            {
                if (Input.GetKeyUp(KeyCode.Alpha4))
                {
                    StopSound();
                }
                if (Input.GetKey(KeyCode.Alpha4))
                {
                    return true;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    return true;
                }
            }
        }
        if (currentActiveTile == 4)
        {
            if (IsHold(tiles) == true)
            {
                if (Input.GetKeyUp(KeyCode.Alpha5))
                {
                    StopSound();
                }
                if (Input.GetKey(KeyCode.Alpha5))
                {
                    return true;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Alpha5))
                {
                    return true;
                }
            }
        }
        return false;
    }
    //Check whether user pressed incorrect key
    bool CheckMiss(List<Tile> tiles)
    {
        return false;
    }
    //Loop movement of tiles
    void MoveTiles()
    {
        for(int i = 0; i < rows.Length; i++)
        {
            if (rows[i].transform.position.z <= activePos && rows[i].transform.position.z > activePos - 0.3f)
            {
                SetActiveRow(i);
            }
            if (rows[i].transform.position.z <= bottom)
            {
                ResetRow(i);
            }
        }
        totalAudioLength = currentTime + addTime;
        audioSourceLength = audioSource.clip.length;
        if (currentTime + addTime >= audioSource.clip.length - 1.5f)
        {
            DeactivateGame();
            GameStart();
            currentTime = 0f;
            addTime = 0f;
            uic.EndGame();
        }
        if(rowsGone >= activeTiles.Count - 10)
        {
            PopulateMap();
        }
    }
    //Create a map of positions of all tiles in the game
    void PopulateMap()
    {
        int rowLength = rows[0].GetComponent<RowController>().tiles.Length;
        audioSource = gameObject.GetComponent<AudioSource>();
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
            int pos2 = Random.Range(0, rowLength);
            int chance = Random.Range(0, 2);
            if(chance == 0)
            {
                pos2 = pos;
            }
            Tile tileObj2 = new Tile(pos2);
            if (i > 0 && activeTiles2[i - 1].pos == pos2)
            {
                activeTiles2[i - 1].next = true;
                tileObj2.prev = true;
            }
            activeTiles2.Add(tileObj2);
        }

    }
    //Move row of tiles to the end of the screen
    void ResetRow(int row)
    {
        rowsGone++;
        rows[row].GetComponent<RowController>().ClearTiles();
        rows[row].GetComponent<RowController>().SetTiles(activeTiles[rowsGone].pos, activeTiles[rowsGone].prev, activeTiles[rowsGone].next);
        rows[row].GetComponent<RowController>().SetTiles(activeTiles2[rowsGone].pos, activeTiles2[rowsGone].prev, activeTiles2[rowsGone].next);
        rows[row].transform.position = new Vector3(rows[row].transform.position.x, rows[row].transform.position.y, top);
        activatedTile = false;
        activatedTile2 = false;
        StopSound();
    }
    void SetActiveRow(int newActive)
    {
        activeRow = newActive;
    }
    //Tells if user is user required to hold a key
    bool IsHold(List<Tile> list)
    {
        if (list[rowsGone + Mathf.RoundToInt(activePos)].prev == true || list[rowsGone + Mathf.RoundToInt(activePos)].next == true)
        {
            return true;
        }
        return false;
    }
    //User pressed correct key 
    void KeyHit(List<Tile> list)
    {
        float distance = Mathf.Abs(rows[activeRow].transform.position.z + activePos);
        if (!IsHold(list))
        {
            pc.AddScore(distance);
        }
        if (activeTile == activeTile2)
        {
            sawObjects[activeTile].GetComponent<SawController>().Activate();
        }
        else
        {
            if (activatedTile)
            {
                sawObjects[activeTile].GetComponent<SawController>().Activate();
            }
            if (activatedTile2)
            {
                sawObjects[activeTile2].GetComponent<SawController>().Activate();
            }
        }
        PlaySound(list);
        float time = Random.Range(0f, 0.5f);
        rows[activeRow].GetComponent<RowController>().DestroyOnHit();
    }
    //Set selected by the user song as currently played
    public void AssignAudio(AudioClip newClip)
    {
        StopSound();
        audioSource.clip = newClip;
        audioSource.pitch = 1f;
        audioSource.time = 0f;
        currentTime = 0f;
        addTime = 0f;
        PopulateMap();
    }
    //Play a piece of the song with randomly selected pitch
    public void PlaySound(List<Tile> list)
    {
        if (IsHold(list) == false)
        {
            int random = Random.Range(0, 10);
            if (random == 0)
            {
                RandomPitch();
            }
            else if (random >= 8)
            {
                audioSource.pitch = 1f;
            }
        }
        if (isPlaying == false)
        {
            currentTime = currentTime + addTime;
            audioSource.time = currentTime;
            audioSource.Play();
            addTime = 0f;
            isPlaying = true;
        }
        if(isPlaying == true)
        {
            StopCoroutine("TurnVolumeDown");
            audioSource.volume = 1f;
        }
        cc.Shake(0.5f);
        StartCoroutine(cc.MakeShake(0.05f, 0.05f));
        rows[activeRow].GetComponent<RowController>().tiles[activeTiles[rowsGone + Mathf.RoundToInt(activePos)].pos].GetComponent<MeshRenderer>().material.color = Color.grey;
    }
    //Gradually decrease volume of the song and then stop playing it
    public void StopSound()
    {
        if(audioSource.volume > 0.3f)
        {
            StartCoroutine("TurnVolumeDown");
        }
        else
        {
            audioSource.Stop();
            isPlaying = false;
        }
    }
    //Decrease volume every 0.1 of a second
    IEnumerator TurnVolumeDown()
    {
        yield return new WaitForSeconds(0.1f);
        audioSource.volume -= 0.2f;
        StopSound();
    }
    //Sets the song to play in reverse if user presses wrong key
    void FailPitch()
    {
        audioSource.pitch = -1.0f;
        audioSource.Play();
        addTime = 0f;
        isPlaying = true;
    }
    //Creates random audio and video effects
    void RandomPitch()
    {
        float random = Random.Range(0.6f, 0.9f);
        cc.StartCoroutine("LessGlitch", random);
        vc.SlowVideo(random);
        audioSource.pitch = random;
    }
}
