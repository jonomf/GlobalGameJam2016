using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class FollowEnemy : MonoBehaviour {

	public float speed = 2;
	public float damage = 5;

    public const int FOLLOW_DISTANCE = 8;

	Animator anim;

	private Vector3 direction;
	string dir = "down";

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
        Vector3 distVect = Player.Position - transform.position;
		direction = (Player.Position - transform.position).normalized;
		if (!(distVect.magnitude < FOLLOW_DISTANCE)) {
			this.IdleAnimate ();
			return;
		}
		GetComponent<Rigidbody2D>().MovePosition(transform.position + distVect.normalized * speed * Time.deltaTime); float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

		if (angle >= 45 && angle < 135) {
			anim.Play("enemyUp");
			dir = "up";
		}
		else if ((angle >= 135 && angle <= 180) || (angle <= -135 && angle >= -180)) {
			anim.Play("enemyLeft");
			dir = "left";
		}
		else if (angle <= -45 && angle > -135) {
			anim.Play("enemyDown");
			dir = "down";
		}
		else {
			anim.Play("enemyRight");
			dir = "right";
		}
	}

	private void IdleAnimate() {
		if (dir == "down") {
			anim.Play ("eIdleDown");
		}
		else if (dir == "up") {
			anim.Play ("eIdleUp");
		}
		else if (dir == "right") {
			anim.Play ("eIdleRight");
		}
		else if (dir == "left") {
			anim.Play ("eIdleLeft");
		} 
	}
}
