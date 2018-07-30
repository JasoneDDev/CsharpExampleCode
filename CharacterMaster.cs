using UnityEngine;
using System.Collections;

public class CharacterMaster : MonoBehaviour {

	
	public float Speed;
	
	loadingScript loader;
	
	OuyaSDK.OuyaPlayer playerName;
	public float AttackRange=10.0f;
	public float Health = 100.0f;
	public Animator animator;
	public Transform Character;
	public Transform HandNull;
	public GameObject Machete;
	private GameObject macheteSpawn;
	public GameObject Shotgun;
	private GameObject shotgunSpawn;
	public Vector3 Movement;
	private CharacterController Hawes;
	[HideInInspector] public bool attack = false;
	public int weaponInHand = 0; // 0= none, 1= machete, 2 = shotgun,
	private bool reloading=false;
	private WeaponScript WS;
	public float rotateSpeed = 50.0f;
	public GameObject BloodSplatter;
	public Material HawesDiffuse;
	public Material HawesInjured;
	public Material HawesBloody;
	
	private bool dead=false;
	private bool beingAttacked=false;
	
	private Transform Enemy;
	private GameObject[] Enemies;
	StartScript startScript;
	GUI_HUD gui;
	// Use this for initialization
	void Start ()
	{
		loader = FindObjectOfType(typeof(loadingScript)) as loadingScript;
		playerName = OuyaSDK.OuyaPlayer.player1;
		Hawes = GetComponent(typeof(CharacterController)) as CharacterController;
		startScript = FindObjectOfType(typeof(StartScript)) as StartScript;
		gui = FindObjectOfType(typeof(GUI_HUD)) as GUI_HUD;
		HandNull =  GameObject.FindGameObjectWithTag("HandNull").transform;
		
		Character.renderer.material= HawesDiffuse;
		//PlayerPrefs.SetString("currentWeapon","machete");
		//PlayerPrefs.SetString("currentWeapon","shotgun");
		weaponInHand = 2;
		if(PlayerPrefs.GetString("currentWeapon") == "")
		{
			weaponInHand = 0;
		}
		else if(PlayerPrefs.GetString("currentWeapon") == "machete")
		{
			weaponInHand = 1;
		}
		else if(PlayerPrefs.GetString("currentWeapon") == "shotgun")
		{
			weaponInHand = 2;
		}
		else if(PlayerPrefs.GetString("currentWeapon") == "semiauto")
		{
			weaponInHand = 3;
		}
		
		weaponInHand = 1;
		
		LoadWeapon();
	}
	
	
	void GetClosestEemy() 
{
	
	
	Enemies = GameObject.FindGameObjectsWithTag("Enemy");

	var distanceToEnemy = Mathf.Infinity;
	GameObject wantedEnemy;
	
	
	foreach ( GameObject enemy1  in Enemies)
	{
		
	
		float newDistanceToEnemy = Vector3.SqrMagnitude(enemy1.transform.position - transform.position);
		if (newDistanceToEnemy < distanceToEnemy)
		{
			distanceToEnemy = newDistanceToEnemy;
			Enemy = enemy1.transform;
	
		}
	}
	
	




}
	
	
	void  OnTriggerEnter(Collider pickup)
	{
		
		if(!pickup.gameObject.GetComponent<PickUp>())
		{
			Debug.Log("you hit ? " + pickup.gameObject.name);
			return;
		}
		PickUp tempPickup = pickup.gameObject.GetComponent<PickUp>();
		
		if(tempPickup.isHealth && Health < (100 - tempPickup.amount))
		{
			Debug.Log("health picked up" + pickup.gameObject.name);
			Health+= tempPickup.amount;
			tempPickup.DestoyPackage();
		}
		else if(tempPickup.isShotgunAmmo && weaponInHand==2)
		{
			Debug.Log("ammo picked up" + pickup.gameObject.name);
			WS.Ammunition += tempPickup.amount;
			tempPickup.DestoyPackage();
		}
		else{
		Debug.Log("??" + pickup.gameObject.name);	
		}
	}
	
	// Update is called once per frame
		void FixedUpdate ()
	{
	
		if(!dead && !startScript.gamePaused)
		{
		animator.SetFloat("speed", Speed);
		animator.SetBool("Attack", attack);
		animator.SetInteger("weapon", weaponInHand);
		animator.SetBool("Damage", beingAttacked);
		//Debug.Log("weaponInHand " + weaponInHand + " msg sent " + animator.GetInteger("weapon"));
		
		GetClosestEemy();
		
		if(Speed >0f)
		{
			Speed-=0.05f;
		}
			
			
	
			
		
		if (InputManager.GetAxis("Vertical", playerName) > 0.01f ||
				InputManager.GetAxis("Vertical", playerName) < -0.01f ||
				InputManager.GetAxis("Roll", playerName) >0.01f ||
				InputManager.GetAxis("Roll", playerName) <-0.01f ||
				gui.LToggle.joystickPosition.x > 0.01f ||
				gui.LToggle.joystickPosition.x <- 0.01f ||
				gui.LToggle.joystickPosition.y <- 0.01f ||
				gui.LToggle.joystickPosition.y > 0.01f)
				
			{
				
				if(loader.ControllerCount<=0)
				{
					Movement = new Vector3(gui.LToggle.joystickPosition.x,0, gui.LToggle.joystickPosition.y);
				}
				else
				{
				Movement = new Vector3(InputManager.GetAxis("Roll", playerName),0, -1*InputManager.GetAxis("Vertical", playerName));
					
					
					if(InputManager.GetButton("SkipRight", playerName))
						{
							
							LoadWeapon();
						}
									
					if(!InputManager.GetButton("Shoot", playerName) && loader.ControllerCount>0)
					{
						attack = false;
					}
					
					else if(InputManager.GetButton("Shoot", playerName))
					{
							Attack();
					}
				}
				
			
			Hawes.Move(Movement);
			
			// rotation
		
				transform.rotation = Quaternion.LookRotation(Hawes.velocity);
			
			
			
			
			if((Movement.x > Movement.z && Movement.x>0) || (Movement.x < Movement.z && Movement.x<0))
			{
				Speed = Mathf.Abs(Movement.x);
			}
			else
			{
					Speed = Mathf.Abs(Movement.z);
			}
			//Hawes.velocity;
			}
		else
		{
			
			if(Speed >0f)
			{
				Speed-=0.05f;
			}
			
			if(Enemy !=null)
			{
				if ( Vector3.Distance(transform.position, Enemy.position) < AttackRange ) //If the enemy is too far from the Player...
				{
					  Vector3 targetDir = Enemy.position - transform.position;
					    float step = rotateSpeed * Time.deltaTime;
	       				Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
	        			Debug.DrawRay(transform.position, newDir, Color.red);
					newDir.y =0;
	        			transform.rotation = Quaternion.LookRotation(newDir);
				}
			}
		}
		
		
		
		
		
		}
		else
		{
		}
	}
	
	public void Attack()
	{
		
		if(!beingAttacked)
			{
				attack=true;
				animator.SetBool("Attack", attack);
				if(WS!=null && WS.isMachete)
				{
					StartCoroutine(WS.Fire());
				Debug.Log("attackjing?-----------------");
				}
				else if(WS!=null && WS.isShotgun && WS.Ammunition > 0)
				{
					StartCoroutine(WS.Fire());	
				Debug.Log("attackjing?");
				}
				else
				{
					LoadWeapon();
				}
			
			
			}	
	}
	
	public IEnumerator AttackPause(float pause)
	{
		yield return new WaitForSeconds(pause);
		
	}
	
	public IEnumerator DoDamage(float AttackDamage)
	{
		
		
		if(Health <=0)
		{
			Death();
			
		}
		else
		{
			beingAttacked=true;
			Health -= AttackDamage;
			startScript.CharHealth = Health;
			Vector3 tempVec;
			tempVec = new Vector3(this.transform.position.x,4,this.transform.position.z);
			GameObject BloodSplat = GameObject.Instantiate(BloodSplatter, tempVec, Quaternion.identity) as GameObject;
			
			if(Health < 25 && Character.renderer.material != HawesBloody)
			{
				Character.renderer.material= HawesBloody;
			}
			else if(Health < 55 && Character.renderer.material != HawesInjured)
			{
				Character.renderer.material = HawesInjured;
			}
			else if(Health > 75 && Character.renderer.material != HawesDiffuse)
			{
				Character.renderer.material = HawesDiffuse;
			}
		}
		
		yield return new WaitForSeconds(0.5f);
		
		beingAttacked=false;
	}
	
	void Death()
	{
		dead=true;
		attack=false;
		animator.SetBool("Attack", attack);
		animator.SetBool("Death", dead);
		 gui.onDeath( );
		this.tag = "Untagged";
	}
	
	public IEnumerator Pause(float pause)
	{
		yield return new WaitForSeconds(pause);
		reloading=false;
	}
	
	void LoadWeapon()
	{
	// here we load the last weapon the user had equipped
		if(!reloading)
		{
				reloading=true;
			weaponInHand ++;
			if(weaponInHand > 2)
			{
				weaponInHand=0;
			}
			
			if(weaponInHand == 0)
			{
				if(macheteSpawn!=null)
				{
					Destroy(macheteSpawn.gameObject);
				}
				 if(shotgunSpawn!=null)
				{
						Destroy(shotgunSpawn.gameObject);
				}
	//			else if()
	//			{
	//			}
				WS = null;
				PlayerPrefs.SetString("currentWeapon","");
			}
			else if(weaponInHand == 1 )
			{
				macheteSpawn = GameObject.Instantiate(Machete, HandNull.position, Quaternion.identity) as GameObject;
				macheteSpawn.transform.parent = HandNull;
				macheteSpawn.transform.rotation = HandNull.rotation;
				WS = macheteSpawn.GetComponent<WeaponScript>();
				PlayerPrefs.SetString("currentWeapon","machete");
				
				 if(shotgunSpawn!=null)
				{
						Destroy(shotgunSpawn.gameObject);
				}
			}
			else if(weaponInHand == 2)
			{
				 shotgunSpawn = GameObject.Instantiate(Shotgun, HandNull.position, Quaternion.identity) as GameObject;
				shotgunSpawn.transform.parent = HandNull;
				shotgunSpawn.transform.rotation = HandNull.rotation;
				WS = shotgunSpawn.GetComponent<WeaponScript>();
				PlayerPrefs.SetString("currentWeapon","shotgun");
				
				if(macheteSpawn!=null)
				{
					Destroy(macheteSpawn.gameObject);
				}
			}
			
			if(WS!=null)
			{
			WS.ShowAmmunition();
			}
			else
			{
				startScript.Ammunition=0;
			}
			
	//		else if(weaponInHand == 3)
	//		{
	//			PlayerPrefs.SetString("currentWeapon","semiauto");
	//		}
		
			StartCoroutine(Pause(0.2f));
		}
	}
	
}
