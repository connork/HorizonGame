function OnTriggerEnter(other : Collider) {
  	GameObject.Find("Platform4").gameObject.animation.Play("Rise2");
} 