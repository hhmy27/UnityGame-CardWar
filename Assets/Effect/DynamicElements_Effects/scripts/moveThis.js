var translationSpeedX:float=0;
var translationSpeedY:float=1;
var translationSpeedZ:float=0;






function Update () {



transform.Translate(Vector3(translationSpeedX,translationSpeedY,translationSpeedZ)*Time.deltaTime);

}