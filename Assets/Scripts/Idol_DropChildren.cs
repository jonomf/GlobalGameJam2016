using UnityEngine;
using System.Collections;

public class Idol_DropChildren : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.layer == Layers.PlayerNum){
			if (Child.ChildHeld){
				// TODO: "You got kid" event
				Child.ChildCollected();
			}
		}
	}
}
