using UnityEngine;
using System.Collections;

public class BossDirectionIndicator : MonoBehaviour {

	void Update() {
		Quaternion rotation = Quaternion.LookRotation(Boss_Serenity.Position - transform.position, transform.TransformDirection(Vector3.up));
		transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
	}
}
