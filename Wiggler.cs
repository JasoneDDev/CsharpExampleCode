using UnityEngine;
using System.Collections;

public class Wiggler : MonoBehaviour {

	
	Vector3 BasePOS;
	
	void Start ()
	{
		BasePOS = this.transform.position;
	}
	
	float amplitudeX = 3.0f;
	float amplitudeY = 3.0f;
	float omegaX = 0.25f;
	float omegaY = 0.25f;
	float index;
 
	public void Update()
	{
	    index += Time.deltaTime;
		float x = amplitudeX*Mathf.Cos (omegaX*index);
		if(x > BasePOS.x + 20)
		{
			 
		}
		else
		{
			 x *=-1;
		}
	   
	    float y = amplitudeY*Mathf.Sin (omegaY*index);
	    transform.localPosition= new Vector3(x,0,y);
	}
}
