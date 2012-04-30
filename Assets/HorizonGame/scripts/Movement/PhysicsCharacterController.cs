using UnityEngine;
using System.Collections;

public class PhysicsCharacterController : BaseCharacterController {
    Rigidbody phys;

    public float forwardMultiplier = 10;
    public float horizontalMultiplier = 10;
    public ForceMode forceMode = ForceMode.Acceleration;

	// Use this for initialization
	void Start () {
        phys = gameObject.rigidbody;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 movement = new Vector3(inputController.getMovementVector().x * forwardMultiplier, 0, inputController.getMovementVector().z * horizontalMultiplier);
 
        UpdateMovement(movement);

	}

    void UpdateMovement(Vector3 input)
    {
        phys.AddTorque(input, forceMode);
       
        
    }
}
