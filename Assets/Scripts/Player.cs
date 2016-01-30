using UnityEngine;
using System.Collections;
using InControl;
using UnityEngine.SceneManagement;

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
		if(GodManager.checkEndGame()){
            Die();
        }
        inputDevice = InputManager.ActiveDevice;
		Move();
		Attack();
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.layer == Layers.CollectableNum) {
			Collect(collision.gameObject);
		}
		if (collision.gameObject.layer == Layers.BossBulletNum){
			gettingKnockedBack = true;
			GetComponent<Rigidbody2D>().velocity = collision.gameObject.GetComponent<Rigidbody2D>().velocity * 0.25f;
			StartCoroutine(GetKnockedBack());
			Destroy(collision.gameObject);
			GetHurt();
		}
	}

	private bool gettingKnockedBack;
	private IEnumerator GetKnockedBack() {
		while (GetComponent<Rigidbody2D>().velocity.magnitude > 2){
			yield return null;
		}
		gettingKnockedBack = false;
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

		if (Mathf.Abs (inputDevice.LeftStickY.Value) > Mathf.Abs (inputDevice.LeftStickX.Value)) {
			if (inputDevice.LeftStickY.Value > 0) {
				anim.Play ("runUp");
				direction = "up";
			} 
			else if (inputDevice.LeftStickY < 0) {
				anim.Play ("runDown");
				direction = "down";
			}
		} 
		else if (Mathf.Abs (inputDevice.LeftStickX.Value) > Mathf.Abs (inputDevice.LeftStickY.Value)) {
			if (inputDevice.LeftStickX.Value > 0) {
				anim.Play ("runRight");
				direction = "right";
			}
			else if (inputDevice.LeftStickX.Value < 0) {
				anim.Play ("runLeft");
				direction = "left";
			}
		}



		// Gamepad movement
		movement += Vector3.right * inputDevice.LeftStickX.Value + Vector3.up * inputDevice.LeftStickY.Value;
		// Actually apply the movement
		if (!gettingKnockedBack){
			GetComponent<Rigidbody2D>().MovePosition(transform.position + Vector3.ClampMagnitude(movement, 1) * speed * Time.deltaTime);
		}

		if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || inputDevice.GetControl(InputControlType.LeftStickX).WasReleased ||Input.GetKeyUp(KeyCode.S) || inputDevice.GetControl(InputControlType.LeftStickY).WasReleased ) {
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

		if (shootAngle != Vector3.zero && (Input.GetKeyDown(KeyCode.Space) || inputDevice.GetControl(InputControlType.RightTrigger).WasReleased)){
			ShootArrow(shootAngle);
		}
	}

	private void ShootArrow(Vector3 direction) {
		Rigidbody2D arrowRb = (Instantiate(arrowPrefab, transform.position, Quaternion.Euler(direction)) as GameObject).GetComponent<Rigidbody2D>();
		arrowRb.AddForce(direction * arrowForce);
		arrowReticle.position = Offscreen;

        //GodManager.updateBars(1,-1,0);
	}

	private void Collect(GameObject obj) {
		Destroy(obj);
		collectAudio.Play();
        GodManager.updateBars(0,0,3);
	}

	public static void GetHurt(float damage = 5f) {
		Health -= damage;
		UIContoller.UpdatePlayerHealth();
        GodManager.updateBars(0,1,0);
		if (Health <= 0){
			Die();
		}
	}

	public static void Die() {
		SceneManager.LoadScene("Lose");
	}
}
