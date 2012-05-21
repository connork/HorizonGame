using UnityEngine;
using System.Collections;
using System;


[Serializable]
public class HorizonMovementMode
{
    public GameObject movementObject;
    public String buttonName;
    public float cameraDistance = 10;
    public bool hideModel = false;
}



public class HorizonInputController : MonoBehaviour {

    public Transform startPoint;

    Transform followTarget;

    public HorizonMovementMode[] MovementModes;

    protected HorizonMovementMode currentMode;

    private Vector2 lastMouse = Vector2.zero;

    private float followDistance = 10;
    private float targetDistance = 10;
    private float previousDistance = 10;

    public float xSpeed = 250.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = -20;
    public float yMaxLimit = 80;

    private float x = 0.0f;
    private float y = 0.0f;

    Vector3 movementVector;

    GameObject obj;

    bool changingCamera = false;

    public AnimationCurve FollowGraph = new AnimationCurve();
    public float ChangeTime = 2.0f;
    private float changeTimer = 2.0f;

	// Use this for initialization
	void Start () {
        currentMode = MovementModes[0];
        targetDistance = currentMode.cameraDistance;
        obj = (GameObject)GameObject.Instantiate(currentMode.movementObject, startPoint.position, startPoint.rotation);
        followTarget = obj.transform;
        //gameObject.GetComponent<SmoothFollow>().target = obj.transform;
        obj.GetComponent<BaseCharacterController>().setInputController(this);
        followDistance = currentMode.cameraDistance;
        lastMouse = new Vector2(Screen.width / 2, Screen.height / 2);

        x = transform.eulerAngles.x;
        y = transform.eulerAngles.y;

	}
	
	// Update is called once per frame
	void Update () {
        if (changingCamera)
        {
            changeTimer -= Time.deltaTime;
            float dt = 1.0f;
            if (changeTimer >= 0)
            {
                dt = (ChangeTime - changeTimer) / ChangeTime;

            }
            else
            {
                changingCamera = false;
                if (currentMode.hideModel)
                {
                    obj.GetComponentInChildren<Renderer>().enabled = false;
                }
            }
            followDistance = Mathf.Lerp(previousDistance, targetDistance, FollowGraph.Evaluate(dt));


        }
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
        previousDistance = currentMode.cameraDistance;
       
        currentMode = mode;
        targetDistance = currentMode.cameraDistance;

        changeTimer = ChangeTime;
        changingCamera = true;

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

    public bool GetJump()
    {
        return Input.GetButton("Jump");
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
