using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class FollowEnemy : MonoBehaviour {

	public float speed = 3;
	public float damage = 20;//dealt to player

    public const int FOLLOW_DISTANCE = 8;
    
    public const int STARTING_HEALTH = 3;
    public int Health { get; private set; }

	Animator anim;

	private Vector3 direction;
	string dir = "down";

	void Start () {
		anim = GetComponent<Animator> ();
        Health = STARTING_HEALTH;
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.layer == Layers.PlayerArrowNum){
			Destroy(other.gameObject);
			GetHurt();
		}
		if (other.gameObject.layer == Layers.PlayerNum){
			Destroy(gameObject);
            Player.GetHurt(20);
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

    public void GetHurt(int dmg = 1) {
        Health -= dmg;
        GodManager.updateBars(2,-1,0);
        if (Health <= 0){
            Destroy(gameObject);
        }
    }
}
