using UnityEngine;
using System.Collections;

public class Collectible : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == Layers.PlayerNum){
            Player.Collect(gameObject);
        }
    }
}
