using UnityEngine;
using System.Collections;

public class WalkingCharacterController : MonoBehaviour {
    public Vector3 walkingSpeed;

    AnimationCurve test;



	// Use this for initialization
	void Start () {
        Quaternion rot = Quaternion.AngleAxis(40, new Vector3(0, 1, 0));
       
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
