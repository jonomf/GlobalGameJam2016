using UnityEngine;
using System.Collections;
using InControl;

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
	private Vector3 shootAngle;
	private InputDevice inputDevice;
	private Animator anim;

	private Vector3 Offscreen { get { return Vector3.down * 10000; } }

	void Start() {
		arrowReticle = (Instantiate(arrowReticlePrefab, Offscreen, Quaternion.identity) as GameObject).transform;
		anim = GetComponent<Animator> ();
	}

	void Update () {
		inputDevice = InputManager.ActiveDevice;
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
		// Keyboard anim + movement
		if (Input.GetKey(KeyCode.W)) {
			movement += Vector3.up;
			anim.Play("runUp");
		}
		else if (Input.GetKey(KeyCode.S)) {
			movement += Vector3.down;
			anim.Play ("runDown");
		}
		if (Input.GetKey(KeyCode.D)) {
			movement += Vector3.right;
			anim.Play ("runRight");
		}
		else if (Input.GetKey(KeyCode.A)) {
			movement += Vector3.left;
			anim.Play ("runLeft");
		}
		// Gamepad anim
		if (inputDevice.LeftStickY.Value > 0) {
			anim.Play("runUp");
		}
		else if (inputDevice.LeftStickY.Value < 0) {
			anim.Play("runDown");
		}
		if (inputDevice.LeftStickX.Value > 0) {
			anim.Play("runRight");
		}
		else if (inputDevice.LeftStickX.Value < 0) {
			anim.Play("runLeft");
		}
		// Gamepad movement
		movement += Vector3.right * inputDevice.LeftStickX.Value + Vector3.up * inputDevice.LeftStickY.Value;
		// Actually apply the movement
		GetComponent<Rigidbody2D>().MovePosition(transform.position + Vector3.ClampMagnitude(movement, 1) * speed * Time.deltaTime);
		
		if (Input.anyKey == false) {
			anim.Play("idleDown");
		}
	}
		
	private void Attack() {
		shootAngle = Vector3.up * inputDevice.RightStickY.Value + Vector3.right * inputDevice.RightStickX.Value;
		if (Input.GetKey(KeyCode.UpArrow)){
			shootAngle += Vector3.up;
		} else if (Input.GetKey(KeyCode.DownArrow)){
			shootAngle += Vector3.down;
		}
		if (Input.GetKey(KeyCode.LeftArrow)){
			shootAngle += Vector3.left;
		} else if (Input.GetKey(KeyCode.RightArrow)){
			shootAngle += Vector3.right;
		}
		arrowReticle.position = transform.position + Vector3.ClampMagnitude(shootAngle, 1) * arrowReticleDistance;

		if (Input.GetKeyDown(KeyCode.Space) || inputDevice.GetControl(InputControlType.RightTrigger).WasReleased){
			ShootArrow(shootAngle);
		}
	}

	private void ShootArrow(Vector3 direction) {
		Rigidbody2D arrowRb = (Instantiate(arrowPrefab, transform.position, Quaternion.Euler(direction)) as GameObject).GetComponent<Rigidbody2D>();
		arrowRb.AddForce(direction * arrowForce);
		arrowReticle.position = Offscreen;
	}

	private void Collect(GameObject obj) {
		Destroy(obj);
		collectAudio.Play();
	}
}
