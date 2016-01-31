using UnityEngine;
using System.Collections;

public class Relic : MonoBehaviour {

	void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.layer == Layers.PlayerNum) {
			Player.GetHurt (-10);
			Player.CheckHealth ();
			Boss_Greed.Regenerate ();
			Boss_Greed.CheckHealth();
			Boss_Greed.drainSpeed += 0.1f;
			Boss_Greed.damageBuff *= 0.95f;
		} 

		Destroy (gameObject);
	}
}
