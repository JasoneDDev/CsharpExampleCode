using UnityEngine;
using System.Collections;

public class CamScript : MonoBehaviour
{
	
	
	loadingScript loader;
	
	
	public Vector2 upperLeft;
	public Vector2 lowerRight;
	public Transform LBound;
	public Transform RBound;
	public Transform TBound;
	public Transform BBound;
	
	public Transform target;
	public float smoothTime = 0.3f;
	private Transform thisTransform;
	private Vector2 velocity;
	
	public CharacterMaster player;
	private Vector3 Movement;
	
	void Start ()
	{
		
		loader = GetComponent(typeof(loadingScript)) as loadingScript;
		
		thisTransform = transform;
	
			player = FindObjectOfType(typeof(CharacterMaster)) as CharacterMaster;
	}
	
	
	void FixedUpdate ()
	{
	
	
			Movement = player.Movement;
			
			if( LBound.position.x > upperLeft.x &&
				RBound.position.x < lowerRight.x  &&
				TBound.position.z < upperLeft.y &&
				BBound.position.z > lowerRight.y
				
				)
			{
					Move(true,true);
				
			}
			else
			{
				//Debug.Log(" Left= " + LBound.position.x + " right= " + RBound.position.x + " top= " + TBound.position.z);
				bool x = true;
				bool y = true;
//				if(LBound.position.x < upperLeft.x && Movement.x < 0)
//				{
//						Movement.x=0;
//					x=false;
//				}
//				
//				if(RBound.position.x > lowerRight.x  && Movement.x > 0)
//				{
//					Movement.x=0;
//					x=false;
//				}
//				
//				if(TBound.position.z > upperLeft.y  && Movement.z > 0)
//				{
//					Movement.z=0;
//					y=false;
//				}
//				
//				if(BBound.position.z < lowerRight.y  && Movement.z < 0)
//				{
//					Movement.z=0;
//					y=false;
//				}
//				
				Move(x,y);
				
			}
			
			
			
				
			
		}
	
		void Move(bool x, bool y)
		{
				Vector3 tempV3 = new Vector3(thisTransform.position.x,0,thisTransform.position.z);
					
				if(x)
				{
					tempV3.x = Mathf.SmoothDamp( tempV3.x, 
					target.position.x, ref velocity.x, smoothTime);
				}
		
				if(y)
				{
					tempV3.z = Mathf.SmoothDamp( tempV3.z, 
					target.position.z, ref velocity.y, smoothTime);
					thisTransform.position = tempV3;	
			
				}
		}
	}

