using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIContoller : MonoBehaviour {

	public static UIContoller instance;

	public Slider boss1Slider;
    public Slider boss2Slider;
    public Slider boss3Slider;
	
	void Start () {
		instance = this;
	}

	// To set slider from any script: UIContoller.instance.boss1Slider.value = <value from 0 to 100>
}
