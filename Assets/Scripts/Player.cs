using UnityEngine;
using System.Collections;
using InControl;

public class Player : MonoBehaviour {

	public static Player instance;

	[Header("Settings")]
	[SerializeField] private float startingHealth = 100;
	public float speed;
	public float arrowForce = 1000;
	public float arrowReticleDistance = 2f;
	[Header("Audio")]
	public AudioSource collectAudio;
	[Header("References")]
	public GameObject arrowPrefab;
	public GameObject arrowReticlePrefab;

	public static Vector3 Position { get { return instance.transform.position; } }

	public static float Health { get; private set; }

	private Transform arrowReticle;
	private Vector3 movement;
	private Vector3 shootAngle;
	private InputDevice inputDevice;
	private Animator anim;
	private string direction = "down";
	private bool hasChild;

	private Vector3 Offscreen { get { return Vector3.down * 10000; } }

	void Start() {
		instance = this;
		arrowReticle = (Instantiate(arrowReticlePrefab, Offscreen, Quaternion.identity) as GameObject).transform;
		anim = GetComponent<Animator> ();
		Health = startingHealth;
		UIContoller.UpdatePlayerHealth();
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
			if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("runLeft") && !anim.GetCurrentAnimatorStateInfo (0).IsName ("runRight")) {
				anim.Play ("runUp");
			}
			direction = "up";
		}
		else if (Input.GetKey(KeyCode.S)) {
			movement += Vector3.down;
			if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("runLeft") && !anim.GetCurrentAnimatorStateInfo (0).IsName ("runRight")) {
				anim.Play ("runDown");
			}
			direction = "down";
		}
		if (Input.GetKey(KeyCode.D)) {
			movement += Vector3.right;
			if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("runUp") && !anim.GetCurrentAnimatorStateInfo (0).IsName ("runDown")) {
				anim.Play ("runRight");
			}
			direction = "right";
		}
		else if (Input.GetKey(KeyCode.A)) {
			movement += Vector3.left;
			if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("runUp") && !anim.GetCurrentAnimatorStateInfo (0).IsName ("runDown")) {
				anim.Play ("runLeft");
			}
			direction = "left";
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
		
		if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S)) {
			if (direction == "down") {
				anim.Play ("idleDown");
			}
			else if (direction == "up") {
				anim.Play ("idleUp");
			}
			else if (direction == "right") {
				anim.Play ("idleRight");
			}
			else if (direction == "left") {
				anim.Play ("idleLeft");
			} 
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

	public static void GetHurt(float damage = 5f) {
		Health -= damage;
		UIContoller.UpdatePlayerHealth();
		if (Health <= 0){
			Die();
		}
	}

	private static void Die() {
		Destroy(instance.gameObject);
		Debug.Log("You be dead!");
	}
}
