using UnityEngine;
using System.Collections;

public class Pillar : MonoBehaviour {

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.layer == Layers.PlayerNum){
			Player.Die();
		}
	}
}
