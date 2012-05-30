#pragma strict

function Start () {

}

function Update () {

}

function OnTriggerEnter( other : Collider )
{
	GameObject.Find("WorldLightController").GetComponent(WorldLightControl).ActivateWorldLight();
}