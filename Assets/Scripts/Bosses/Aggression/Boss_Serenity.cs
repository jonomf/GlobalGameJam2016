using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Boss_Serenity : MonoBehaviour {

	public static Boss_Serenity instance;

	public float surviveTime = 15f;
	public float startingHealth = 100;
	public float speed = 1;
	public float walldropDelay = 3f;

	public static float RemainingTime { get { return instance.surviveTime - Time.timeSinceLevelLoad; } }

	public static float Health { get; private set; }

	private enum State { Backup, Walldrop }
	private State state = State.Backup;

	void Start() {
		instance = this;
		Health = startingHealth;
		UIController_SerenityBoss.instance.bossHealthSlider.maxValue = Health;
		//StartCoroutine()
	}

	void Update() {
		if (RemainingTime <= 0){
			GameController.Lose();
		}
		if (state == State.Backup){
			// Always look at the player
			Quaternion rotation = Quaternion.LookRotation(Player.Position - transform.position,
				transform.TransformDirection(Vector3.up));
			transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
			GetComponent<Rigidbody2D>().MovePosition(transform.position + transform.right * speed * Time.deltaTime);
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.layer == Layers.PlayerArrowNum){
			GetHurt();
		}
	}

	private IEnumerator WalldropAfterDelay() {
		yield return new WaitForSeconds(walldropDelay);
		// TODO: Drop wall
	}

	private void GetHurt(float damage = 1f) {
		Health -= damage;
		UIController_SerenityBoss.UpdateBossHealth();
		if (Health <= 0){
			GameController.Win();
		}
	}
}
