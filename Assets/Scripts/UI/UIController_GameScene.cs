using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController_GameScene : MonoBehaviour {

	public static UIController_GameScene instance;

	public Text secondsLeftText;
    public Slider boss1Slider;
	public Slider boss2Slider;
	public Slider boss3Slider;

    public float numSeconds = 30;
    public static float RemainingGameTime { get { return instance.numSeconds - Time.timeSinceLevelLoad; } }

	void Awake() {
		instance = this;
	}

    void Update() {
        secondsLeftText.text = string.Format("{0:00} seconds left!", RemainingGameTime);
        if (RemainingGameTime <= 0){
            GameController.Transition();
        }
    }
}
