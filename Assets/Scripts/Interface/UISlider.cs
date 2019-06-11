using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISlider : MonoBehaviour {

    public Slider slider;
    float currentValue;

	void Update () {
		if(slider.value != currentValue)
        {
            AudioListener.volume = slider.value;
            currentValue = slider.value;
        }
	}
}
