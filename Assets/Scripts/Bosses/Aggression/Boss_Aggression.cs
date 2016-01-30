using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Boss_Aggression : MonoBehaviour {

	public static Boss_Aggression instance;

	[Header("Settings")]
	public int surviveTime = 15;
	public float speed = 1;
	public float volleyStartDelay = 1f;
	public int numShotsPerVolley = 3;
	public float delayBetweenVolleyShots = 0.3f;
	public float shotForce = 1000;
	[Header("References")]
	public GameObject bulletPrefab;

	public static float RemainingTime { get { return instance.surviveTime - Time.timeSinceLevelLoad; } }

	private Rigidbody2D pivotRb;
	private Transform pivot;

	void Start() {
		instance = this;
		pivotRb = transform.parent.GetComponent<Rigidbody2D>();
		pivot = transform.parent;
		StartCoroutine(ShootVolley());
	}

	void Update() {
		pivot.RotateAround(Vector3.forward, speed * Time.deltaTime);
		if (RemainingTime <= 0){
			SceneManager.LoadScene("Win");
		}
	}

	private IEnumerator ShootVolley() {
		yield return new WaitForSeconds(volleyStartDelay);
		for (int i = 0; i < numShotsPerVolley; i++){
			Shoot();
			yield return new WaitForSeconds(delayBetweenVolleyShots);
		}
		StartCoroutine(ShootVolley());
	}

	private void Shoot() {
		bool xNotY = Mathf.Abs(transform.position.x) > Mathf.Abs(transform.position.y);
		bool shootPositiveDir = xNotY ? transform.position.x < Player.Position.x : transform.position.y < Player.Position.y;
		Rigidbody2D rb = (Instantiate(bulletPrefab, transform.position, Quaternion.Euler((xNotY ? Vector3.right : Vector3.up) * (shootPositiveDir ? 1 : -1))) as GameObject).GetComponent<Rigidbody2D>();
		rb.AddForce((xNotY ? Vector2.right : Vector2.up) * (shootPositiveDir ? 1 : -1) * shotForce);
	}
}
