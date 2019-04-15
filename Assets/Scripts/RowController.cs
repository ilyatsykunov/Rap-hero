using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowController : MonoBehaviour {

    public WorldController wc;
    public GameObject[] tiles;
    public List<GameObject> connections;
    public bool enabled;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.back * Time.deltaTime * wc.speed);
    }
    public void Disable()
    {
        foreach (GameObject go in tiles)
        {
            enabled = false;
            go.GetComponent<MeshRenderer>().enabled = false;
        }
    }
    public void Enable()
    {
        if(enabled == false)
        {
            foreach (GameObject go in tiles)
            {
                go.GetComponent<MeshRenderer>().enabled = true;
            }
            enabled = true;
        }

    }
    public void SetTiles(int tile, bool prev, bool next)
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i].GetComponent<MeshRenderer>().material.color = Color.white;
            tiles[i].GetComponent<MeshRenderer>().enabled = true;
            connections[i].GetComponent<MeshRenderer>().enabled = false;
        }
        tiles[tile].GetComponent<MeshRenderer>().material.color = Color.black;
        if (next == true)
        {
            connections[tile].GetComponent<MeshRenderer>().enabled = true;
        }
        if(prev == true && next == true)
        {
            tiles[tile].GetComponent<MeshRenderer>().enabled = false;
        }
    }
    public void Spawn()
    {
        for(int i = 0; i < tiles.Length; i++)
        {
            var newConnection = Instantiate(wc.connection, new Vector3(tiles[i].transform.position.x, tiles[i].transform.position.y, tiles[i].transform.position.z + 0.5f), Quaternion.identity, gameObject.transform);
            connections.Add(newConnection);
            newConnection.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
