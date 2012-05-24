private var done = false;

function Update () {
    if(animation.isPlaying && !done)
        done = true;
    if(!animation.isPlaying && done)
    	playNextAnimation();
}

function playNextAnimation(){
    animation.CrossFade("FloatingPlatform3");
}