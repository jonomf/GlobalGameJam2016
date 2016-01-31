using UnityEngine;
using System.Collections;

public class Child : MonoBehaviour {

	public static bool ChildHeld { get; private set; }

    public const int STARTING_HEALTH = 3;
    public int Health { get; private set; }

    void Start(){
        Health = STARTING_HEALTH;
    }

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.layer == Layers.PlayerNum){
			if (!ChildHeld){
				ChildHeld = true;
				Destroy(gameObject);
			}
		}
        else if (other.gameObject.layer == Layers.PlayerArrowNum){
            Destroy(other.gameObject);
            Die();
        }
	}

    void Die(){
        GodManager.updateBars(5,-10,0);
        Destroy(gameObject);
    }

	public static void ChildCollected() {
		ChildHeld = false;
		AudioController.instance.childDropOff.Play();
        GodManager.updateBars(-10,30,0);
	}
}
