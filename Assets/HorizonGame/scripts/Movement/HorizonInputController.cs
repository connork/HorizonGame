using UnityEngine;
using System.Collections;
using System;


[Serializable]
public class HorizonMovementMode
{
    public GameObject movementObject;
    public String buttonName;
    

}



public class HorizonInputController : MonoBehaviour {

    public Transform startPoint;

    Transform followTarget;

    public HorizonMovementMode[] MovementModes;

    protected HorizonMovementMode currentMode;

    private Vector2 lastMouse = Vector2.zero;

    public int followDistance = 10;

    public float xSpeed = 250.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = -20;
    public float yMaxLimit = 80;

    private float x = 0.0f;
    private float y = 0.0f;

    Vector3 movementVector;

    GameObject obj;

	// Use this for initialization
	void Start () {
        currentMode = MovementModes[0];

        obj = (GameObject)GameObject.Instantiate(currentMode.movementObject, startPoint.position, startPoint.rotation);
        followTarget = obj.transform;
        //gameObject.GetComponent<SmoothFollow>().target = obj.transform;
        obj.GetComponent<BaseCharacterController>().setInputController(this);

        lastMouse = new Vector2(Screen.width / 2, Screen.height / 2);

        x = transform.eulerAngles.x;
        y = transform.eulerAngles.y;

	}
	
	// Update is called once per frame
	void Update () {

        movementVector = new Vector3(Input.GetAxis("Vertical"), 0, -Input.GetAxis("Horizontal"));

        HandleKeypress();
        HandleMouseMovement();
	}



    public void ChangeMode(HorizonMovementMode mode)
    {
       
        Transform currentLocation = obj.transform;

        GameObject.Destroy(obj);

        obj = (GameObject)GameObject.Instantiate(mode.movementObject, currentLocation.position, currentLocation.rotation);
        followTarget = obj.transform;
        obj.GetComponent<BaseCharacterController>().setInputController(this);

        currentMode = mode;
    }

    public void HandleKeypress()
    {
        foreach (HorizonMovementMode mode in MovementModes)
        {
            if (mode == currentMode) continue;
            if (Input.GetButtonDown(mode.buttonName))
            {
                ChangeMode(mode);
                return;
            }
        }


    }


    public Vector3 getMovementVector()
    {
        return movementVector;
    }


    public void HandleMouseMovement()
    {
        x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
        y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

        y = ClampAngle(y, yMinLimit, yMaxLimit);

        Quaternion rotation = Quaternion.Euler(y, x, 0);
        Vector3 position = rotation * new Vector3(0.0f, 0.0f, -followDistance) + followTarget.position;

        transform.rotation = rotation;
        transform.position = position;
    }

    public static float WrapAngle(float angle)
    {
        while (angle < -360)
            angle += 360;
        while (angle > 360)
            angle -= 360;
        return angle;
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        angle = WrapAngle(angle);
        return Mathf.Clamp(angle, min, max);
    }
}
