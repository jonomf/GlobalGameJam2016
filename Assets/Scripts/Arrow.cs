using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

	public float destroyDelay = 2f;

	void Awake() {
		StartCoroutine(DestroyAfterDelay());
	}

	void OnCollisionEnter2D(Collision2D coll) {
		Destroy(gameObject);
	}

	private IEnumerator DestroyAfterDelay() {
		yield return new WaitForSeconds(destroyDelay);
		Destroy(gameObject);
	}
}
