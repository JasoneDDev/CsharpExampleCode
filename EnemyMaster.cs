using UnityEngine;
using System.Collections;

public class EnemyMaster : MonoBehaviour
{
	
	private Transform Player;
	private CharacterController enemy;
	public float AttackRange = 10.0f;
	public float AwareWange = 50.0f;
	public float RotateSpeed = 1.0f;
	public float AttackDelay = 1.0f;
	public float AttackDamage = 2.0f;
	private Animator animator;
	public float health = 100.0f;
	public float MaxSpeed=25.0f;
	public float Speed=0.6f;
	 public bool attack = false;
	 public bool damage = false;
	 public int state = 0;
	public float pauseDamage = 0.5f;
	private float TimeSinceLastAttack;
	
	 public bool canAttack=true;
	
	public GameObject particleDamage;
	public GameObject particleDeath;
	
	StartScript startScript;
	
	private GameObject[] Players;
	
	
	// Use this for initialization
	void Start ()
	{
		enemy = GetComponent<CharacterController>();
		animator = this.GetComponent<Animator>();
		MaxSpeed = Random.Range(10,30);
		startScript = FindObjectOfType(typeof(StartScript)) as StartScript;
	}
	
	
	void GetClosestPlayer() 
{
	
	
	Players = GameObject.FindGameObjectsWithTag("Player");

	var distanceToPlayer = Mathf.Infinity;
	GameObject wantedPlayer;
	
	
	foreach ( GameObject player1  in Players)
	{
		
	
		float newDistanceToPlayer = Vector3.SqrMagnitude(player1.transform.position- transform.position);
		if (newDistanceToPlayer < distanceToPlayer)
		{
			distanceToPlayer = newDistanceToPlayer;
			Player = player1.transform;
	
		}
	}
	
	




}
	
	// Update is called once per frame
	void Update ()
	{
		if(!startScript.gamePaused)
		{
		GetClosestPlayer() ;
		
		if(animator != null && state != 1 && Player!=null && !damage)
		{
		animator.SetFloat("speed", Speed);
		animator.SetBool("attack", attack);
		//animator.SetBool("damage", damage);
		
		
			
		
		//---------------------------------------------------------------------------------------------
			
		//Rotate towards the player's postion
		Vector3 localPlayer = transform.InverseTransformPoint(Player.position);
		float PlayerAngle = Mathf.Atan2(localPlayer.x, localPlayer.z) * Mathf.Rad2Deg;
		
		//If the player is to your right, rotate to the right
		if ( PlayerAngle > RotateSpeed * Time.deltaTime )
		{
			//print("chase right");
			transform.Rotate(new Vector3(0,RotateSpeed,0) * Time.deltaTime); //Rotate right towards the Player
		}
		else if ( PlayerAngle < -RotateSpeed * Time.deltaTime ) //If the player is to your left, rotate to the left
		{
			//print("chase left");
			transform.Rotate(new Vector3(0,-RotateSpeed,0) * Time.deltaTime); //Rotate left towards the Player
		}
		else
		{
			transform.LookAt(Player.position); //Look directly at the player
		}
		
		//Avoid collision with terrain and other objects when chasing the player and having such an obstacle between the enemy and the player
		if (Physics.Raycast (transform.position, transform.TransformDirection (Vector3.forward - new Vector3(0.2f,0,0)), 0.5f) )
		{
			print("collision from left");
			//rotate right to avoid collision
			transform.Rotate(new Vector3(0,RotateSpeed * 2,0) * Time.deltaTime);
		}
		else if (Physics.Raycast (transform.position, transform.TransformDirection (Vector3.forward - new Vector3(-0.2f,0,0)), 0.5f) )
		{
			print("collision from right");
			//rotate left to avoid collision
			transform.Rotate(new Vector3(0,-RotateSpeed * 2,0) * Time.deltaTime);
		}
		
		//move towards Player
	
			if ( Vector3.Distance(transform.position, Player.position) > AttackRange  ) //If the enemy is too far from the Player...
			{
				if ( !attack && !damage && Vector3.Distance(transform.position, Player.position) < AwareWange ) //if the enemy is not attacking, moce towards the player
				{
				transform.Translate(Vector3.forward * Time.deltaTime * Speed); //move the enemy forward relative to it's own rotation 
				
					
				if(Speed <=0 && Speed < MaxSpeed)
					{
						Speed +=0.15f;
					}
				else
					{
						Speed=MaxSpeed;
					}
				}
			}
		
	
		
		//Keep track of the time since the last attack
		TimeSinceLastAttack += Time.deltaTime;
		
		//If the enemy is within Attack Range of the Player, ATTACK!
		if ( Vector3.Distance(transform.position, Player.position) <= AttackRange )
		{
			if ( TimeSinceLastAttack > AttackDelay ) //Wait for some time between each attack
			{
				TimeSinceLastAttack = 0; //reset the attack delay timer
				
				
					
					Attack();
				
			}
				else
				{
					attack=false;
				}
		
		}	
			
		//--------------------------------------------------------------------------------------------	
			
			
		}
		
		else if(state == 1)
		{
			transform.Translate(-1*Vector3.up * Time.deltaTime * 0.3f);
			
			if(transform.position.y < -5.0)
			{
				Destroy(this.gameObject);
			}
		}
	}}
	
	public void Attack()
	{
		StartCoroutine(Player.GetComponent<CharacterMaster>().DoDamage(AttackDamage)); //reduce the damage value from the Player's health
		
		Speed=0;	
		attack=true;
		Debug.Log("attacking");
	}
	
	
	
	
	
	public void GetDamage( float damageAmnt)
	{
		
		if(canAttack && state != 1)
		{
			canAttack=false;
		Speed =0.0f;	
		animator.SetFloat("speed", Speed);	
		
		damage = true;
		
		animator.SetBool("attack", attack);
		animator.SetBool("damage", damage);
			//Debug.Log("get damaged");
		GameObject BloodSpurt = GameObject.Instantiate(particleDamage, (this.transform.position + new Vector3(0,8.7f,0)), Quaternion.identity) as GameObject;
		
		
			
		StartCoroutine(DamagePause(damageAmnt));	
	
		}
	}
	
		public IEnumerator DamagePause(float damageAmnt)
		
	{
			yield return new WaitForSeconds(pauseDamage);
			health -= damageAmnt;
		if(health <= 0.0f)
		{
			state =1;
			animator.SetInteger("state", state);
			 Destroy(this.GetComponent<Collider>());
			this.rigidbody.isKinematic=true;
			this.tag = "Untagged";
			GameObject BloodPool = GameObject.Instantiate(particleDeath, this.transform.position, Quaternion.identity) as GameObject;
			
		}
			Debug.Log("health =" + health);
			damage = false;
			animator.SetBool("attack", attack);
		animator.SetBool("damage", damage);
			canAttack=true;
	}
}
