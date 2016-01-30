using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	[Header("Settings")]
	public float speed;
	public float arrowForce = 1000;
	public float arrowReticleDistance = 2f;
	[Header("Audio")]
	public AudioSource collectAudio;
	[Header("References")]
	public GameObject arrowPrefab;
	public GameObject arrowReticlePrefab;

	private Transform arrowReticle;
	private Vector3 movement;

	private Vector3 Offscreen { get { return Vector3.down * 10000; } }

	void Start() {
		arrowReticle = (Instantiate(arrowReticlePrefab, Offscreen, Quaternion.identity) as GameObject).transform;
	}

	void Update () {
		Move();
		Attack();
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.layer == Layers.CollectableNum) {
			Collect(collision.gameObject);
		}
	}

	private void Move() {
		movement = Vector3.zero;
		if (Input.GetKey(KeyCode.W)) {
			movement += Vector3.up;
		}
		else if (Input.GetKey(KeyCode.S)) {
			movement += Vector3.down;
		}
		if (Input.GetKey(KeyCode.D)) {
			movement += Vector3.right;
		}
		else if (Input.GetKey(KeyCode.A)) {
			movement += Vector3.left;
		}
		GetComponent<Rigidbody2D>().MovePosition(transform.position + movement * speed * Time.deltaTime);
	}

	private void Attack() {
		if (Input.GetKey(KeyCode.UpArrow)){
			arrowReticle.position = transform.position + Vector3.up * arrowReticleDistance;
		} else if (Input.GetKey(KeyCode.DownArrow)){
			arrowReticle.position = transform.position + Vector3.down * arrowReticleDistance;
		}
		if (Input.GetKey(KeyCode.LeftArrow)){
			arrowReticle.position = transform.position + Vector3.left * arrowReticleDistance;
		} else if (Input.GetKey(KeyCode.RightArrow)){
			arrowReticle.position = transform.position + Vector3.right * arrowReticleDistance;
		}

		if (Input.GetKeyUp(KeyCode.UpArrow)){
			ShootArrow(Vector3.up);
		} else if (Input.GetKeyUp(KeyCode.DownArrow)){
			ShootArrow(Vector3.down);
		}
		if (Input.GetKeyUp(KeyCode.LeftArrow)){
			ShootArrow(Vector3.left);
		} else if (Input.GetKeyUp(KeyCode.RightArrow)){
			ShootArrow(Vector3.right);
		}
	}

	private void ShootArrow(Vector3 direction) {
		Rigidbody2D arrowRb = (Instantiate(arrowPrefab, transform.position + direction, Quaternion.Euler(direction)) as GameObject).GetComponent<Rigidbody2D>();
		arrowRb.AddForce(direction * arrowForce);
		arrowReticle.position = Offscreen;
	}

	private void Collect(GameObject obj) {
		Destroy(obj);
		collectAudio.Play();
	}
}
