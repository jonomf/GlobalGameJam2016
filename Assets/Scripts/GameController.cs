using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public static GameController instance;

	void Awake () {
		instance = this;
		Layers.Init();
	}
}
