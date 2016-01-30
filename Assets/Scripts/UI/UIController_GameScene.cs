using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController_GameScene : UIContoller {

	public new static UIController_GameScene instance;

	public Slider boss1Slider;
	public Slider boss2Slider;
	public Slider boss3Slider;

	void Awake() {
		instance = this;
	}
}
