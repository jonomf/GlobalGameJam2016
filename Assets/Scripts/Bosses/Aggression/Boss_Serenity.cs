using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Boss_Serenity : MonoBehaviour {

	public static Boss_Serenity instance;

	public float surviveTime = 15f;

	public static float RemainingTime { get { return instance.surviveTime - Time.timeSinceLevelLoad; } }

	void Start() {
		instance = this;
	}
}
