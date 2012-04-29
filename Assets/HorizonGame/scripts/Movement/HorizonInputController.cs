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



	// Use this for initialization
	void Start () {
        currentMode = MovementModes[0];

        GameObject obj = (GameObject)GameObject.Instantiate(currentMode.movementObject, startPoint.position, startPoint.rotation);
        gameObject.GetComponent<SmoothFollow>().target = obj.transform;
	}
	
	// Update is called once per frame
	void Update () {
        movementVector = new Vector3(Input.GetAxis("Vertical"), 0, -Input.GetAxis("Horizontal"));


	}
}
