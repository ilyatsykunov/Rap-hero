/// <summary>
/// Controller for effects of camera: 
///     creating camera shake using Unity mechanics; 
///     creating glitch effects using 3rd party shader.
/// </summary>
///
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
    //Creates smooth up and down motion
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
    //Makes camera jump to random positions within a short range
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
    //Changes intensity of camera shake by either increasing or decreasing it 
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
    //Gradually decrease camera shake 
    IEnumerator LessShake()
    {
        yield return new WaitForSeconds(0.5f);
        cameraShake -= 0.2f;
        Shake(0f);
    }
    //Starts random glitch effect and later stops it
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
