using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController_AggressionBoss : MonoBehaviour {

	public static UIController_AggressionBoss instance;

	public Text secondsLeftText;

	private string originalText;

	void Awake() {
		instance = this;
		originalText = secondsLeftText.text;
	}

	void Update() {
		secondsLeftText.text = string.Format("{0:0.00}{1}", Boss_Aggression.RemainingTime, originalText);
	}
}
