function OnTriggerEnter(other : Collider) {
    GameObject.Find("Platform3").gameObject.animation.Play("rise");
}