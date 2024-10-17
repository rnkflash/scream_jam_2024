//written by Boris Chuprin smokerr@mail.ru
//this is MAIN script which generates road segments
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class roadGenerate : MonoBehaviour
{
	private float newTileToPlayerDist;//absract variable to make a point in 3d world to create new road segment from this coordinates
		
	private GameObject loadingRoadPrefab;//absract variable - to assign road prefabe to this variable
	[SerializeField] private GameObject player;//absract variable - to assign player to this variable
	private GameObject lastTile;//absract variable - name of last road segment

	//this section is all road segments to assign in Inspector
	//straight road segment
	public GameObject roadPref_Straight;

	//light turns
	public GameObject roadPrefR_Light;
	public GameObject roadPrefL_Light;
		
	//easy turns
	public GameObject roadPrefR_Easy;
	public GameObject roadPrefL_Easy;

	//medium turns
	public GameObject roadPrefR_Medium;
	public GameObject roadPrefL_Medium;

	//hard turns
	public GameObject roadPrefR_Hard;
	public GameObject roadPrefL_Hard;
	
	//extreme turns
	public GameObject roadPrefR_Extreme;
	public GameObject roadPrefL_Extreme;

	//hills
	public GameObject roadPref_Up;
	public GameObject roadPref_Down;
	
	//lean fix(described in documentation) - special segments to fix horizontal lean of road
	public GameObject roadPref_LeanR;
	public GameObject roadPref_LeanL;


	//array container for hills and mounts aside of road
	public List<GameObject> hillsCount;


	//array container for obstacles
	public List<GameObject> obstaclesCount;

	
	//array container for props aside of road
	public List<GameObject> propsCount;


	//array container for traffic vehicle prefab
	public List<GameObject> trafficCount;



	//this is scrollers in Inspector to make initial road setup or edit it in runtime.
	[Range(0, 100)]
	public int roadHardess;//road curvature scroller in Inspector. 0 - straight road, 100 - only extreme turns.
	[Range(0, 100)]
	public int hillsPercentage;//frequency of hills. 100 means every road segment is hill.
	[Range(0, 100)]
	public int traffic;//percentage of possible traffic(per new generated road segment). 0 - no traffic at all, 100 - one random car on every new road segment.
	[Range(0, 100)]
	public int spoilers;//percentage of possible road obstacles. 0 - no obstacles, 100 - random obstacle on every new road segment.
	[Range(0, 100)]
	public int sideProps;//percentage of possible pros aside of road. 0 - no props, 100 - random props on every new road segment.
	public int maxRoadHeight;//possible height of road. It means how high or how low your hills or moutains can be.
	//when road reach this height the сlimb change direction to downhill. And vise versa.
	private bool hillBuilded = false;//abstract variable - to get know is last segment was a hill or not.


	public Vector3 lastRoadTilePosition = new Vector3 (0.0f, -0.5f, 0.0f);//first road segment place in 3d world
	//(0.0f, -0.5f, 0.0f) - the coords where your game begins :)
	public Vector3 lastRoadTileEndRotation = new Vector3 (0.0f, 0.0f, 0.0f);//first road segment rotation in 3d world
	//(0.0f, 0.0f, 0.0f) - the rotation angles where your game begins :)
	Transform lastRoadTileEnd;
		

	
// Use this for initialization
void Start ()
{
		//make first road segment at zero
		loadingRoadPrefab = roadPref_Straight;//it will be straight segment
		GameObject lastRoadTile = Instantiate (loadingRoadPrefab, lastRoadTilePosition, Quaternion.Euler (lastRoadTileEndRotation)) as GameObject;
		lastTile = GameObject.Find ("road_straight(Clone)");//to find a distance between player and this segment
		player ??= GameObject.Find ("player_vehicle");

		roadHardess = Mathf.Clamp (roadHardess,0,100);//roadHardess MUST be between 0 and 100;(maybe someone wiil make it -5 or 4737 by mistake)
		
		Transform forAutoPilot = lastRoadTile.transform.Find ("roadEndCoords");//for AI navigation to know where to drive to
		lastRoadTilePosition = forAutoPilot.transform.position;//for AI navigation
		}
	
		// Update is called once per frame
void Update ()
{
		//to prevent situation when there is no information about coordinates of last road segment to create new segment 
		if (lastTile != null){
			newTileToPlayerDist = Vector3.Distance (lastTile.transform.position, player.transform.position);
		}
		//when it Ok, then proceed...

		//when distance from player less than 80 meters we need to make new road segment(80 is good for demo - change it for your needs)
		if (newTileToPlayerDist < 80.0f) {

			//need to calculate hills chances
			int willItHill = Random.Range (0, hillsPercentage/10);
			int willNot = Random.Range (0, 10-hillsPercentage/10);
			if(willItHill > willNot){//makeing a hill road segment
				hillBuilded = true;
				int upOrDown = Random.Range (0, 2);//will it be climb or downhill(result always is 0 or 1)
				if (lastRoadTilePosition.y > maxRoadHeight){
					upOrDown = 0;
				}
				if (lastRoadTilePosition.y < -maxRoadHeight){
					upOrDown = 1;
				}
				if (upOrDown < 1){//making downhill
					loadingRoadPrefab = roadPref_Down;
				} else {//making climb hill
					loadingRoadPrefab = roadPref_Up;
					}
				makeRoadSegment(loadingRoadPrefab);
			}
			if (!hillBuilded){//hills got low chanses :) so, it will be horizontal road not hill
				//but which one segment of all our prefabs ?
				//it's strange formula but it works really good for 11 segments(1 straight, 2 light, 2 easy, 2 medium, 2 hard, 2 extreme)
				//this formula should make nearest kind of turns with your road curvature number on Inspector's scroller.
				//that's why it looks so strange. Because if you choose, let's say 'curvature 75', you wish to get only turns like hard with very few
				//medium and extreme, but NOT , light, easy.
				int randMax = roadHardess/5;
				int randMin = Mathf.Clamp((roadHardess-55)/5, 0, 50);
				int newRoadTileIs = Random.Range (randMin, randMax);

				//now, assign new road segment according formula results
				if (newRoadTileIs == 0) {
						loadingRoadPrefab = roadPref_Straight;
				} else if (newRoadTileIs == 1) {
						loadingRoadPrefab = roadPrefR_Light;
				} else if (newRoadTileIs == 2) {
						loadingRoadPrefab = roadPrefL_Light;
				} else if (newRoadTileIs == 3) {
						loadingRoadPrefab = roadPrefR_Easy;
				} else if (newRoadTileIs == 4) {
						loadingRoadPrefab = roadPrefL_Easy;
				} else if (newRoadTileIs == 5) {
						loadingRoadPrefab = roadPrefR_Medium;
				} else if (newRoadTileIs == 6) {
						loadingRoadPrefab = roadPrefL_Medium;
				} else if (newRoadTileIs == 7) {
						loadingRoadPrefab = roadPrefR_Hard;
				} else if (newRoadTileIs == 8) {
						loadingRoadPrefab = roadPrefL_Hard;
				} else if (newRoadTileIs == 9) {
						loadingRoadPrefab = roadPrefR_Extreme;
				} else if (newRoadTileIs == 10) {
						loadingRoadPrefab = roadPrefL_Extreme;
				} else
						return;

				//call method of road segment generation
				makeRoadSegment(loadingRoadPrefab);

				//call method of creating aside hills for generated above road segment
				makeSideHills();

				//call method of creating obstacles for generated above road segment
				makeSpoilers();

				//call method of creating aside props for generated above road segment
				makeSideProps();

				}

			}

		hillBuilded = false;
	
	}
	//method of generation of road segment
	private void makeRoadSegment (GameObject loadingRoadPrefab){
		Vector3 position = lastRoadTilePosition;

		//first of all we need to check for horizontal leanng !
		//what ?
		//what is horizontal leaning ? I have no any screwed road segments ! How it possible ?
		//
		//imagine to yourself situation, where just after hill you create turn it will be turn in 3d spece, not only horizontal dimension
		//but in with some skew(incline). Segment after segment, tile after tile it can lean your road by 180 degrees and upside down.
		//you don't want it.
		//so, what's why we cheking leaning angle before everything and if road isn't horizontal we insert fixing by 5 degree segment. 

		//need to know is road still horizontal
		if (lastTile.transform.root.eulerAngles.z > 5 & lastTile.transform.root.eulerAngles.z < 180){
			loadingRoadPrefab = roadPref_LeanR; 
		} 
		if (lastTile.transform.root.eulerAngles.z < 355 & lastTile.transform.root.eulerAngles.z > 180) {
			loadingRoadPrefab = roadPref_LeanL;
		}
		//if road not horizontal we cancel current road segment and change it by fixing segment(roadPref_LeanR or roadPref_LeanL) 


		GameObject lastRoadTile = Instantiate (loadingRoadPrefab, position, Quaternion.Euler (lastRoadTileEndRotation)) as GameObject;
		lastRoadTileEnd = lastRoadTile.transform.Find ("roadEndCoords");
		lastRoadTilePosition = lastRoadTileEnd.transform.position;
		lastRoadTileEndRotation = lastRoadTileEnd.transform.eulerAngles;


		lastTile = lastRoadTile;


		//now time to get chances to create(or not) some traffic
		int willCar = Random.Range (1, 101);

		//if any car is added to traffic array
		if (trafficCount.Count > 0) {
			//creating a vehicle when traffic number in Inspector more than random 'willCar'
			if (willCar <= traffic) {
				int whichLane = Random.Range (0, 2);//is that car on left or right side of road

				position = new Vector3 (position.x, position.y + 2, position.z);

				//which car of cars array should appear ?
				GameObject whichVehicle;
				int arrayLenght = Random.Range (0, trafficCount.Count);
				whichVehicle = trafficCount[arrayLenght];

				//creating a vehicle
				GameObject vehicle = Instantiate (whichVehicle, position, Quaternion.Euler (lastRoadTileEndRotation)) as GameObject;
				vehicle.transform.position = new Vector3 (vehicle.transform.localPosition.x + 2, vehicle.transform.localPosition.y, vehicle.transform.localPosition.z);//сдвигаем машину сразу на свою полосу

				//positioning car on right side and place of the road
				if (whichLane > 0) {
					vehicle.transform.rotation = Quaternion.Euler (vehicle.transform.localEulerAngles.x, vehicle.transform.localEulerAngles.y + 180, vehicle.transform.localEulerAngles.z);//50%, что машина будет встречкой
					vehicle.GetComponent<vehicleScript> ().nameWP_GO = "laneL1";
					vehicle.transform.position = new Vector3 (vehicle.transform.localPosition.x - 4, vehicle.transform.localPosition.y, vehicle.transform.localPosition.z);//сдвигаем машину сразу на свою полосу
				} else
					vehicle.GetComponent<vehicleScript> ().nameWP_GO = "laneR1";

			}
		}

	}

	//method of aside hills and mountains generation
	private void makeSideHills(){
		//if any hill is added to hills array
		if (hillsCount.Count > 0) {

			//hills on the right side(EVERY road segments)
			int newMount = Random.Range (0, 10);
			if (newMount < 11) {//it's ALWAYS hill at right when there is no any hills on the screen the picture seems bad
				//so that's why hill no right side will appear every road segment

				//find point of coordinates of last road segment
				Transform lastRoadTileEnd = lastTile.transform.Find ("laneR1");

				Vector3 placeForMount = new Vector3 (lastRoadTileEnd.position.x, lastRoadTileEnd.position.y, lastRoadTileEnd.position.z);

				//which hill of hills array should appear ??
				GameObject whichMount;
				int arrayLenght = Random.Range (0, hillsCount.Count);
				whichMount = hillsCount[arrayLenght];

				//creating hill prefab
				GameObject mount = Instantiate (whichMount, placeForMount, Quaternion.Euler (lastRoadTileEndRotation)) as GameObject;
			
				//put this hill to road segment GameObject to destroy it by one string of script
				mount.transform.parent = lastTile.transform;

				mount.transform.Translate (Vector3.right * Random.Range (40, 80));//move right by random distance
				mount.transform.Rotate (Vector3.up * Random.Range (0, 360));//turn by random
				mount.transform.localScale = new Vector3 (Random.Range (0.7f, 1.2f), Random.Range (0.7f, 1.2f), Random.Range (0.7f, 1.2f));//scale by random
			}

			newMount = Random.Range (0, 10);//now same for left hills
			if (newMount < 6) {// there is chance to left side with no hills ...too much hills is bad too
				Transform lastRoadTileEnd = lastTile.transform.Find ("laneL1");
			
				Vector3 placeForMount = new Vector3 (lastRoadTileEnd.position.x, lastRoadTileEnd.position.y, lastRoadTileEnd.position.z);

				//which hill of hills array should appear ??
				GameObject whichMount;
				int arrayLenght = Random.Range (0, hillsCount.Count);
				whichMount = hillsCount[arrayLenght];
				
				GameObject mount = Instantiate (whichMount, placeForMount, Quaternion.Euler (lastRoadTileEndRotation)) as GameObject;

				mount.transform.parent = lastTile.transform;
			
				mount.transform.Translate (Vector3.right * Random.Range (-40, -80));
				mount.transform.Rotate (Vector3.up * Random.Range (0, 360));
				mount.transform.localScale = new Vector3 (Random.Range (0.7f, 1.2f), Random.Range (0.7f, 1.2f), Random.Range (0.7f, 1.2f));

			}
		}
	}
	

	//method of road obstacles genaration
	private void makeSpoilers(){
		//if any obstacle is added to the obstacles array
		if (obstaclesCount.Count > 0) {

			//now time to get chances to create(or not) some obstacles
			int chanceToObstacle = Random.Range (1, 101);

			//creating a obstacle when spoilers number in Inspector more than random 'chanceToSpoil'
			if (chanceToObstacle <= spoilers) {

				//finding last road segment
				Transform lastRoadTileEnd = lastTile.transform.Find ("laneR1");

				//choosing left or right side of road
				int leftOrRight = Random.Range (0, 2);
				if (leftOrRight < 1) {
					lastRoadTileEnd = lastTile.transform.Find ("laneL1");
				}

				Vector3 placeForSpoilers = new Vector3 (lastRoadTileEnd.position.x, lastRoadTileEnd.position.y + 5, lastRoadTileEnd.position.z);

				//which obstalce of obstacles array should appear ?
				GameObject whichObstacle;
				int arrayLenght = Random.Range (0, obstaclesCount.Count);
				whichObstacle = obstaclesCount[arrayLenght];


				//create and put GameObject obstace on road
				GameObject spoiler = Instantiate (whichObstacle, placeForSpoilers, Quaternion.Euler (lastRoadTileEndRotation)) as GameObject;
				spoiler.transform.parent = lastTile.transform;
				spoiler.transform.Translate (Vector3.up * 2);

			}
		}
	}

	//method of aside props genaration
	private void makeSideProps(){
		//if any sideProps is added to the props array
		if (propsCount.Count > 0) {

			//now time to get chances to create(or not) some props
			int chanceToProps = Random.Range (1, 101);

			//creating a props when sideProps number in Inspector more than random 'chanceToProps'
			if (chanceToProps <= sideProps) {

				//finding last road segment
				Transform lastRoadTileEnd = lastTile.transform.Find ("laneR1");
			
				Vector3 placeForProps = new Vector3 (lastRoadTileEnd.position.x, lastRoadTileEnd.position.y, lastRoadTileEnd.position.z);

				//which sideprop of props array should appear ?
				GameObject whichProp;
				int arrayLenght = Random.Range (0, propsCount.Count);
				whichProp = propsCount[arrayLenght];

				GameObject props = Instantiate (whichProp, placeForProps, Quaternion.Euler (lastRoadTileEndRotation)) as GameObject;
				props.transform.parent = lastTile.transform;

				//moving props left or right to the side of the road randomly
				int leftOrRight = Random.Range (0, 2);
				if (leftOrRight < 1) {
					props.transform.Translate (Vector3.right * Random.Range (7, 10));
				} else
					props.transform.Translate (Vector3.right * Random.Range (-12, -15));
			}
		}
	}
}
