var rotationSpeedX:float=90;
var rotationSpeedY:float=0;
var rotationSpeedZ:float=0;
var rotationVector:Vector3=Vector3(rotationSpeedX,rotationSpeedY,rotationSpeedZ);



function Update () {


/*transform.Rotate(Vector3.x*Time.deltaTime*rotationSpeedX);
transform.Rotate(Vector3.y*Time.deltaTime*rotationSpeedY);
transform.Rotate(Vector3.z*Time.deltaTime*rotationSpeedZ);*/

transform.Rotate(Vector3(rotationSpeedX,rotationSpeedY,rotationSpeedZ)*Time.deltaTime);

}