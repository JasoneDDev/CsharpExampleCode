using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour
{
	public float damageAmount=10.0f;
	public float reloadTime= 0.5f;
	public bool isMachete=false;
	public bool isShotgun=false;
	public bool isAutomatic=false;
	public GameObject particleShot;
	private bool shooting=false;
	
	public int Ammunition = 100;
	public StartScript startScript;
	
	
	public IEnumerator Fire()
	{
		if (shooting)
		{
		
		}
		else 
		{
		shooting=true;
		
		yield return new WaitForSeconds(reloadTime);
		
		if(isShotgun)
			{
			if(particleShot!=null)
			{
				//Debug.Log("I just shot my " + this.gameObject.name);
				GameObject blast = GameObject.Instantiate(particleShot, this.transform.position, this.transform.rotation) as GameObject;
				blast.transform.parent = this.transform;
				blast.transform.position = this.transform.position;
					//Debug.Log("fired");
			}
			if(Ammunition >0)
				{		
				Ammunition-=2;
				}
				
		}
		startScript.Ammunition = Ammunition;
		shooting=false;
		}
		
	}
	
	void OnTriggerStay(Collider collider)
	{
		EnemyMaster enemy = collider.GetComponent<EnemyMaster>();
		
		
		if ((enemy != null && shooting && isMachete) || (enemy != null && shooting && isShotgun))
		{
			///Debug.Log("Just hit a bad guy");
			enemy.GetDamage(damageAmount);
			
		}
		
		else
		{
		
			//Debug.Log("hitting? " + collider.name + " shooting? " + shooting + " machete? " + isMachete);
		}
	}
	
	public void ShowAmmunition()
	{
		startScript = FindObjectOfType(typeof(StartScript)) as StartScript;
		startScript.Ammunition = Ammunition;
	}
	
	
}
