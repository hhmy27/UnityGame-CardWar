var activateThis:GameObject;
var delay:float=2;
private var delayTime:float=0;

function Update () {
delayTime+=Time.deltaTime;
if(delayTime>delay) activateThis.SetActiveRecursively(true);

}