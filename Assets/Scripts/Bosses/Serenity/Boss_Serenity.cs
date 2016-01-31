using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Boss_Serenity : MonoBehaviour {

	public static Boss_Serenity instance;

	public float surviveTime = 15f;
	public float startingHealth = 100;
	public float speed = 1;
	public float maxDistance = 15;
	public float resumeAgainDistance = 6;
	[Header("Little drops")]
	public GameObject littleDropPrefab;
	public float littleDropPeriod = 0.75f;
	public float littleDropLateralRange = 2f;
	[Header("Wall drop")]
	public GameObject wallPrefab;
	public float walldropDelay = 3f;
	public float wallDropPause = 1.5f;
	public float wallDropFastBackupLength = 1.5f;
	public float wallDropFastBackupSpeed = 12f;

	public static float RemainingTime { get { return instance.surviveTime - Time.timeSinceLevelLoad; } }
	public static Vector3 Position { get { return instance.transform.position; } }

	public static float Health { get; private set; }

	private enum State { Backup, Walldrop, FastBackup, Waiting }
	private State state = State.Backup;

	void Start() {
		instance = this;
		Health = startingHealth;
		UIController_SerenityBoss.instance.bossHealthSlider.maxValue = Health;
		StartCoroutine(WalldropAfterDelay());
		StartCoroutine(LittleDropDuringBackup());
	}

	void Update() {
		if (RemainingTime <= 0){
			GameController.Lose();
		}
		if ((Player.Position - transform.position).magnitude > maxDistance){
			state = State.Waiting;
			return;
		}
		switch (state){
			case State.Waiting:
				if ((Player.Position - transform.position).magnitude < resumeAgainDistance) {
					state = State.Backup;
				}
				break;
			case State.Backup:
				// Always look at the player
				Quaternion rotation = Quaternion.LookRotation(Player.Position - transform.position,
					transform.TransformDirection(Vector3.up));
				transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
				GetComponent<Rigidbody2D>().MovePosition(transform.position + transform.right * speed * Time.deltaTime);
				break;
			case State.FastBackup:
				GetComponent<Rigidbody2D>().MovePosition(transform.position + transform.right * wallDropFastBackupSpeed * Time.deltaTime);
				break;
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.layer == Layers.PlayerArrowNum){
			GetHurt();
		}
	}

	private IEnumerator WalldropAfterDelay() {
		while (true) {
			yield return new WaitForSeconds(walldropDelay);
			state = State.Walldrop;
			yield return new WaitForSeconds(wallDropPause);
			Instantiate(wallPrefab, transform.position + transform.right * -2, transform.rotation * Quaternion.Euler(Vector3.forward * 90));
			state = State.FastBackup;
			yield return new WaitForSeconds(wallDropFastBackupLength);
			state = State.Backup;
		}
	}

	private IEnumerator LittleDropDuringBackup() {
		while (true){
			yield return new WaitForSeconds(littleDropPeriod);
			if (state == State.Backup){
				Instantiate(littleDropPrefab,
					transform.position + transform.right * -2 +
					Vector3.up * Random.Range(-littleDropLateralRange, littleDropLateralRange),
					transform.rotation * Quaternion.Euler(Vector3.forward * 90));
			}
		}
	}

	private void GetHurt(float damage = 1f) {
		if (state == State.Waiting){
			state = State.Backup;
		}
		Health -= damage;
		UIController_SerenityBoss.UpdateBossHealth();
		if (Health <= 0){
			GameController.Win();
		}
	}
}
