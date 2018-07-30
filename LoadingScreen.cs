using UnityEngine;
using System.Collections;
using UIToolkitNameSpace;

public class LoadingScreen : MonoBehaviour {

	// Use this for initialization
	
	public float pauseTime = 2.0f;
	string sceneToLoad="";
	public UIToolkit buttonUI;
	
	void Start ()
	{
		Time.timeScale = 1f;
		sceneToLoad = PlayerPrefs.GetString("loadLevel");
		// animated sprite
		var animatedSprite = UI.firstToolkit.addSprite( "LOADING01.png", 0, 0, 1 );
		var anim = animatedSprite.addSpriteAnimation( "anim", 0.15f, "LOADING01.png", "LOADING01.png", "LOADING02.png", "LOADING02.png", "LOADING03.png", "LOADING03.png", "LOADING04.png", "LOADING04.png","LOADING01.png", "LOADING01.png", "LOADING02.png", "LOADING02.png", "LOADING03.png", "LOADING03.png", "LOADING04.png", "LOADING04.png" );
		animatedSprite.positionFromCenter( 0.0f, 0.0f );
		//anim.loopReverse = true; // optinally loop in reverse
		animatedSprite.playSpriteAnimation( "anim", 5 );
		StartCoroutine(loadPause(pauseTime));
	}
	
	IEnumerator loadPause(float time)
	{
		yield return new WaitForSeconds(time);
		Application.LoadLevelAsync(sceneToLoad);
	}
}
