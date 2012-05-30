using UnityEngine;
using System.Collections;

public class PhysicsCharacterController : BaseCharacterController {
    Rigidbody phys;
    float maxmimumRollChangeVelocity = 10.0f;
	
	public float raycastLength = 2.5f;

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
        movement = Camera.main.transform.TransformDirection(movement);
		
		RaycastHit hit;
		if(Physics.Raycast(transform.position, -Vector3.up, out hit, raycastLength))
		{
			UpdateMovement(movement);
		}
		

	}

    void UpdateMovement(Vector3 input)
    {
			phys.AddTorque(input, forceMode);

		
        
    }

    public override bool CanChangeMode()
    {
        if (phys.velocity.magnitude >= maxmimumRollChangeVelocity)
        {
            return false;
        }
        return true;
    }
	
	
}
