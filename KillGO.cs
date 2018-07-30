using UnityEngine;
using System.Collections;

public class KillGO : MonoBehaviour {
	
	public float killTime=1.0f;

	// Use this for initialization
	void Start ()
	{
		StartCoroutine(KillGo());
	}
	
	// Update is called once per frame
	
	
	public IEnumerator KillGo()
	{
		
		yield return new WaitForSeconds(killTime);
		Destroy(this.gameObject);
			
		}
}
