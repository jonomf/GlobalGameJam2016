using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class FollowEnemy : MonoBehaviour {

	public float speed = 5;
	public float damage = 5;

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.layer == Layers.PlayerArrowNum){
			Destroy(other.gameObject);
			Destroy(gameObject);
		}
		if (other.gameObject.layer == Layers.PlayerNum){
			Destroy(gameObject);
			Player.GetHurt(damage);
		}
	}

	void Update () {
		MoveTowardsPlayer();
	}

	private void MoveTowardsPlayer() {
		GetComponent<Rigidbody2D>().MovePosition(transform.position + (Player.Position - transform.position).normalized * speed * Time.deltaTime);
	}
}
