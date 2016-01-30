using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

	string direction = "";
	float speed = 20;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (direction == "up") {
			transform.position += Vector3.up * speed * Time.deltaTime;
		}
		else if (direction == "down") {
			transform.position += Vector3.down * speed * Time.deltaTime;
		}
		if (direction == "right") {
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
		else if (direction == "left") {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.layer == 8) {
			Destroy (other.gameObject);
		}
	}
}
