using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Free Cam")]
internal class FreeCam : MonoBehaviour
{

    public RotationAxes axes;
    public float maximumX = 360f;
    public float maximumY = 60f;
    public float minimumX = -360f;
    public float minimumY = -60f;
    private Quaternion originalRotation;
    private float rotationX;
    private float rotationY;
    public float sensitivityX = 15f;
    public float sensitivityY = 15f;
    public float speed = 50f;


    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f)
        {
            angle += 360f;
        }
        if (angle > 360f)
        {
            angle -= 360f;
        }
        return Mathf.Clamp(angle, min, max);
    }

    private void Start()
    {
        if (base.GetComponent<Rigidbody>() != null)
        {
            base.GetComponent<Rigidbody>().freezeRotation = true;
        }
        this.originalRotation = base.transform.localRotation;
    }

    private void Update()
    {
        if (this.axes == RotationAxes.MouseXAndY)
        {
            this.rotationX += Input.GetAxis("Mouse X") * this.sensitivityX;
            this.rotationY += Input.GetAxis("Mouse Y") * this.sensitivityY;
            this.rotationX = ClampAngle(this.rotationX, this.minimumX, this.maximumX);
            this.rotationY = ClampAngle(this.rotationY, this.minimumY, this.maximumY);
            Quaternion quaternion = Quaternion.AngleAxis(this.rotationX, Vector3.up);
            Quaternion quaternion2 = Quaternion.AngleAxis(this.rotationY, Vector3.left);
            base.transform.localRotation = (this.originalRotation * quaternion) * quaternion2;
        }
        else if (this.axes == RotationAxes.MouseX)
        {
            this.rotationX += Input.GetAxis("Mouse X") * this.sensitivityX;
            this.rotationX = ClampAngle(this.rotationX, this.minimumX, this.maximumX);
            Quaternion quaternion3 = Quaternion.AngleAxis(this.rotationX, Vector3.up);
            base.transform.localRotation = this.originalRotation * quaternion3;
        }
        else
        {
            this.rotationY += Input.GetAxis("Mouse Y") * this.sensitivityY;
            this.rotationY = ClampAngle(this.rotationY, this.minimumY, this.maximumY);
            Quaternion quaternion4 = Quaternion.AngleAxis(this.rotationY, Vector3.left);
            base.transform.localRotation = this.originalRotation * quaternion4;
        }
        if (Input.GetAxis("Vertical") > 0f)
        {
            Transform transform = base.transform;
            transform.position += (Vector3) ((base.transform.forward * this.speed) * Time.deltaTime);
        }
        else if (Input.GetAxis("Vertical") < 0f)
        {
            Transform transform2 = base.transform;
            transform2.position += (Vector3) ((base.transform.forward * -this.speed) * Time.deltaTime);
        }
        else if (Input.GetAxis("Horizontal") > 0f)
        {
            Transform transform3 = base.transform;
            transform3.position += (Vector3) ((base.transform.right * this.speed) * Time.deltaTime);
        }
        else if (Input.GetAxis("Horizontal") < 0f)
        {
            Transform transform4 = base.transform;
            transform4.position += (Vector3) ((-base.transform.right * this.speed) * Time.deltaTime);
        }
    }


    public enum RotationAxes
    {
        MouseXAndY,
        MouseX,
        MouseY
    }
}

