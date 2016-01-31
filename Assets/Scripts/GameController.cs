using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour {

	public static GameController instance;

	public GameObject feedbackPopupPrefab;
	public AnimationClip feedbackPopupAnimClip;

    public static string lastScene = "Game scene";

	void Awake () {
		instance = this;
        Layers.Init();
	    if (SceneManager.GetActiveScene().name == "Game scene"){
			GodManager.Reset();
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
        lastScene = SceneManager.GetActiveScene().name;
		SceneManager.LoadScene("Win");
	}

	public static void Lose() {
        lastScene = SceneManager.GetActiveScene().name;
		SceneManager.LoadScene("Lose");
	}

    public static void LoadLastScene(){
        SceneManager.LoadScene(lastScene);
    }
}
