using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class FollowEnemy : MonoBehaviour {

	public float speed = 3;
	public float damage = 10;

    public const int FOLLOW_DISTANCE = 8;
    
    public const int STARTING_HEALTH = 3;
    public int Health { get; private set; }

	Animator anim;

	private Vector3 direction;

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
			Player.GetHurt(damage);
		}
	}

	void Update () {
		MoveTowardsPlayer();
	}

	private void MoveTowardsPlayer() {
        Vector3 distVect = Player.Position - transform.position;
		direction = (Player.Position - transform.position).normalized;
		if (!(distVect.magnitude < FOLLOW_DISTANCE)) return;
		GetComponent<Rigidbody2D>().MovePosition(transform.position + distVect.normalized * speed * Time.deltaTime); float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

		if (angle >= 45 && angle < 135) {
			anim.Play("enemyUp");
		}
		else if ((angle >= 135 && angle <= 180) || (angle <= -135 && angle >= -180)) {
			anim.Play("enemyLeft");
		}
		else if (angle <= -45 && angle > -135) {
			anim.Play("enemyDown");
		}
		else {
			anim.Play("enemyRight");
		}
	}

    public void GetHurt(int damage = 1) {
        Health -= damage;
        GodManager.updateBars(2,-1,0);
        if (Health <= 0){
            Destroy(gameObject);
        }
    }
}
