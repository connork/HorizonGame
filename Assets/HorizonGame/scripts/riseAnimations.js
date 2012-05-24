private var done = false;

function Update () {
    //if(!animation.isPlaying && !done){
        playNextAnimation();
    //    done = true;
   // }
}

function playNextAnimation(){
    animation.Play("FloatingPlatform3");
}