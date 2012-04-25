using UnityEngine;
using System.Collections;

public class WindPressureVolume : MonoBehaviour {

    public Vector3 forceDirection = Vector3.zero;
    public ForceMode forceMode = ForceMode.Acceleration;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collision)
    {
        GameObject collidingObject = collision.gameObject;
        Rigidbody phys = collidingObject.rigidbody;
        if (phys == null) return;

        phys.AddForce(forceDirection, forceMode);
    }
}
