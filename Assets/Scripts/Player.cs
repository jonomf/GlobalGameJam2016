using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	[Header("Settings")]
	public float speed;
	[Header("Audio")]
	public AudioSource collectAudio;
	[Header("References")]
	public GameObject arrowPrefab;

	private Vector3 movement;

	void Update () {
		movement = Vector3.zero;
		if (Input.GetKey(KeyCode.W)){
			movement += Vector3.up;
		}
		else if (Input.GetKey(KeyCode.S)){
			movement += Vector3.down;
		}
		if (Input.GetKey(KeyCode.D)){
			movement += Vector3.right;
		}
		else if (Input.GetKey(KeyCode.A)){
			movement += Vector3.left;
		}
		GetComponent<Rigidbody2D>().MovePosition(transform.position + movement * speed * Time.deltaTime);
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.layer == Layers.CollectableNum){
			Destroy(collision.gameObject);
			collectAudio.Play();
		}
	}
}
