using UnityEngine;
using System.Collections;

public class GroundTileTrigger : MonoBehaviour {

	public Vector2 direction;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.layer == Layers.PlayerNum){
			Instantiate(transform.parent.gameObject,
				(Vector2) transform.position + direction * (transform.parent.GetComponent<Collider>().bounds.size.x / 2),
				Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
