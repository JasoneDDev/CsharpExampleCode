using UnityEngine;
using System.Collections;

public class LevelBuilder : MonoBehaviour
{

	public Vector2 LvlSize;
	
	public GameObject topRowBGBlocks;
	public GameObject topThirdRowBGBlocks;
	public GameObject midRowBGBlocks; //---------- these blocks will be the ones that exspand to create smaller / larger scenes
	public GameObject bottomThirdRowBGBlocks;
	public GameObject bottomRowBGBlocks;
	
	public GameObject boundarySolid;
	public GameObject boundaryReturn;
	
	public float randGrass;
	public GameObject RandomGrassBlock1;
	public GameObject RandomGrassBlock2;
	

}
