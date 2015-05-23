using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {

	public GameObject Enemy1;
	public GameObject Enemy2;
	public GameObject Enemy3;

	private bool trySpawning1;
	private bool trySpawning2;
	private bool trySpawning3;


	private int enemyCount;

	private Vector3 randPos;

	public int maxEnemies;

	// Use this for initialization
	void Start () 
	{
		enemyCount = 0;

		trySpawning1 = true;
		trySpawning2 = true;
		trySpawning3 = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(trySpawning1)
		{
			Debug.Log ("GOT HERE 1");
			StartCoroutine(spawnEnemy1());
		}
		if(trySpawning2)
		{
			Debug.Log ("GOT HERE 2");

			StartCoroutine(spawnEnemy2());
		}
		if(trySpawning3)
		{
			Debug.Log ("GOT HERE 3");

			StartCoroutine(spawnEnemy3());
		}
	}


	IEnumerator spawnEnemy1()
	{
		if(enemyCount < maxEnemies)
		{
			trySpawning1 = false;
			randPos = new Vector3 (Random.Range(-1440,1440),Random.Range (-470,470));
			Instantiate (Enemy1, randPos, Quaternion.identity);
			enemyCount++;
			yield return new WaitForSeconds (.2f);
			trySpawning1 = true;
		}
	}

	IEnumerator spawnEnemy2()
	{
		if(enemyCount < maxEnemies)
		{
			trySpawning2 = false;
			Instantiate (Enemy2, randPos, Quaternion.identity);
			randPos = new Vector3 (Random.Range(-1440,1440),Random.Range (-470,470));
			enemyCount++;
			Debug.Log ("here");
			yield return new WaitForSeconds (.5f);
			Debug.Log ("here2");

			trySpawning2 = true;
		}
	}

	IEnumerator spawnEnemy3()
	{
		if(enemyCount < maxEnemies)
		{
			trySpawning3 = false;
			Instantiate (Enemy3, randPos, Quaternion.identity);
			randPos = new Vector3 (Random.Range(-1000,1000),Random.Range (-300,300));
			enemyCount++;
			yield return new WaitForSeconds (2f);
			trySpawning3 = true;

		}
	}
}
