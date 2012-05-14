var followMe : Transform;

function Update () {
	if(followMe != null){
		transform.position.x = followMe.position.x;
		transform.position.z = followMe.position.z;
	}else
		Destroy(this.gameObject);
}

function setFollower(follower : Transform){
	followMe = follower;
}