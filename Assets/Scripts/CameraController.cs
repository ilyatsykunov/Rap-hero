using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float cameraShake;

    public float yMax;
    private bool right;
    private float yOrig;
    private float yCur;

    public GlitchEffect ge;

    // Use this for initialization
    void Start () {
        right = true;
        yOrig = gameObject.transform.parent.position.y;
    }
	
	// Update is called once per frame
	void Update () {
        yCur = gameObject.transform.parent.position.y;
        UpDown();
    }
    void UpDown()
    {
        if (right == true)
        {
            gameObject.transform.parent.Translate(Vector3.up * Time.deltaTime * cameraShake);
        }
        if (yCur >= yOrig + yMax)
        {
            right = false;
        }
        if (right == false)
        {
            gameObject.transform.parent.Translate(Vector3.down * Time.deltaTime * cameraShake);
        }
        if (yCur <= yOrig - yMax)
        {
            right = true;
        }
    }
    public IEnumerator MakeShake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0f;
        while(elapsed < duration)
        {
            float x = Random.Range(-magnitude, magnitude);
            float y = Random.Range(-magnitude, magnitude);

            transform.localPosition = new Vector3(x, y, originalPos.z);
            elapsed += Time.deltaTime;

            yield return null;

        }
        transform.localPosition = originalPos;
    }
    public void Shake(float newShake)
    {
        cameraShake += newShake;
        
        if (cameraShake > 0.2f)
        {
            StartCoroutine("LessShake");
        }
        if(cameraShake < 0.2f)
        {
            cameraShake = 0.2f;
        }
        if(cameraShake > 3f)
        {
            cameraShake = 3f;
        }
    }
    IEnumerator LessShake()
    {
        yield return new WaitForSeconds(0.5f);
        cameraShake -= 0.2f;
        Shake(0f);
    }
    public IEnumerator LessGlitch(float random)
    {
        int parameter = Random.Range(0, 4);
        if(parameter == 0)
        {
            ge.intensity = random;
        }
        else if(parameter == 1)
        {
            ge.flipIntensity = random;
        }
        else
        {
            ge.colorIntensity = random;
        }
        float time = Random.Range(1f, 5f);
        yield return new WaitForSeconds(time);
        ge.intensity = 0f;
        ge.flipIntensity = 0f;
        ge.colorIntensity = 0f;
    }
}
