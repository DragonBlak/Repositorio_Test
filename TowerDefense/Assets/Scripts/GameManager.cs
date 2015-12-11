using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {


	public GameObject enemy;
	public GameObject tower;

	public Transform spawnPoint;

	public int Waves;
	public float spawnDelay;

	public int enemiesInScene;
	public int towersInScene;
	GameObject[] GM_Tower;
	BuildingAttack[] bAttScript;
	int currentWave;
	GameObject enemyPrefab;
	GameObject towerToInstantiate;

	void Start () {
		currentWave = Waves;
	}

	void Update () {
		enemiesInScene = GameObject.FindGameObjectsWithTag ("Enemy").Length; 
		towersInScene = GameObject.FindGameObjectsWithTag ("Tower").Length; 

		if (Input.GetKeyUp (KeyCode.S)) {
			towerToInstantiate = Instantiate (tower, new Vector3(-10, 1, 0), Quaternion.identity) as GameObject;
		}
		if (enemiesInScene == 0) {
			StartCoroutine ("spawnEnemies");
		}
		GetTowers ();

		if (Input.GetKeyUp (KeyCode.R)) {
			Application.LoadLevel(Application.loadedLevel) ;
		}
	}
	public IEnumerator spawnEnemies(){
		yield return new WaitForSeconds (spawnDelay);

		if (enemiesInScene == 0) {
			int spawnEnemies = Mathf.Abs (Mathf.CeilToInt(towersInScene/2) +3 * 2 + currentWave);
			for(int i = 1; i < spawnEnemies; i++){
				enemyPrefab = Instantiate(enemy, spawnPoint.position, spawnPoint.rotation) as GameObject;

				yield return new WaitForSeconds (1);
			}
			currentWave ++;
		}
	}
	public void GetTowers(){
		GM_Tower = GameObject.FindGameObjectsWithTag("Tower");
		bAttScript = new BuildingAttack[GM_Tower.Length];

		for (int i = 0; i < GM_Tower.Length; i++) {
			bAttScript[i] = GM_Tower[i].GetComponent<BuildingAttack>();
			bAttScript[i].target = new GameObject[enemiesInScene];
			bAttScript[i].target = GameObject.FindGameObjectsWithTag("Enemy");
		}
	}
}
