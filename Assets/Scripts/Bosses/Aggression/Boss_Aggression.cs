using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Boss_Aggression : MonoBehaviour {

	public static Boss_Aggression instance;

	[Header("Settings")]
	public int surviveTime = 15;
	public int surviveTimeAddedPerHit = 1;
	public float[] speed;
	public float[] volleyStartDelay;
	public int[] numShotsPerVolley;
	public float[] delayBetweenVolleyShots;
	public float[] shotForce;
	public float[] stageStartTime;
	[Header("References")]
	public GameObject bulletPrefab;

	public static float RemainingTime { get { return instance.surviveTime - Time.timeSinceLevelLoad; } }

	private int stage = 0;
	private Transform pivot;

	void Start() {
		instance = this;
		pivot = transform.parent;
		StartCoroutine(ShootVolley());
	}

	void Update() {
		pivot.RotateAround(Vector3.forward, speed[stage] * Time.deltaTime);
		for(int s = 0; s < stageStartTime.Length; s++) {
			if (RemainingTime <= stageStartTime[s] && stage == s){
				stage = s + 1;
			}
		}
		if (RemainingTime <= 0){
			GameController.Win();
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.layer == Layers.PlayerArrowNum){
			surviveTime += surviveTimeAddedPerHit;
			Destroy(other.gameObject);
		}
	}

	private IEnumerator ShootVolley() {
		yield return new WaitForSeconds(volleyStartDelay[stage]);
		for (int i = 0; i < numShotsPerVolley[stage]; i++){
			Shoot();
			yield return new WaitForSeconds(delayBetweenVolleyShots[stage]);
		}
		StartCoroutine(ShootVolley());
	}

	private void Shoot() {
		bool xNotY = Mathf.Abs(transform.position.x) > Mathf.Abs(transform.position.y);
		bool shootPositiveDir = xNotY ? transform.position.x < Player.Position.x : transform.position.y < Player.Position.y;

		Rigidbody2D rb =
			(Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject).GetComponent<Rigidbody2D>();
		rb.AddForce((xNotY ? Vector2.right : Vector2.up) * (shootPositiveDir ? 1 : -1) * shotForce[stage]);

		if (stage >= 1) {
			rb =
				(Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject).GetComponent<Rigidbody2D>();
			rb.AddForce((xNotY ? Vector2.right : Vector2.up) * (shootPositiveDir ? 1 : -1) * shotForce[stage]);
			rb.AddForce((xNotY ? Vector2.up : Vector2.right) * 250);
		}
		if (stage >= 2) {
			rb =
				(Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject).GetComponent<Rigidbody2D>();
			rb.AddForce((xNotY ? Vector2.right : Vector2.up) * (shootPositiveDir ? 1 : -1) * shotForce[stage]);
			rb.AddForce((xNotY ? Vector2.up : Vector2.right) * -250);
		}
	}
}
