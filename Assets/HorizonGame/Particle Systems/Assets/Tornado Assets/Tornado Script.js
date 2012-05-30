#pragma strict

function Start () {

}

var revolutionsPerSecond : float = 2.5;
var tornadoScale : float = 0.5;

function Update () {
	transform.localScale = Vector3( tornadoScale,tornadoScale,tornadoScale );
	transform.Rotate(Vector3.up * Time.deltaTime * revolutionsPerSecond * 360.0, Space.World);
}