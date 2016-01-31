using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Boss_Greed : MonoBehaviour {

	public static Boss_Greed instance;

	static SpriteRenderer spriteRenderer;

	public GameObject collectablesGroup;
	public Sprite secondSprite;

	[Header("Settings")]
	public float startingHealth = 500;

	public float drainSpeed = 0.1f;
	public float drainIncrease = 0.05f;

	private static float MaxHealth;
	public Transform bossMove;

	public float damage = 1;
	public float damRes = 0.95f;

	static bool halfHealth = false;

	public static float Health { get; private set; }

	void Start () {
		instance = this;
		halfHealth = false;
		Health = startingHealth;
		MaxHealth = Health;

	}

	void Awake () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		Player.GetHurt (instance.drainSpeed);
	}

	public static void Regenerate(float regen = 100) {
		Health += regen;
		UIController_GreedBoss.UpdateBossHealth ();
	}
		
	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.layer == Layers.PlayerArrowNum){
			Destroy(other.gameObject);
			GetHurt (Player.instance.getAttack() * 2 * damage);
		}
	}

	public static void GetHurt (float damage = 50f) {
		Health -= damage;
		UIController_GreedBoss.UpdateBossHealth ();
		if (Health < MaxHealth / 2  && halfHealth == false) {
			halfHealth = true;
			StageTwo ();
		}

		if (Health <= 0){
			GameController.Win();
		}
	}

	static void StageTwo () 
	{
		spriteRenderer.sprite = instance.secondSprite;
		instance.transform.localScale = new Vector2 (15, 15);
		instance.transform.position = instance.bossMove.position;
		//instance.collectablesGroup.SetActive(true);
	}

	public static void CheckHealth () {
		if (Health > MaxHealth) {
			Health = MaxHealth;
		}
	}

}
