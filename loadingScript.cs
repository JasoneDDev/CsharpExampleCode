using UnityEngine;
using System.Collections;

public class loadingScript : MonoBehaviour {

public bool isTest=false;
public bool  isMobile=false;
public bool  isSplashPage=false;
bool Active=false;

public GUITexture[] SplashPages;

 int currentPage;

 float MasterAlpha;
 float MasterVolume;
 bool triggeredForward=false;
 bool triggeredBack=false;

bool paused=false;
float pauseBetweenSplash;
bool doNotFadeLastImage=false;
float interval;
float TimeRemaining;

public string SceneToLoad;

 static int TimeTrial = 1810;// 30 minutes trial = 1800

bool SwappingSound=false;
AudioClip audioToSwapTo;
float MusicVolumeFloat = 1;

bool audioOn=true;

private bool mogaPro = false;	// Controller Model Conditional
private bool padFound =false;

float CountDownTimer;
[HideInInspector] public bool WW3DIsUnlocked = true;
[HideInInspector] public bool gameIsUnlocked = false;

JavaCommander jCommander;

[HideInInspector] public int ControllerCount;
string[] Controllers;
OuyaSDK.OuyaPlayer[] Players;

bool setPlayer=false;
bool MogaPro=false;



// need a array for 4 players, using strings or ouya.sdk for players.
// when the player presses a btn for player 1 I assign that controller to the player 01 slot....  if for whatever reason the controller number changes I
//pause the game and ask for them to press the specified btn again and then continue the game
void DetectControllers()
{
	Controllers = Input.GetJoystickNames();
	
	if(ControllerCount == 0)
	{
		ControllerCount = Controllers.Length;
		
	}
	
}



void Start ()
{
DontDestroyOnLoad(this.gameObject);

PlayerPrefs.SetInt("StartCount", (PlayerPrefs.GetInt("StartCount")+1));

if(PlayerPrefs.GetInt("audioOn") == 2 )
{
audioOn = true;
AudioListener.volume = 1;

}
else if(PlayerPrefs.GetInt("audioOn") == 1)
{
audioOn = false;
AudioListener.volume = 0;
}
else
{
audioOn = true;
AudioListener.volume = 1;
PlayerPrefs.GetInt("audioOn",2);
}

// UNLOCKS FOR TESTING-------------------------------------------------------------------------------
	//	PlayerPrefs.SetInt("unlockedRiverPack",2);
//
//		var setString:String = "0,1,2,3,4,5,6,7,8,";
//		PlayerPrefs.SetString("unlockedKayaks", setString);
//					
//				
//		var setString3:String = "0,1,2,";
//		PlayerPrefs.SetString("unlockedHats", setString3 );
//			
//		var setString2:String = "0,1,2,";
//		PlayerPrefs.SetString("unlockedHairs", setString2 );
//PlayerPrefs.SetInt("MoonCoinCount",5500);
//PlayerPrefs.SetInt("GoldCoinCount",2500);
//PlayerPrefs.DeleteAll();

//--------------------------------------------------------------------------------------------


//PlayerPrefs.SetFloat("CountDownTime", 0.0);

Screen.SetResolution(1280, 720, true);
//OuyaSDK.OuyaJava.JavaSetResolution("1280x720");


jCommander = GetComponent(typeof(JavaCommander)) as JavaCommander;
		      

DetectControllers();
//
//		for(var ii:int=0; ii < 4; ii++)
//			{
//			var controllerNum:OuyaSDK.OuyaPlayer = (ii+1);
//			Debug.Log(ii + " cont " + controllerNum);
//			
//			Players[ii]=(controllerNum);
//			}

if(PlayerPrefs.GetString("NeverLoaded") != "IHaveBeenLoaded" && !isMobile)
{
	CountDownTimer=0.0f;
	PlayerPrefs.SetFloat("CountDownTime", CountDownTimer);
	PlayerPrefs.SetString("NeverLoaded", "IHaveBeenLoaded");
	
}
else
{
	
	CountDownTimer = PlayerPrefs.GetFloat("CountDownTime");
	
	if(PlayerPrefs.GetString("WW3DPurchased") == "ofCourseIBoughtIt" || isTest || isMobile)
	{
		WW3DIsUnlocked = true;
		gameIsUnlocked = true;
		
	}
	else if(CountDownTimer >= TimeTrial && PlayerPrefs.GetString("WW3DPurchased") != "ofCourseIBoughtIt")
	{
		WW3DIsUnlocked = false;
		gameIsUnlocked = false;
	}
	
}


//Application.LoadLevel("MenuScene");
audio.volume = 0;

if(isSplashPage)
{
LaunchGame();

}

}

void Awake()
	{
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
			

		/*
		MOGA MAPPING - EXAMPLE 1
		Input.RegisterInputButton("Fire1", MogaController.KEYCODE_BUTTON_A);
		
		MOGA MAPPING - EXAMPLE 2
		Input.RegisterInputButton("joystick button 0", Controller.KEYCODE_BUTTON_A);
		*/

		// MOGA MAPPING - EXAMPLE 3
		#if UNITY_ANDROID
		MogaInput.RegisterInputKey(KeyCode.JoystickButton0, MogaController.KEYCODE_BUTTON_A);
		MogaInput.RegisterInputKey(KeyCode.JoystickButton1, MogaController.KEYCODE_BUTTON_B);
		MogaInput.RegisterInputKey(KeyCode.JoystickButton2, MogaController.KEYCODE_BUTTON_X);
		MogaInput.RegisterInputKey(KeyCode.JoystickButton3, MogaController.KEYCODE_BUTTON_Y);
		MogaInput.RegisterInputKey(KeyCode.JoystickButton4, MogaController.KEYCODE_BUTTON_L1);
		MogaInput.RegisterInputKey(KeyCode.JoystickButton5, MogaController.KEYCODE_BUTTON_R1);
		MogaInput.RegisterInputKey(KeyCode.JoystickButton6, MogaController.KEYCODE_BUTTON_SELECT);
		MogaInput.RegisterInputKey(KeyCode.JoystickButton7, MogaController.KEYCODE_BUTTON_START);
		MogaInput.RegisterInputKey(KeyCode.JoystickButton8, MogaController.KEYCODE_BUTTON_THUMBL);
		MogaInput.RegisterInputKey(KeyCode.JoystickButton9, MogaController.KEYCODE_BUTTON_THUMBR);
		MogaInput.RegisterInputKey(KeyCode.JoystickButton10, MogaController.KEYCODE_BUTTON_L2);
		MogaInput.RegisterInputKey(KeyCode.JoystickButton11, MogaController.KEYCODE_BUTTON_R2);
		MogaInput.RegisterInputKey(KeyCode.JoystickButton12, MogaController.KEYCODE_DPAD_UP);
		MogaInput.RegisterInputKey(KeyCode.JoystickButton13, MogaController.KEYCODE_DPAD_DOWN);
		MogaInput.RegisterInputKey(KeyCode.JoystickButton14, MogaController.KEYCODE_DPAD_LEFT);
		MogaInput.RegisterInputKey(KeyCode.JoystickButton15, MogaController.KEYCODE_DPAD_RIGHT);
	
		MogaInput.RegisterInputAxis("Horizontal", MogaController.AXIS_X);		// Left Nub Horizontal
		MogaInput.RegisterInputAxis("Vertical", MogaController.AXIS_Y);			// Left Nub Vertical
		MogaInput.RegisterInputAxis("LookHorizontal", MogaController.AXIS_Z);	// Right Nub Horizontal
		MogaInput.RegisterInputAxis("LookVertical", MogaController.AXIS_RZ);	// Right Nub Vertical
		MogaInput.RegisterInputAxis("L2", MogaController.AXIS_LTRIGGER);	// L2 Trigger Axis
		MogaInput.RegisterInputAxis("R2", MogaController.AXIS_RTRIGGER);	// R2 Trigger Axis
		#endif
	}

void LaunchGame()
{
	if(SplashPages.Length>0)
	{
		currentPage=0;
		triggeredForward=true;
		Active=true;
	}
}

void FixedUpdate()
{
	if(Active)
	{
		
	
		
		if(MasterAlpha<0.999f && triggeredForward)
		{
			triggeredBack=false;
			MasterAlpha +=0.01f;
			if(MasterVolume < 1)
			{
			MasterVolume +=0.05f;
			audio.volume = MasterVolume;
			
			}
		}
		else if(doNotFadeLastImage && currentPage+1 == SplashPages.Length)
		{
		Active=false;
		isSplashPage=false;
		LoadScene();
		}
		else if(MasterAlpha>0.001f && triggeredBack)
		{
			triggeredForward=false;
			MasterAlpha -=0.01f;
		}
		else if(triggeredForward && MasterAlpha>0.999f)
		{
		triggeredForward=false;
		triggeredBack=true;
		}
		else if(triggeredBack && MasterAlpha<0.001f)
		{
			StartCoroutine(PauseTime());
		}
		
		if(currentPage < SplashPages.Length)
		{
		Color color = SplashPages[currentPage].color;
				color.a = MasterAlpha;
		SplashPages[currentPage].color = color;
		}
		else
		{
		Active=false;
		isSplashPage=false;
		StartCoroutine(LoadScene());
		}
	}
	
	DetectControllers();
	
		if (MogaInput.GetControllerState() == MogaController.ACTION_CONNECTED)
		{
			if ((MogaInput.GetControllerSupportedVersion() == MogaController.ACTION_VERSION_MOGAPRO) && (!InputManager.Moga))
			{
			InputManager.Moga = true;
			MogaPro=true;
			//ControllerCount = 1;
			//Controllers = ["MogaPro Controller"];
			}
			else if ((MogaInput.GetControllerSupportedVersion() == MogaController.ACTION_VERSION_MOGA) && (!InputManager.Moga))
			{
			InputManager.Moga = true;
			MogaPro=false;
			//ControllerCount = 1;
			//Controllers = ["Moga Controller"];
			}
		}
		else
		{
		InputManager.Moga = false;
//			if(Controllers[0] == "MogaPro Controller" || Controllers[0] == "Moga Controller")
//			{
//			//Controllers = null;
//			//ControllerCount =0;
//			}
		}
		
	OuyaExampleCommon.UpdateJoysticks();
	
	//Debug.Log(SystemInfo.deviceModel);
	
	
	if(SwappingSound)
	{
		if(audio.clip != audioToSwapTo && MusicVolumeFloat>0)
		{
			MusicVolumeFloat-=0.05f;
			audio.volume = MusicVolumeFloat;
			//Debug.Log("music volume " + MusicVolumeFloat);
		}
		else if( MusicVolumeFloat<=0 && audio.clip != audioToSwapTo)
		{
			audio.clip = audioToSwapTo;
			if(!audio.isPlaying)
			{
				audio.Play();
				}
		//	Debug.Log("swapped audio clip");
		}
		else if( MusicVolumeFloat < 1  && audio.clip == audioToSwapTo)
		{
			MusicVolumeFloat+=0.05f;
			audio.volume = MusicVolumeFloat;
			if(!audio.isPlaying)
			{
				audio.Play();
				}
			//Debug.Log("swapping " + MusicVolumeFloat);
		}
		else if(MusicVolumeFloat >=1 && audio.clip == audioToSwapTo && audio.isPlaying)
		{
		SwappingSound=false;
	//	Debug.Log("swapping OFF");
		}
	}	
	
	
	if(CountDownTimer <= TimeTrial && WW3DIsUnlocked && !gameIsUnlocked)
	{
			CountDownTimer += Time.deltaTime; 
			TimeRemaining = TimeTrial - CountDownTimer;
			interval += Time.deltaTime;
			
		//	Debug.Log("time remianing "+TimeRemaining + " " + TimeTrial + " - " + CountDownTimer);
			
			if(interval > 10)
			{
				PlayerPrefs.SetFloat("CountDownTime", CountDownTimer);
				PlayerPrefs.Save();
				interval=0;
			}
	}
	else if(WW3DIsUnlocked && !gameIsUnlocked)
	{
			WW3DIsUnlocked = false;
			PlayerPrefs.SetFloat("CountDownTime", CountDownTimer);
			PlayerPrefs.Save();
			
	}		
}

public IEnumerator PauseTime()
{
			if(!paused)
			{
			paused=true;
			yield return new WaitForSeconds (pauseBetweenSplash);
			triggeredForward=true;
			triggeredBack=false;
			currentPage++;
			yield return new WaitForSeconds (0.02f);
			paused=false;
			}
			
}

public IEnumerator LoadScene()
{
Debug.Log("loading the scene");
yield return new WaitForSeconds (pauseBetweenSplash);
var loadScene = Application.LoadLevelAsync(SceneToLoad);
yield return loadScene;

}









}
