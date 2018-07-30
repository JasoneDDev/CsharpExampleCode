using UnityEngine;
using System.Collections;

public static class LevelBuilderExtension
{

	public static Vector3 spawnLocation;
	
	public static float ySpawnLocation;
	
	public static void buildBGLayer(this Transform mesh, Vector2 size, GameObject topRow, GameObject tThirdRow, GameObject midRow, GameObject bThirdRow, GameObject bottomRow, GameObject boundarySolid, GameObject boundaryReturn, GameObject grassBlock01, GameObject grassBlock02, float randGrass)
	{
		
		
		ySpawnLocation=0;
		
		var BGMaster = new GameObject("BGMaster");
		
		for(var tbNum = 0; tbNum < size.x; tbNum++)
		{
			
			spawnLocation = new Vector3((tbNum * topRow.GetComponent<BoxCollider>().size.x), 0, 0);
			GameObject newClone = GameObject.Instantiate(topRow, spawnLocation, Quaternion.identity) as GameObject;
			newClone.gameObject.transform.parent = BGMaster.transform;
		}
		
		ySpawnLocation += (-1*topRow.GetComponent<BoxCollider>().size.z);
		
		for(var ttbNum = 0; ttbNum < size.x; ttbNum++)
		{
			
			spawnLocation = new Vector3((ttbNum * tThirdRow.GetComponent<BoxCollider>().size.x), 0, ySpawnLocation);
			GameObject newClone = GameObject.Instantiate(tThirdRow, spawnLocation, Quaternion.identity) as GameObject;
			
			newClone.gameObject.transform.parent = BGMaster.transform;
			
			
			int rand01 = Random.Range(0,10);
				if(rand01 > randGrass)
				{
					Vector3 tempLoc01 = new Vector3(0,0.5f,0);
					tempLoc01 += spawnLocation;
					//PutGrass(tempLoc01, grassBlock01, BGMaster);
				int rando01 = Random.Range(0,10);
					if(rando01>=5.1)
					{
					PutGrass(tempLoc01, grassBlock02, BGMaster);
					}
					else
					{
					PutGrass(tempLoc01, grassBlock01, BGMaster);
					}
				}
		}
		ySpawnLocation += (-1*tThirdRow.GetComponent<BoxCollider>().size.z);
		
		//-----------------------MID ROWS-----------------------------------
		
		for(var mbNum = 0; mbNum < size.y; mbNum++)
		{
		
				for(var mbyNum = 0; mbyNum < size.x; mbyNum++)
				{
					
					spawnLocation = new Vector3((mbyNum * tThirdRow.GetComponent<BoxCollider>().size.x), 0, ySpawnLocation);
					GameObject newClone = GameObject.Instantiate(midRow, spawnLocation, Quaternion.identity) as GameObject;
				
				newClone.gameObject.transform.parent = BGMaster.transform;
				int rand = Random.Range(0,10);
				if(rand > randGrass)
				{
					Vector3 tempLoc = new Vector3(0,0.5f,0);
					tempLoc += spawnLocation;
					//PutGrass(tempLoc, grassBlock01, BGMaster);
					int rando01 = Random.Range(0,10);
					if(rando01>=5.1)
					{
					PutGrass(tempLoc, grassBlock02, BGMaster);
					}
					else
					{
					PutGrass(tempLoc, grassBlock01, BGMaster);
					}
				}
				
				}
				ySpawnLocation += (-1*midRow.GetComponent<BoxCollider>().size.z);
		
			
		}
		//---------------------------------------------------Bottom 2 Rows
		
		for(var btbNum = 0; btbNum < size.x; btbNum++)
		{
			
			spawnLocation = new Vector3((btbNum * bThirdRow.GetComponent<BoxCollider>().size.x), 0, ySpawnLocation);
			GameObject newClone = GameObject.Instantiate(bThirdRow, spawnLocation, Quaternion.identity) as GameObject;
			
			newClone.gameObject.transform.parent = BGMaster.transform;
			
			int rand02 = Random.Range(0,10);
				if(rand02 > randGrass)
				{
					Vector3 tempLoc02 = new Vector3(0,0.5f,0);
					tempLoc02 += spawnLocation;
					int rando02 = Random.Range(0,10);
					if(rando02>=5.1)
					{
					PutGrass(tempLoc02, grassBlock02, BGMaster);
					}
					else
					{
					PutGrass(tempLoc02, grassBlock01, BGMaster);
					}
					
				}
			
		}
		ySpawnLocation += (-1*bThirdRow.GetComponent<BoxCollider>().size.z);
		
		for(var bbNum = 0; bbNum < size.x; bbNum++)
		{
			
			spawnLocation = new Vector3((bbNum * bottomRow.GetComponent<BoxCollider>().size.x), 0, ySpawnLocation);
			GameObject newClone = GameObject.Instantiate(bottomRow, spawnLocation, Quaternion.identity) as GameObject;
			
			newClone.gameObject.transform.parent = BGMaster.transform;
		}
		
		//------------------------------------BUILD BOUNDARIES--------------------------------------
		
			
			spawnLocation = new Vector3(0, 0, 0);//top boundary boundarySolid.GetComponent<BoxCollider>().size.y
		
			GameObject newBoundary = GameObject.Instantiate(boundarySolid, spawnLocation, Quaternion.identity) as GameObject;
			newBoundary.gameObject.transform.parent = BGMaster.transform;
			Vector3 BSize = new Vector3(((size.x*topRow.GetComponent<BoxCollider>().size.x) + 10),newBoundary.GetComponent<BoxCollider>().size.y,newBoundary.GetComponent<BoxCollider>().size.z);
			newBoundary.GetComponent<BoxCollider>().size = BSize;
			Vector3 BCenter = new Vector3((BSize.x*0.465f),newBoundary.GetComponent<BoxCollider>().center.y,newBoundary.GetComponent<BoxCollider>().center.z);
			newBoundary.GetComponent<BoxCollider>().center = BCenter;
		
			spawnLocation = new Vector3(0, 0, ((-1*midRow.GetComponent<BoxCollider>().size.z)* size.y)+((-1*bThirdRow.GetComponent<BoxCollider>().size.z))+((-1*bottomRow.GetComponent<BoxCollider>().size.z)) + ((-1*topRow.GetComponent<BoxCollider>().size.z))+ ((-1*tThirdRow.GetComponent<BoxCollider>().size.z)));//top boundary boundarySolid.GetComponent<BoxCollider>().size.y
		
			GameObject newBoundaryB = GameObject.Instantiate(boundarySolid, spawnLocation, Quaternion.identity) as GameObject;
			newBoundaryB.gameObject.transform.parent = BGMaster.transform;
			Vector3 BSizeB = new Vector3(((size.x*topRow.GetComponent<BoxCollider>().size.x) + 10),newBoundaryB.GetComponent<BoxCollider>().size.y,newBoundaryB.GetComponent<BoxCollider>().size.z);
			newBoundaryB.GetComponent<BoxCollider>().size = BSizeB;
			Vector3 BCenterB = new Vector3((BSizeB.x*0.465f),newBoundary.GetComponent<BoxCollider>().center.y,-1*newBoundaryB.GetComponent<BoxCollider>().center.z);
			newBoundaryB.GetComponent<BoxCollider>().center = BCenterB;
		
		//-------------------------------------SIDE BOUNDARIES-------------------------------------------
		
		
			spawnLocation = new Vector3(0, 0, 0);//top boundary boundarySolid.GetComponent<BoxCollider>().size.y
		
			GameObject newBoundaryS = GameObject.Instantiate(boundaryReturn, spawnLocation, Quaternion.identity) as GameObject;
			newBoundaryS.gameObject.transform.parent = BGMaster.transform;
			float heightCalc = ((-1*midRow.GetComponent<BoxCollider>().size.z)* size.y)+((-1*bThirdRow.GetComponent<BoxCollider>().size.z))+((-1*bottomRow.GetComponent<BoxCollider>().size.z)) + ((-1*topRow.GetComponent<BoxCollider>().size.z))+ ((-1*tThirdRow.GetComponent<BoxCollider>().size.z));
			Vector3 BSizeS = new Vector3(newBoundaryS.GetComponent<BoxCollider>().size.x ,newBoundaryS.GetComponent<BoxCollider>().size.y,heightCalc);
			newBoundaryS.GetComponent<BoxCollider>().size = BSizeS;
			Vector3 BCenterS = new Vector3(newBoundaryS.GetComponent<BoxCollider>().size.x/-2  ,newBoundaryS.GetComponent<BoxCollider>().center.y,(BSizeS.z*0.5f));
			newBoundaryS.GetComponent<BoxCollider>().center = BCenterS;
		
			spawnLocation = new Vector3((midRow.GetComponent<BoxCollider>().size.z* size.x ), 0,0);//top boundary boundarySolid.GetComponent<BoxCollider>().size.y
		
			GameObject newBoundarySB = GameObject.Instantiate(boundaryReturn, spawnLocation, Quaternion.identity) as GameObject;
			newBoundarySB.gameObject.transform.parent = BGMaster.transform;
			Vector3 BSizeSB = new Vector3(newBoundarySB.GetComponent<BoxCollider>().size.x, newBoundarySB.GetComponent<BoxCollider>().size.y,heightCalc );
			newBoundarySB.GetComponent<BoxCollider>().size = BSizeSB;
			Vector3 BCenterSB = new Vector3(newBoundarySB.GetComponent<BoxCollider>().size.x/2, newBoundarySB.GetComponent<BoxCollider>().center.y, (BSizeSB.z*0.5f) );
			newBoundarySB.GetComponent<BoxCollider>().center = BCenterSB;
		
		
		
	}
	
	private static void PutGrass(Vector3 location, GameObject grass, GameObject Parent)
	{
		
			GameObject newGrass = GameObject.Instantiate(grass, location, Quaternion.identity) as GameObject;
			
			newGrass.gameObject.transform.parent = Parent.transform;
	}
}
