using UnityEngine;
using System.Collections;

public class BaseCharacterController : MonoBehaviour {

    protected HorizonInputController inputController = null;


    protected void setInputController(HorizonInputController controller)
    {
        this.inputController = controller;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
