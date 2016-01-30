using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class FollowEnemy : MonoBehaviour {

	public float speed = 5;
	public float damage = 5;

	Animator anim;

	private Vector3 direction;

	float angle;

	void Start () {
		anim = GetComponent<Animator> ();
	}

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
		direction = (Player.Position - transform.position).normalized;
		GetComponent<Rigidbody2D>().MovePosition(transform.position + direction * speed * Time.deltaTime);	

		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

		if (angle >= 45 && angle < 135) {
			anim.Play ("enemyUp");
		}
		else if ((angle >= 135 && angle <=180) || (angle <= -135 && angle >= -180)) {
			anim.Play ("enemyLeft");
		}
		else if (angle <= -45 && angle > -135) {
			anim.Play ("enemyDown");
		}
		else {
			anim.Play ("enemyRight");
		}
	}
}
