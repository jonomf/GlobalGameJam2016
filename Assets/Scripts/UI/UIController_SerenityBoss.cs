using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController_SerenityBoss : MonoBehaviour {

	public static UIController_SerenityBoss instance;

	public Text secondsLeftText;
	public Slider bossHealthSlider;

	private string secondsLeftOriginalText;

	void Awake() {
		instance = this;
		secondsLeftOriginalText = secondsLeftText.text;
	}

	void Update() {
		secondsLeftText.text = string.Format("{0:0.00}{1}", Boss_Serenity.RemainingTime, secondsLeftOriginalText);
	}

	public static void UpdateBossHealth() {
		instance.bossHealthSlider.value = Boss_Serenity.Health;
	}
}
