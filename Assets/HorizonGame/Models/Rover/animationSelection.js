#pragma strict

private var speed:float = 1.0;
private var selectedAnimation:int = 0;
private var time:float = 10.0;

function Start ()
{
   animation.wrapMode = WrapMode.Once;
   animation["roll"].layer = 1;
   animation["jump"].layer = 1;
   animation["jump"].speed = 3;
   animation["grasp"].layer = 1;
   animation["recoil"].layer = 1;
   animation.Stop();
}

function Update () {

	time += Time.deltaTime;
   if (Input.GetKeyDown("1")){
	   if(selectedAnimation == 1){
	   		selectedAnimation = 0;
	   		animation["roll"].speed = -speed;
	   		animation["roll"].time = animation["roll"].length;
	   	}else{
	   		animation["roll"].speed = speed;
	   		selectedAnimation = 1;
	   	}
	   	if(animation.IsPlaying("idle1") || animation.IsPlaying("idle2") || animation.IsPlaying("idle3"))
	   		animation.Stop();
		animation.Play("roll");
   }
   if (Input.GetKeyDown("2")){
	   if(selectedAnimation == 2){
	   		selectedAnimation = 0;
	   		animation["grasp"].speed = -speed;
	   		animation["grasp"].time = animation["grasp"].length;
	   	}else{
	   		animation["grasp"].speed = speed;
	   		selectedAnimation = 2;
	   	}
	   	if(animation.IsPlaying("idle1") || animation.IsPlaying("idle2") || animation.IsPlaying("idle3"))
	   		animation.Stop();
		animation.Play("grasp");
   }
   if (Input.GetKeyDown("3") && selectedAnimation == 2){
   		if(animation.isPlaying)
	   		WaitForSeconds(1);
   		animation.CrossFadeQueued("recoil");
   }
   if (Input.GetKeyDown("4") && selectedAnimation == 0){
   		if(animation.isPlaying)
	   		WaitForSeconds(1);
   		animation.CrossFade("jump");
   }
   if(!animation.isPlaying && selectedAnimation == 0 && time > 10){
   		time = 0;
      	if(Random.value < 0.3)
     		animation.Play("idle1");
     	else if(Random.value > 0.7)
     		animation.Play("idle2");
     	else
     		animation.Play("idle3");
   }
}