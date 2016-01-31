using UnityEngine;
using System.Collections;

public class Relic : MonoBehaviour {

	void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.layer == Layers.PlayerNum) {
			Destroy (gameObject);
			Player.AddHealth (10);
			Player.CheckHealth ();
			Boss_Greed.Regenerate ();
			Boss_Greed.CheckHealth ();
			Boss_Greed.instance.drainSpeed += Boss_Greed.instance.drainIncrease;
			Boss_Greed.instance.damage *= Boss_Greed.instance.damRes;
			FeedbackPopup.DoPopup("-HP", Color.red, 0);
		} 

		else {
			Destroy (other.gameObject);
		}


	}
}
