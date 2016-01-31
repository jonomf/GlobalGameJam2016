using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FeedbackPopup : MonoBehaviour {

	public Text text;

	public static void DoPopup(string message, Color color, int popupNum) {	
		GameObject go = (Instantiate(GameController.instance.feedbackPopupPrefab, Player.Position + Vector3.up * popupNum, Quaternion.identity) as GameObject);
		Text text = go.GetComponent<FeedbackPopup>().text;
		text.text = message;
		text.color = color;
		GameController.instance.StartCoroutine(DestroyAfterAnimation(go));
	}

	private static IEnumerator DestroyAfterAnimation(GameObject go) {
		yield return new WaitForSeconds(GameController.instance.feedbackPopupAnimClip.length);
		Destroy(go);
	}
}
