using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour {

	public static GameController instance;

    void Awake () {
		instance = this;
        Layers.Init();
        StartCoroutine(GodManager.DecreaseOnTimer());
	    if (SceneManager.GetActiveScene().name == "Game scene"){
		    StartCoroutine(GameSceneLoop());
	    }
	}

	private IEnumerator GameSceneLoop() {
		while (true){
			GodManager.checkEndGame();
			yield return null;
		}
	}

	public static void Win() {
		SceneManager.LoadScene("Win");
	}

	public static void Lose() {
		SceneManager.LoadScene("Lose");
	}
}
