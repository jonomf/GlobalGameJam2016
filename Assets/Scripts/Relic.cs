using UnityEngine;
using System.Collections;

public class Relic : MonoBehaviour {

	void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.layer == Layers.PlayerNum) {
			Player.GetHurt (-50);
			Boss_Greed.Regenerate ();
		} else {
			Destroy (gameObject);
		}
	}
}
