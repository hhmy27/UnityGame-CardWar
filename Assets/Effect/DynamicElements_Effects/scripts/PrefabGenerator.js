var createThis:GameObject[];  // list of possible prefabs

private var rndNr:float; // this is for just a random number holder when we need it

var thisManyTimes:int=3;
var overThisTime:float=1.0;

var xWidth:float;  // define the square where prefabs will be generated
var yWidth:float;
var zWidth:float;

var xRotMax:float;  // define maximum rotation of each prefab
var yRotMax:float=180;
var zRotMax:float;

var allUseSameRotation:boolean=false;
private var allRotationDecided:boolean=false;

var detachToWorld:boolean=true;

private var x_cur:float;  // these are used in the random palcement process
private var y_cur:float;
private var z_cur:float;


private var xRotCur:float;  // these are used in the random protation process
private var yRotCur:float;
private var zRotCur:float;

private var timeCounter:float;  // counts the time :p
private var effectCounter:int;  // you will guess ti

private var trigger:float;  // trigger: at which interwals should we generate a particle



function Start () {

trigger=overThisTime/thisManyTimes;  //define the intervals of time of the prefab generation.

}


function Update () {

timeCounter+=Time.deltaTime;

	if(timeCounter>trigger&&effectCounter<=thisManyTimes)
		{
		rndNr=Mathf.Floor(Random.value*createThis.length);  //decide which prefab to create


		x_cur=transform.position.x+(Random.value*xWidth)-(xWidth*0.5);  // decide an actual place
		y_cur=transform.position.y+(Random.value*yWidth)-(yWidth*0.5);
		z_cur=transform.position.z+(Random.value*zWidth)-(zWidth*0.5);

		if(allUseSameRotation==false||allRotationDecided==false)  // basically this plays only once if allRotationDecided=true, otherwise it plays all the time
		{
		xRotCur=transform.rotation.x+(Random.value*xRotMax*2)-(xRotMax);  // decide rotation
		yRotCur=transform.rotation.y+(Random.value*yRotMax*2)-(yRotMax);  
		zRotCur=transform.rotation.z+(Random.value*zRotMax*2)-(zRotMax);  
		allRotationDecided=true;
		}

		//var justCreated:GameObject=Instantiate(createThis[rndNr], Vector3(x_cur, y_cur, z_cur), Vector3(xRotCur, yRotCur, zRotCur));  //create the prefab
		var justCreated:GameObject=Instantiate(createThis[rndNr], Vector3(x_cur, y_cur, z_cur), transform.rotation);  //create the prefab
		justCreated.transform.Rotate(xRotCur, yRotCur, zRotCur);
		
			if(detachToWorld==false)  // if needed we attach the freshly generated prefab to the object that is holding this script
			{
			justCreated.transform.parent=transform;
			}
		
		timeCounter-=trigger;  //administration :p
		effectCounter+=1;
		}


}