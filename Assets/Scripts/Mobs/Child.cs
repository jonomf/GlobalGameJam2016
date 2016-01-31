using UnityEngine;
using System.Collections;

public class Child : MonoBehaviour {

	public static bool ChildHeld { get; private set; }//for player
    public static Child currentChild;
    public bool isHeld = false;

    public const int STARTING_HEALTH = 3;
    public int Health { get; private set; }

    void Start(){
        Health = STARTING_HEALTH;
    }

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.layer == Layers.PlayerNum){
			if (!ChildHeld){
				ChildHeld = true;
                isHeld = true;
                currentChild = this;
                //transform.SetParent(other.transform);
			}
		}
        else if (!isHeld && other.gameObject.layer == Layers.PlayerArrowNum){
            Destroy(other.gameObject);
            Die();
        }
	}

    void Update(){
        if(isHeld){
            GetComponent<Rigidbody2D>().position = Player.Position;
        }
    }

    void Die(){
        GodManager.updateBars(5,-10,0);
        Destroy(gameObject);
    }

	public static void ChildCollected() {
		ChildHeld = false;
        Child.currentChild.Die();
		AudioController.instance.childDropOff.Play();
        GodManager.updateBars(-10,30,0);
	}
}
