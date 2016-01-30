using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	[Header("Settings")]
	public float minSpawnDelay = 1.2f;
	public float maxSpawnDelay = 2.5f;
	public float minSpawnDistance = 2f;
	public float maxSpawnDistance = 7f;
	[Header("References")]
	public GameObject enemyPrefab;

	void Start () {
		StartCoroutine(SpawnEnemyOnTimer());
	}

	private IEnumerator SpawnEnemyOnTimer() {
		while (true){
			yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
			Instantiate(enemyPrefab,
				Player.Position + Vector3.up * Random.Range(minSpawnDistance, maxSpawnDistance) * (Random.value > 0.5f ? 1 : -1) +
				Vector3.right * Random.Range(minSpawnDistance, maxSpawnDistance) * (Random.value > 0.5f ? 1 : -1), Quaternion.identity);
		}
	}
}
