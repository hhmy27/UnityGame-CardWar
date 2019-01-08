//Light_Flicker.js

var time : float = .2;
var min : float = .5;
var max : float = 5;
var useSmooth = false;
var smoothTime : float = 10;

function Start () {
	if(useSmooth == false && GetComponent.<Light>()){
	InvokeRepeating("OneLightChange", time, time);
	}
}
function OneLightChange () {
	GetComponent.<Light>().intensity = Random.Range(min,max);

}

function Update () {
	if(useSmooth == true && GetComponent.<Light>()){
		GetComponent.<Light>().intensity = Mathf.Lerp(GetComponent.<Light>().intensity,Random.Range(min,max),Time.deltaTime*smoothTime);
	}
	if(GetComponent.<Light>() == false){
		print("Please add a light component for light flicker");
	}
}