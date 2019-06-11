using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour {

    public GameObject bloodStainPrefab;
    public GameObject[] tileModels;

    public List<GameObject> spawnedTileModels = new List<GameObject>();
    public List<GameObject> bloodObjects = new List<GameObject>();

    public void MakeStain()
    {
        bloodObjects.Clear();
        for (int i = 0; i < 20; i++)
        {
            GameObject newStain = Instantiate(bloodStainPrefab, gameObject.transform);
            newStain.transform.position = transform.position;
            newStain.transform.rotation = bloodStainPrefab.transform.rotation;
            bloodObjects.Add(newStain);
        }
    }
    public void SpawnTileModels()
    {
        GameObject tileModelHolder = gameObject.transform.GetChild(0).gameObject;
        foreach(GameObject go in tileModels)
        {
            GameObject newModel = Instantiate(go, tileModelHolder.transform);
            newModel.transform.position = transform.position;
            newModel.transform.rotation = gameObject.transform.rotation;
            spawnedTileModels.Add(newModel);
            newModel.SetActive(false);
        }
    }
    public void UpdateTileModels(bool isActive)
    {
        foreach(GameObject go in spawnedTileModels)
        {
            go.SetActive(false);
        }
        if (isActive)
        {
            int random = Random.Range(0, spawnedTileModels.Count);
            spawnedTileModels[random].SetActive(true);
        }
    }

    public void UpdateStain(bool isActive, bool prev, bool next)
    {
        foreach (GameObject blood in bloodObjects)
        {
            blood.SetActive(isActive);
        }
        if (next)
        {
            bloodObjects[0].transform.localScale = new Vector3(Random.Range(0.3f, 0.5f), 0.0001f, Random.Range(1.3f, 1.7f));
            bloodObjects[1].transform.localScale = new Vector3(Random.Range(0.3f, 0.5f), 0.0001f, Random.Range(1.1f, 1.5f));
            bloodObjects[1].transform.localPosition = new Vector3(Random.Range(-0.3f, 0.3f), 0f, Random.Range(-0.8f, 1.5f));
            for (int i = 2; i < bloodObjects.Count; i++)
            {
                float multiplier = i * -0.05f;
                bloodObjects[i].transform.localScale = new Vector3(Random.Range(0.7f * multiplier, 0.9f * multiplier), 0.01f, Random.Range(0.9f * multiplier, 1.5f * multiplier));
                bloodObjects[i].transform.localPosition = new Vector3(Random.Range(-0.5f, 0.5f), 0f, Random.Range(-1.5f, 1.5f));
            }
        }
        else
        {
            bloodObjects[0].transform.localScale = new Vector3(Random.Range(0.4f, 0.9f), 0.0001f, Random.Range(0.9f, 1.1f));
            bloodObjects[1].transform.localScale = new Vector3(Random.Range(0.7f, 0.9f), 0.0001f, Random.Range(0.9f, 1.1f));
            bloodObjects[1].transform.localPosition = new Vector3(Random.Range(-0.3f, 0.3f), 0f, Random.Range(-0.5f, 0.5f));
            for (int i = 2; i < bloodObjects.Count; i++)
            {
                float multiplier = i * -0.05f;
                bloodObjects[i].transform.localScale = new Vector3(Random.Range(0.7f * multiplier, 0.9f * multiplier), 0.0001f, Random.Range(0.6f * multiplier, 0.9f * multiplier));
                bloodObjects[i].transform.localPosition = new Vector3(Random.Range(-0.5f, 0.5f), 0f, Random.Range(-0.5f, 0.5f));
            }
        }

    }
}
