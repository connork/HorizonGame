#pragma strict

var worldLightOn = false;
var ambientColorDark : Color = Color( 0.15, 0.15, 0.25, 1.0 );
var ambientColorBright : Color = Color( 0.2, 0.2, 0.2, 1.0 );
var sunlightColorDark : Color = Color( 0.6, 0.6, 1.0, 1.0 );
var sunlightColorBright : Color = Color( 1.0, 1.0, 1.0, 1.0 );
var sunlightIntensityDark : float = 0.2;
var sunlightIntensityBright : float = 0.75;

// Level Names:
// Outside World Developed	1
// Outside World Undeveloped	2
// Light Tower			3
// Wind Tower			4
// Greenhouse Tower		5

function Start () {
	// Set the initial lighting values.
	var sunlight : GameObject = GameObject.Find("Sunlight");
	if( worldLightOn ){
		RenderSettings.ambientLight = ambientColorBright;
		sunlight.light.color = sunlightColorBright;
		sunlight.light.intensity = sunlightIntensityBright;
	}
	else
	{
		RenderSettings.ambientLight = ambientColorDark;
		sunlight.light.color = sunlightColorDark;
		sunlight.light.intensity = sunlightIntensityDark;
	}

}

function Update () {
		var sunlight : GameObject = GameObject.Find("Sunlight");
		if( worldLightOn ){
			RenderSettings.ambientLight = ambientColorBright;
			sunlight.light.color = sunlightColorBright;
			sunlight.light.intensity = sunlightIntensityBright;
		}
		else
		{
			RenderSettings.ambientLight = ambientColorDark;
			sunlight.light.color = sunlightColorDark;
			sunlight.light.intensity = sunlightIntensityDark;
		}
}

function ActivateWorldLight()
{
	worldLightOn = true;
}