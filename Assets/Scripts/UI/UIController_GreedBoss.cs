using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController_GreedBoss : MonoBehaviour {

	public static UIController_GreedBoss instance;

	public Slider bossHealthSlider;

	void Awake() {
		instance = this;
	}



	// Update is called once per frame
	void Update () {
	
	}

	public static void UpdateBossHealth () {
		instance.bossHealthSlider.value = Boss_Greed.Health;
	}
}
