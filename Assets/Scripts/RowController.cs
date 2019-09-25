/// <summary>
/// Controlls everything related to rows of tiles - movement of tiles and setting active tiles
/// </summary>
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowController : MonoBehaviour {

    public WorldController wc;
    public GameObject[] tiles;
    public bool enabled;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (wc.isGameActive)
        {
            transform.Translate(Vector3.back * Time.deltaTime * wc.speed);
        }
    }
    //Unactivate each tile in a row
    public void Disable()
    {
        foreach (GameObject go in tiles)
        {
            enabled = false;
            go.SetActive(false);
        }
    }
    //Disable each tile in a row if user presses correct key
    public void DestroyOnHit()
    {
        foreach (GameObject go in tiles)
        {
            float time = Random.Range(0f, 0.5f);
            StartCoroutine(DestroyCoroutine(go, time));
        }
    }
    //Disable a tile after time specified 
    IEnumerator DestroyCoroutine(GameObject go, float time)
    {
        yield return new WaitForSeconds(time);
        enabled = false;
        go.transform.GetChild(0).gameObject.SetActive(false);
    }
    //Enable each tile in a row
    public void Enable()
    {
        if(enabled == false)
        {
            foreach (GameObject go in tiles)
            {
                go.SetActive(true);
            }
            enabled = true;
        }

    }
    //Disable all tiles and the 3D models assigned to each tile
    public void ClearTiles()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i].transform.GetChild(0).gameObject.SetActive(false);
            tiles[i].SetActive(false);
        }
    }
    //Enable a tile and its 3D model according to its parameters
    public void SetTiles(int tile, bool prev, bool next)
    {
        tiles[tile].SetActive(true);
        tiles[tile].transform.GetChild(0).gameObject.SetActive(true);
        Quaternion newRot = new Quaternion(Random.Range(-50f, 50f), Random.Range(-150f, 150f), Random.Range(-50f, 50), 0f);
        tiles[tile].transform.GetChild(0).transform.rotation = newRot;
        tiles[tile].GetComponent<TileController>().UpdateStain(true, prev, next);
        tiles[tile].GetComponent<TileController>().UpdateTileModels(true);
        if (next == true)
        {
            tiles[tile].transform.GetChild(0).gameObject.SetActive(true);
        }
        if(prev == true && next == true)
        {
            tiles[tile].transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    //Initial spawn of all models for each tile
    public void Spawn()
    {
        for(int i = 0; i < tiles.Length; i++)
        {
            GameObject newEmpty = Instantiate(wc.emptyObj, tiles[i].transform);
            tiles[i].GetComponent<TileController>().MakeStain();
            tiles[i].GetComponent<TileController>().SpawnTileModels();
        }
    }
}
