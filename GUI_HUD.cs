using UnityEngine;
using System.Collections;
using UIToolkitNameSpace;

public class GUI_HUD : MonoBehaviour {

	public float guiScaleFactor = 1.0f;
	public float textScaleFactor = 1.0f;
	public AudioClip buttonSound;
	UIProgressBar HealthBar;
	UISprite HealthBG;
	
	UISprite HealthPickup;
	UISprite AmmoPickup;
	
	UIButton PauseBtn;
	UIButton PlayBtn;
	UIButton RetryBtn;
	UIButton QuitBtn;
	UIToggleButton AudioBtn; 
	
	UITextInstance ScoreText;
	UITextInstance AmmoText;
	UITextInstance PickupAmount;
	
	UITextInstance LevelInstruc;
	public string LvlInstructions;
	
	public UIJoystick  LToggle;
	public UIContinuousButton AttackBtn;

	UIText text;
	
	public UIToolkit buttonUI;
	public UIToolkit textToolkit;
	
	StartScript startScript;
	loadingScript loader;
	CharacterMaster character;
	
	void Start ()
	{
	
	text = new UIText( textToolkit, "GhoulishFont", "GhoulishFont_0.png");	
	startScript = FindObjectOfType(typeof(StartScript)) as StartScript;	
	loader  = FindObjectOfType(typeof(loadingScript)) as loadingScript;	
		character = FindObjectOfType(typeof(CharacterMaster)) as CharacterMaster;	
	//DontDestroyOnLoad(this.gameObject);
		
		
		
	AmmoText = text.addTextInstance( "Bullets: " + startScript.Ammunition.ToString(),0,0,textScaleFactor, 2, Color.white, UITextAlignMode.Left, UITextVerticalAlignMode.Middle );	
	PickupAmount = text.addTextInstance( "+20",0,0,textScaleFactor*5, 2, Color.white, UITextAlignMode.Left, UITextVerticalAlignMode.Middle );	
	PickupAmount.alphaTo(0.1f,0,Easing.Quartic.easeIn);
	PickupAmount.hidden=true;
		
	LevelInstruc = text.addTextInstance( "" + LvlInstructions,0,0,textScaleFactor, 2, Color.white, UITextAlignMode.Center, UITextVerticalAlignMode.Middle );	
	LevelInstruc.positionCenter();
	LevelInstruc.alphaTo(0.01f,0,Easing.Quartic.easeIn);
	
	StartCoroutine( StartLvlInstruction());
	// buttons ------------------------------------
		

	PauseBtn = UIButton.create(buttonUI,"PauseBtn.png","PauseBtn.png",0,0,10);
	PauseBtn.positionFromTopLeft(0.035f,0.045f);
	PauseBtn.onTouchUpInside += onTouchPauseBtn;
	PauseBtn.touchDownSound = buttonSound;
	PauseBtn.hidden = false;
		
	PlayBtn = UIButton.create(buttonUI,"PlayBtn.png","PlayBtn.png",0,0,10);
	PlayBtn.positionFromCenter(0.2f,-0.21f);
	PlayBtn.onTouchUpInside += onTouchPlayBtn;
	PlayBtn.touchDownSound = buttonSound;
	PlayBtn.hidden = true;
		
	RetryBtn = UIButton.create(buttonUI,"RetryBtn.png","RetryBtn.png",0,0,10);
	RetryBtn.positionFromCenter(0.2f,-0.07f);
	RetryBtn.onTouchUpInside += onTouchRetryBtn;
	RetryBtn.touchDownSound = buttonSound;
	RetryBtn.hidden = true;	
		
	QuitBtn = UIButton.create(buttonUI,"QuitBtn.png","QuitBtn.png",0,0,10);
	QuitBtn.positionFromCenter(0.2f,0.07f);
	QuitBtn.onTouchUpInside += onTouchQuitBtn;
	QuitBtn.touchDownSound = buttonSound;
	QuitBtn.hidden = true;		
	
	//------TOGGLE BTNS-------------------------------
	
		
	AudioBtn = UIToggleButton.create(buttonUI,"AudioOffBtn.png","AudioBtn.png","AudioOffBtn.png",0,0,10);
	AudioBtn.positionFromCenter(0.2f,0.21f);
	AudioBtn.onToggle  += onTouchAudioBtn;
	AudioBtn.hidden = true;		
		if(PlayerPrefs.GetInt("volume")== 1)
		{
			AudioBtn.selected = true;
		}
		
	//-------------------HEALTH METER--------------------------------------
		
		
	HealthBG = buttonUI.addSprite( "HealthBG.png", 0, 0, 10 );
	HealthBG.positionFromCenter(-0.41f,0.0f);
	
	
	HealthBar = UIProgressBar.create( buttonUI, "HealthBar.png", 0, 0, false, 5 ,false);
	Vector2 tempVec;
	tempVec = new Vector2(HealthBG.position.x + 95, HealthBG.position.y - 10);
	HealthBar.position = tempVec;
	
	HealthBar.value=1;
		
		
	//----------------------PICKUPS-------------------------
		
		
		
	HealthPickup = buttonUI.addSprite( "HeartPickup.png", 0, 0, 10 );
	HealthPickup.positionFromCenter(-0.0f,0.0f);
	HealthPickup.alphaTo(0.1f,0,Easing.Quartic.easeIn);
	HealthPickup.hidden = true;
		
	AmmoPickup = buttonUI.addSprite( "AmmoPickup.png", 0, 0, 10 );
	AmmoPickup.positionFromCenter(-0.0f,0.0f);
	AmmoPickup.alphaTo(0.1f,0,Easing.Quartic.easeIn);
	AmmoPickup.hidden = true;	
		
		
	//-----------------------------CONTROLS------------------------------------------
		
		
	LToggle = UIJoystick.create(buttonUI,"LToggle.png", new Rect( Screen.width*0.01f, Screen.height*0.45f, Screen.width*0.4f, Screen.height*0.6f ), Screen.width *0.175f, Screen.height*-0.3f );
	LToggle.deadZone = new Vector2( 0.8f, 0.8f );
	//LToggle.setJoystickHighlightedFilename( "LToggleT.png" );	
		AttackBtn = UIContinuousButton.create( "AttackBtn.png", "AttackBtn.png", 0, 0 );
		AttackBtn.positionFromBottomRight( 0.05f, 0.05f );
		AttackBtn.centerize(); // centerize the button so we can scale it from the center
		AttackBtn.highlightedTouchOffsets = new UIEdgeOffsets( 30 );
		AttackBtn.onTouchIsDown += onTouchAttackBtn;
	AttackBtn.onTouchUpInside += onTouchAttackBtnUp;
			
		
		
		
		if(loader!=null)
		{
			if(loader.ControllerCount > 0)
			{
				LToggle.hidden=true;
				AttackBtn.hidden=true;
			}
		}
		
	}
	
	
	
	IEnumerator StartLvlInstruction()
	{
		yield return new WaitForSeconds(1f);
		LevelInstruc.alphaTo(1,1,Easing.Quartic.easeIn);	
		yield return new WaitForSeconds(2.5f);
		LevelInstruc.alphaTo(1,0,Easing.Quartic.easeIn);
		yield return new WaitForSeconds(1f);
		LevelInstruc.hidden=true;
	}
	
	// Update is called once per frame
	void LateUpdate ()
	{
		AmmoText.clear();
		AmmoText.text = "Bullets: " + startScript.Ammunition.ToString();
		AmmoText.positionFromTopRight(0.05f,0.14f);	
		
		HealthBar.value = startScript.CharHealth/100;
	}
	
	public void onTouchPauseBtn( UIButton sender )
	{
		
		PauseBtn.hidden = true;
		PlayBtn.hidden = false;
		RetryBtn.hidden = false;	
		QuitBtn.hidden = false;
		
		AudioBtn.hidden = false;
		
		LToggle.hidden=true;
		AttackBtn.hidden =true;
		startScript.PauseGame(true);
	}
	
	public void onDeath( )
	{
		
		PauseBtn.hidden = true;
		//PlayBtn.hidden = false;
		RetryBtn.hidden = false;	
		QuitBtn.hidden = false;
		
		AudioBtn.hidden = false;
		LToggle.hidden=true;
		AttackBtn.hidden =true;
		startScript.PauseGame(true);
	}
	
	public void onTouchAttackBtn( UIButton sender )
	{
		character.Attack();
	}
	public void onTouchAttackBtnUp( UIButton sender )
	{
		character.attack=false;
	}
	
	
	public void onTouchPlayBtn( UIButton sender )
	{
		//unpause all
		startScript.PauseGame(false);
		//hide btns
		if(loader!=null)
		{
			if(loader.ControllerCount > 0)
			{
				AttackBtn.hidden =false;
				LToggle.hidden=false;
			}
		}
		PauseBtn.hidden = false;
		PlayBtn.hidden = true;
		RetryBtn.hidden = true;	
		QuitBtn.hidden = true;
		
		
		AudioBtn.hidden = true;
	}
	
	public void onTouchRetryBtn( UIButton sender )
	{
		Time.timeScale = 1f;
		PlayerPrefs.SetString("loadLevel" , Application.loadedLevelName);
		Application.LoadLevelAsync("Load");
	}

	
	public void onTouchQuitBtn( UIButton sender )
	{
		// sure you wanna quit?
		Application.Quit();
	}
	
	public void onTouchAudioBtn(UIToggleButton sender, bool toggle)
	{
		//toggle audio on / off
		Debug.Log(toggle);
		if(toggle)
		{
			AudioListener.volume = 0;
			PlayerPrefs.SetInt("volume",1);
		}
		else
		{
			AudioListener.volume = 1;
			PlayerPrefs.SetInt("volume",2);
		}
		
	}
	
	public void Error()
	{
		
	}
	
	public void PickUpFx(string pickedUp, float amount)
	{
		 switch (pickedUp)
		{
		case "Health":
			HealthPickup.hidden = false;
			PickupAmount.hidden=false;
			HealthPickup.alphaTo(0.5f,1,Easing.Quartic.easeIn);
			PickupAmount.clear();
			PickupAmount.text="+" + amount.ToString();
			PickupAmount.positionFromCenter(0.0f,0.1f);	
			PickupAmount.alphaTo(0.5f,1,Easing.Quartic.easeIn);
			
			StartCoroutine(hideHeart());
			
		break;
		case "ShotgunAmmo":
			AmmoPickup.hidden = false;
			PickupAmount.hidden=false;
			AmmoPickup.alphaTo(0.5f,1,Easing.Quartic.easeIn);
			PickupAmount.clear();
			PickupAmount.text="+" + amount.ToString();
			PickupAmount.positionFromCenter(0.0f,0.1f);	
			PickupAmount.alphaTo(0.5f,1,Easing.Quartic.easeIn);
			
			StartCoroutine(hideAmmo());
		break;
		default:
			return;
		}
	}
	
	IEnumerator hideHeart()
	{
		yield return new WaitForSeconds(0.5f);
		
		HealthPickup.alphaTo(0.5f,0,Easing.Quartic.easeIn);
		PickupAmount.alphaTo(0.5f,0,Easing.Quartic.easeIn);
		yield return new WaitForSeconds(0.5f);
		HealthPickup.hidden = true;
		PickupAmount.hidden = true;
	}
	
	IEnumerator hideAmmo()
	{
		yield return new WaitForSeconds(0.5f);
		
		AmmoPickup.alphaTo(0.5f,0,Easing.Quartic.easeIn);
		PickupAmount.alphaTo(0.5f,0,Easing.Quartic.easeIn);
		yield return new WaitForSeconds(0.5f);
		AmmoPickup.hidden = true;
		PickupAmount.hidden = true;
	}
	
}
