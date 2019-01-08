var moveThis : GameObject;
var hit : RaycastHit;
var createThis : GameObject[];
var cooldown : float;
var changeCooldown : float;
var selected:int=0;
var writeThis:GUIText;
private var rndNr:float;
function Start () {
writeThis.text=selected.ToString();
}

function Update () {
if(cooldown>0){cooldown-=Time.deltaTime;}
if(changeCooldown>0){changeCooldown-=Time.deltaTime;}

var ray = Camera.main.ScreenPointToRay (Input.mousePosition);

if (Physics.Raycast (ray, hit)) {
// Create a particle if hit
moveThis.transform.position=hit.point;

if(Input.GetMouseButton(0)&&cooldown<=0){
Instantiate(createThis[selected], moveThis.transform.position, moveThis.transform.rotation);


/*rndNr=Mathf.Floor(Random.value*createThis.length);
Instantiate(createThis[rndNr], moveThis.transform.position, moveThis.transform.rotation);
moveThis.transform.position.x+=Random.value*12-Random.value*12;


rndNr=Mathf.Floor(Random.value*createThis.length);
Instantiate(createThis[rndNr], moveThis.transform.position, moveThis.transform.rotation);
moveThis.transform.position.x+=Random.value*12-Random.value*12;



rndNr=Mathf.Floor(Random.value*createThis.length);
Instantiate(createThis[rndNr], moveThis.transform.position, moveThis.transform.rotation);
moveThis.transform.position.x+=Random.value*12-Random.value*12;*/




cooldown=0.1;
}



//Instantiate (particle, hit.point, transform.rotation);

}


if (Input.GetKeyDown("space") && changeCooldown<=0)
{
	selected+=1;
		if(selected>(createThis.length-1)) {selected=0;}
	
	writeThis.text=selected.ToString();
	changeCooldown=0.1;
}

if (Input.GetKeyDown(KeyCode.UpArrow) && changeCooldown<=0)
{
	selected+=1;
		if(selected>(createThis.length-1)) {selected=0;}
	
	writeThis.text=selected.ToString();
	changeCooldown=0.1;
}

if (Input.GetKeyDown(KeyCode.DownArrow) && changeCooldown<=0)
{
	selected-=1;
		if(selected<0) {selected=createThis.length-1;}
	
	writeThis.text=selected.ToString();
	changeCooldown=0.1;
}




}