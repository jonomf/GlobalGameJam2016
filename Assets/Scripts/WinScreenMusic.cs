using UnityEngine;
using System.Collections;

public class WinScreenMusic : MonoBehaviour {

	public AudioSource fanfare;
	public AudioSource winTheme;

	void Awake() {
		StartCoroutine(PlayWinThemeAfterFanfare());
	}

	private IEnumerator PlayWinThemeAfterFanfare() {
		yield return new WaitForSeconds(fanfare.clip.length);
		winTheme.Play();
	}
}
