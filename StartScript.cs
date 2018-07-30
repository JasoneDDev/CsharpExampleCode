using UnityEngine;
using System.Collections;

public class StartScript : MonoBehaviour {

	
	public int Ammunition = 0;
	public float CharHealth = 100;
	public int enemyNum=0;
	
	public bool gamePaused = false;
	// Use this for initialization
	void Start ()
	{
		Time.timeScale = 1f;
	}
	
	public void PauseGame(bool isPaused)
	{
		gamePaused = isPaused;
		if(isPaused)
		{
			Time.timeScale = 0.1f;
		}
		else
		{
			Time.timeScale = 1f;
		}
	}
}
