using UnityEngine;
using System.Collections;

public class Child : MonoBehaviour {

	public static bool ChildHeld { get; private set; }

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.layer == Layers.PlayerNum){
			if (!ChildHeld){
				ChildHeld = true;
				Destroy(gameObject);
			}
		}
	}

	public static void ChildCollected() {
		ChildHeld = false;
	}
}
