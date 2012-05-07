using UnityEngine;
using System.Collections;

public abstract class BaseCharacterController : MonoBehaviour {

    protected HorizonInputController inputController = null;


    public void setInputController(HorizonInputController controller)
    {
        this.inputController = controller;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public abstract bool CanChangeMode();
}
