using UnityEngine;
using System.Collections;

public class WalkingCharacterController : BaseCharacterController {
    public Vector3 walkingSpeed;

    AnimationCurve test;



	// Use this for initialization
	void Start () {
      
       
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, inputController.transform.eulerAngles.y, this.transform.eulerAngles.z);
        Vector3 movement = new Vector3(-inputController.getMovementVector().z, 0, inputController.getMovementVector().x);
        movement.Scale(walkingSpeed);
        Debug.Log(movement);
        this.transform.Translate(movement);
	}

    public override bool CanChangeMode()
    {
        return true;
    }
}
