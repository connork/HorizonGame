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

    public HorizonMovementMode[] MovementModes;

    protected HorizonMovementMode currentMode;


    Vector3 movementVector;

    GameObject obj;

	// Use this for initialization
	void Start () {
        currentMode = MovementModes[0];

        obj = (GameObject)GameObject.Instantiate(currentMode.movementObject, startPoint.position, startPoint.rotation);
        gameObject.GetComponent<SmoothFollow>().target = obj.transform;
        obj.GetComponent<BaseCharacterController>().setInputController(this);
	}
	
	// Update is called once per frame
	void Update () {
        movementVector = new Vector3(Input.GetAxis("Vertical"), 0, -Input.GetAxis("Horizontal"));

        HandleKeypress();
	}


    public void ChangeMode(HorizonMovementMode mode)
    {
       
        Transform currentLocation = obj.transform;

        GameObject.Destroy(obj);

        obj = (GameObject)GameObject.Instantiate(mode.movementObject, currentLocation.position, currentLocation.rotation);
        gameObject.GetComponent<SmoothFollow>().target = obj.transform;
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
}
