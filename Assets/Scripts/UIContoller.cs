using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIContoller : MonoBehaviour {

	public static UIContoller instance;

	public Slider boss1Slider;
	public Slider playerHealthSlider;
	
	void Awake() {
		instance = this;
	}

	public static void UpdatePlayerHealth() {
		instance.playerHealthSlider.value = Player.Health;
	}

	// To set slider from any script: UIContoller.instance.boss1Slider.value = <value from 0 to 100>
}
