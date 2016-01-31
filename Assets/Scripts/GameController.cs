using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour {

	public static GameController instance;

	public GameObject feedbackPopupPrefab;
	public AnimationClip feedbackPopupAnimClip;

	void Awake () {
		instance = this;
        Layers.Init();
	    if (SceneManager.GetActiveScene().name == "Game scene"){
            StartCoroutine(GodManager.DecreaseOnTimer());
		    StartCoroutine(GameSceneLoop());
	    }
	}

	private IEnumerator GameSceneLoop() {
		while (true){
			GodManager.checkEndGame();
			yield return null;
		}
	}

    public static void Transition(){
        SceneManager.LoadScene("Transition");
    }

	public static void Win() {
		SceneManager.LoadScene("Win");
	}

	public static void Lose() {
		SceneManager.LoadScene("Lose");
	}
}
