using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class FollowEnemy : MonoBehaviour {

	public float speed = 2;
	public float damage = 5;

    public const int FOLLOW_DISTANCE = 8;

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
        if(distVect.magnitude < FOLLOW_DISTANCE){
            GetComponent<Rigidbody2D>().MovePosition(transform.position + distVect.normalized * speed * Time.deltaTime);
        }
	}
}
