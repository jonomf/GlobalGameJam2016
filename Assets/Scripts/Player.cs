using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float speed;
	
	void Update () {
		if (Input.GetKey(KeyCode.UpArrow)) {
			transform.position += Vector3.up * speed * Time.deltaTime;
		}
		else if (Input.GetKey(KeyCode.DownArrow)) {
			transform.position += Vector3.down * speed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.RightArrow)) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
		else if (Input.GetKey(KeyCode.LeftArrow)) {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.layer == 8){
			Destroy(other.gameObject);
		}
	}
}
