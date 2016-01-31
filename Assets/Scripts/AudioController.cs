using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {

	public static AudioController instance;

	public AudioSource[] arrowShots;
	public AudioSource childDropOff;
	public AudioSource[] clawSwipes;
	public AudioSource collect;
	public AudioSource enemyDead;
	public AudioSource enemyLoseHealth;
	public AudioSource[] fireball;
	public AudioSource godWarning;

	void Awake() {
		instance = this;
	}
}
