using UnityEngine;
using System.Collections;
using UnityEditor.SceneManagement;

public class Boss_Greed : MonoBehaviour {

	public static Boss_Greed instance;
	[Header("Settings")]
	public float startingHealth = 500;
	public float drainSpeed = 0.1f;

	public static float Health { get; private set; }

	void Start () {
		instance = this;
		Health = startingHealth;
	}
	
	// Update is called once per frame
	void Update () {
		Player.GetHurt (drainSpeed);
	}

	public static void Regenerate(float regen = 100) {
		Health += regen;
		UIController_GreedBoss.UpdateBossHealth ();
	}
		
	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.layer == Layers.PlayerArrowNum){
			Destroy(other.gameObject);
			GetHurt ();
		}
	}

	public static void GetHurt (float damage = 50f) {
		Health -= damage;
		UIController_GreedBoss.UpdateBossHealth ();
		if (Health <= 0){
			GameController.Win();
		}
	}

}
