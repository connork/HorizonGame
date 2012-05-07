using UnityEngine;
using System.Collections;

public class ChangeLevelTrigger : MonoBehaviour {

    public string LevelName = "";

	// Use this for initialization
	void Start () {
	   
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        Application.LoadLevel(LevelName);
    }

}
