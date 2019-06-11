using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawController : MonoBehaviour {

    public bool isActive;

    public GameObject particleObj;
    public GameObject[] meatPrefabs;

    public List<GameObject> meatObj = new List<GameObject>();
    public List<Vector3> meatVector = new List<Vector3>();

	// Use this for initialization
	void Start () {
        particleObj.GetComponent<ParticleSystem>().Stop();
    }
	
	// Update is called once per frame
	void Update () {
		if(isActive)
        {
            transform.Rotate(-30f, 0f, 0f);
        }
	}
    public void Activate() {
        bool loopVariable = particleObj.GetComponent<ParticleSystem>().main.loop;
        if (isActive)
        {
            StopCoroutine("Turn");
            StartCoroutine("Turn", 0.5f);
            loopVariable = true;
        }
        else
        {
            StartCoroutine("Turn", 0.5f);
            loopVariable = false;
            MeatExplosion();
        }
        particleObj.GetComponent<ParticleSystem>().Play();
    }
    void MeatExplosion()
    {
        if (meatObj.Count == 0)
        {
            foreach(GameObject obj in meatPrefabs)
            {
                GameObject newObj = Instantiate(obj, particleObj.transform);
                newObj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                meatObj.Add(newObj);
            }
        }
        foreach (GameObject obj in meatObj)
        {
            obj.transform.position = transform.position;
            obj.SetActive(true);
            Vector3 randPosition = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(2f, 10f), -10f);
            obj.GetComponent<MeatExplosion>().isActive = true;
            obj.GetComponent<MeatExplosion>().position = randPosition;
            //obj.transform.LookAt(randPosition);
            StartCoroutine(DeactiveObject(obj, 2f));
        }

    }
    IEnumerator DeactiveObject(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        obj.SetActive(false);
        obj.GetComponent<MeatExplosion>().isActive = false;
    }
    IEnumerator Turn(float time)
    {
        isActive = true;
        yield return new WaitForSeconds(time);
        isActive = false;
    }
}
