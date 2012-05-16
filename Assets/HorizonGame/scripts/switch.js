function OnTriggerEnter(other : Collider) {
    GameObject.Find("Rising Platform").gameObject.animation.Play();
}