using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	// Use this for initialization
	
	public GameObject[] Enemy;
	public float SpawnTime = 5.0f;
	public int SpawnNum = 10;
	private int Spawned=0;
	private bool spawning=false;
	
	StartScript startScript;
	
	void Start ()
	{
	startScript = FindObjectOfType(typeof(StartScript)) as StartScript;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(!startScript.gamePaused)
		{
		StartCoroutine(SpawnEnemy());
		}
	}
	
	public IEnumerator SpawnEnemy()
		
	{
		if(!spawning && Spawned < SpawnNum)
		{
		spawning=true;
		yield return new WaitForSeconds(SpawnTime);
		Spawned++;
		startScript.enemyNum++;
		GameObject enemyPicked = PickEnemy();	
		GameObject EnemyClone = GameObject.Instantiate(enemyPicked, (this.transform.position ), Quaternion.identity) as GameObject;
			spawning=false;
		}
	}
	
	private  GameObject PickEnemy()
	{
		foreach(GameObject enemy in Enemy)
		{
			int ran = Random.Range(1,10);
			if(ran > 8)
			{
				return enemy;
			}
		}
		
		return Enemy[0];
	}
	
	
}
