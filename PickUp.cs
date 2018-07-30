using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {

	public bool isHealth=false;	
	public bool isShotgunAmmo=false;
	
	public int amount = 20;
	
	GUI_HUD gui;
	
	void Start ()
	{
		gui = FindObjectOfType(typeof(GUI_HUD)) as GUI_HUD;
	}
	
	
	
	
	// Update is called once per frame
	void LateUpdate ()
	{
		//Vector3 rot = new Vector3(0,Time.deltaTime,0);
		transform.RotateAround(transform.position, transform.up, Time.deltaTime * 150);
	}
	
	public void DestoyPackage()
	{
	
		if(isHealth)
		{
			gui.PickUpFx("Health", amount);
		}
		else if(isShotgunAmmo)
		{
			gui.PickUpFx("ShotgunAmmo", amount);
		}
		Destroy(this.gameObject);
	}
}
