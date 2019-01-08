var translationSpeedX:float=0;
var translationSpeedY:float=1;
var translationSpeedZ:float=0;

var translationSpeedXRnd:float;
var translationSpeedYRnd:float;
var translationSpeedZRnd:float;

var forceX:float;
var forceY:float;
var forceZ:float;

var forceXRnd:float;
var forceYRnd:float;
var forceZRnd:float;

var gravity:float=-10;

var dampening:float=1;

private var dampeningTargetAtNextSecond:float;
private var dampeningTime:float=0;

var speedTreshold:float=0.001;


private var gravityAdd:float;

function Start () {
forceX+=(Random.value*forceXRnd)-(forceXRnd/2);
forceY+=(Random.value*forceYRnd)-(forceYRnd/2);
forceZ+=(Random.value*forceZRnd)-(forceZRnd/2);

translationSpeedX+=(translationSpeedXRnd*Random.value)-(translationSpeedXRnd/2);
translationSpeedY+=(translationSpeedYRnd*Random.value)-(translationSpeedYRnd/2);
translationSpeedZ+=(translationSpeedZRnd*Random.value)-(translationSpeedZRnd/2);
}


function Update () {

if(Mathf.Abs(translationSpeedX)<speedTreshold) translationSpeedX=0;
if(Mathf.Abs(translationSpeedY)<speedTreshold) translationSpeedY=0;
if(Mathf.Abs(translationSpeedZ)<speedTreshold) translationSpeedZ=0;

translationSpeedX+=Time.deltaTime*forceX;
translationSpeedY+=Time.deltaTime*forceY;
translationSpeedZ+=Time.deltaTime*forceZ;

dampeningTime+=Time.deltaTime;

dampeningTargetAtNextSecond=translationSpeedX*dampening;
translationSpeedX=Mathf.Lerp(translationSpeedX, dampeningTargetAtNextSecond, dampeningTime);

dampeningTargetAtNextSecond=translationSpeedY*dampening;
translationSpeedY=Mathf.Lerp(translationSpeedY, dampeningTargetAtNextSecond, dampeningTime);

dampeningTargetAtNextSecond=translationSpeedZ*dampening;
translationSpeedZ=Mathf.Lerp(translationSpeedZ, dampeningTargetAtNextSecond, dampeningTime);


transform.Translate(Vector3(translationSpeedX,translationSpeedY,translationSpeedZ)*Time.deltaTime);




gravityAdd+=gravity*Time.deltaTime;
transform.Translate(Vector3(0, gravityAdd, 0)*Time.deltaTime, Space.World);

}