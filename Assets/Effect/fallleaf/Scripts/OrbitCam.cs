using UnityEngine;
using System.Collections;

//Decompile by Si Borokokok

[AddComponentMenu("Camera-Control/Mouse Orbit")]

public class OrbitCam : MonoBehaviour
{
    public float distance = 10f;
    public Transform target;
    private float x;
    public float xSpeed = 250f;
	public float ySpeed = 120f;
    private float y;
    public int yMaxLimit = 80;
    public int yMinLimit = -20;

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
        {
            angle += 360;
        }
        if (angle > 360)
        {
            angle -= 360;
        }
        return Mathf.Clamp(angle, min, max);
    }

    public void LateUpdate()
    {
        if (target != null)
        {
            x += (Input.GetAxis("Mouse X") * xSpeed) * 0.02f;
            y -= (Input.GetAxis("Mouse Y") * ySpeed) * 0.02f;
            y = ClampAngle(y, (float) yMinLimit, (float) yMaxLimit);
            Quaternion quaternion = Quaternion.Euler(y, x, (float) 0);
            Vector3 vector = ((Vector3) (quaternion * new Vector3((float) 0, (float) 0, -distance))) + target.position;
            transform.rotation = quaternion;
            transform.position = vector;
        }
    }


    public void Start()
    {
        Vector3 eulerAngles = transform.eulerAngles;
        x = eulerAngles.y;
        y = eulerAngles.x;
        if (GetComponent<Rigidbody>() != null)
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }
    }
}

 

