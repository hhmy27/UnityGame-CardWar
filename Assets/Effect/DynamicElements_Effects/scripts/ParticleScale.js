
var particleScale:float=1;  


function Start () {

GetComponent.<ParticleEmitter>().minSize*=particleScale;
GetComponent.<ParticleEmitter>().maxSize*=particleScale;
GetComponent.<ParticleEmitter>().worldVelocity*=particleScale;
GetComponent.<ParticleEmitter>().localVelocity*=particleScale;
GetComponent.<ParticleEmitter>().rndVelocity*=particleScale;
GetComponent.<ParticleEmitter>().angularVelocity*=particleScale;
GetComponent.<ParticleEmitter>().rndAngularVelocity*=particleScale;	


}

function Update () {


}