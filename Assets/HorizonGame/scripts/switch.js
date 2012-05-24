function OnTriggerEnter(other : Collider) {
    GameObject.Find("Platform3").gameObject.animation.Play("Rise1");
    pausecomp("10");
    GameObject.Find("Platform3").gameObject.animation.Play("FloatingPlatform3");
}

function pausecomp(ms) {
    ms += new Date().getTime();
    while (new Date() < ms){}
} 