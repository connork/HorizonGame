using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour {

    public Font game_font;

    public float mainMenu = Screen.height;
    private bool loadMain = false;
    private bool isClick = true;
	private bool showCredits = false;
	
	// The array of GUIText elements to display and scroll
	public GUIText[] textElements;
	
	// The delay time before displaying the GUIText elements
    public float displayTime = 5.0f;
	// The delay time before starting the GUIText scroll
    public float scrollTime = 5.0f;
	// The scrolling speed
    public float scrollSpeed = 0.2f;
	
	
	void Update () 
	{
    
        if(mainMenu != 200.0f){
            mainMenu += (4.0f);
        }
	
		if(showCredits==true){
			displayTime -= Time.deltaTime;

			if (displayTime < 0)
			{
				// if it is time to display, start the scrolling count down timer	
				scrollTime -= Time.deltaTime;
			}         
			// if it is time to scroll, cycle through the GUIElements and
					// increase their Y position by the desired speed
			if (scrollTime < 0)
			{
				foreach (GUIText text in textElements)
				{
					text.transform.Translate(Vector3.up * scrollSpeed);
				}
			}
		}		
	}//end update
	
		
    //GUI Block
    void OnGUI() {

        //GUI.skin.font = game_font;
        if(isClick){
			if(GUI.Button(new Rect(((Screen.width /4) + 100), mainMenu, ((Screen.width / 2) - 200), 20), "Click here to begin your journy"))
			{ 
				loadMain =true; 
				isClick =false;
			}
        }
		
		if(loadMain==true)
        {
		
            //make background box
            //200
            GUI.Box(new Rect((Screen.width / 4), mainMenu, (Screen.width / 2), 240), "Main Menu");

            //make "Continue Journey" button
            //240
            if (GUI.Button(new Rect(((Screen.width / 4) + 100), mainMenu + 40, ((Screen.width / 2) - 200), 20),     "Continue Journey")) {
                Application.LoadLevel(1);
            }

            //make "New Journey" button
            //280
            if (GUI.Button(new Rect(((Screen.width / 4) + 100), mainMenu+80, ((Screen.width / 2) - 200), 20), "New Journey")) {
                print("This will start a New Journey");
            }

            //make "Options" button
            //320
            if (GUI.Button(new Rect(((Screen.width / 4) + 100), mainMenu+120, ((Screen.width / 2) - 200), 20), "Options")) {
                print("This will show the option screen");
            }

            //make "Credits" button
            //360
            if (GUI.Button(new Rect(((Screen.width / 4) + 100), mainMenu+160, ((Screen.width / 2) - 200), 20), "Credits")) {
                showCredits=true;
				loadMain=false;
            }
    
            //make "Exit" button
            //400
            if (GUI.Button(new Rect(((Screen.width / 4) + 100), mainMenu+200, ((Screen.width / 2) - 200), 20), "Exit")){
                Application.Quit();
            }
        }//end load main if
		
		if(showCredits==true){
			displayCredits();
		}
		
    }//end GUI block

	void displayCredits(){
		
		GUI.Box(new Rect((Screen.width / 4), 160, (Screen.width / 2), 400), "Credits");
		
		if(GUI.Button (new Rect(Screen.width - 350, 160, 50, 50), "Back")){
			loadMain = true;
			showCredits = false;
		}
		
		GUI.Label(new Rect(480, 175, ((Screen.width / 3) - 20), 20), "NMC 499: Video Game Creation.");
		GUI.Label(new Rect(460, 190, ((Screen.width / 3) - 20), 20), "New Media Communications program");
		GUI.Label(new Rect(500, 205, ((Screen.width / 3) - 20), 20), "Oregon State University");
		
		
		GUI.Label(new Rect(300, 220, ((Screen.width / 3) - 20), 20), "Instructer/Producer: Todd Kesterson");
		GUI.Label(new Rect(300, 235, ((Screen.width / 3) - 20), 20), "Technical Director: Islam Almusaly");
		GUI.Label(new Rect(300, 250, ((Screen.width / 3) - 20), 20), "Creative Director and Narrative Director: Dan Felder");
		GUI.Label(new Rect(300, 265, ((Screen.width / 3) - 20), 20), "Lead Game Programmer: Garret Fleenor");
		GUI.Label(new Rect(300, 280, ((Screen.width / 3) - 20), 20), "Concept Sorcerer and Art Director: Fari 'Bandit' Nguyen");
		GUI.Label(new Rect(300, 295, ((Screen.width / 3) - 20), 20), "Associate Producer: Jacob Hart");
		GUI.Label(new Rect(300, 310, ((Screen.width / 3) - 20), 20), "Technical Artist: Justin Field");
		GUI.Label(new Rect(300, 325, ((Screen.width / 3) - 20), 20), "Modeling and Animation: Nick Harper" );
		GUI.Label(new Rect(300, 340, ((Screen.width / 3) - 20), 20), "Modeling and Interface Design: Keenan Carr" );
		GUI.Label(new Rect(300, 355, ((Screen.width / 3) - 20), 20), "Interface Design and Programming: Jonathan Gill" );
		GUI.Label(new Rect(300, 370, ((Screen.width / 3) - 20), 20), "Interface Programming: Kyle Connor" );
		GUI.Label(new Rect(300, 385, ((Screen.width / 3) - 20), 20), "Sound Design and Modeling: Maruc 'PuffballsUnited' Bromander" );
		GUI.Label(new Rect(300, 400, ((Screen.width / 3) - 20), 20), "Level Designer (Environment): Courtland B Dastyck" );
		GUI.Label(new Rect(300, 415, ((Screen.width / 3) - 20), 20), "Graphic Magician (Programmer): Padraic Mc Graw" );
		GUI.Label(new Rect(300, 430, ((Screen.width / 3) - 20), 20), "Game Programming: Adam Keaton" );
		GUI.Label(new Rect(300, 445, ((Screen.width / 3) - 20), 20), "Game Programming: Mark Udarbew" );
		GUI.Label(new Rect(300, 460, ((Screen.width / 3) - 20), 20), "Game Programming: Micheal Tichenor" );
		GUI.Label(new Rect(300, 475, ((Screen.width / 3) - 20), 20), "Concept Art and Modeling:Amy Liu" );
		GUI.Label(new Rect(300, 490, ((Screen.width / 3) - 20), 20), "Modeling and Animation: Jason Monahan" );
		GUI.Label(new Rect(300, 505, ((Screen.width / 3) - 20), 20), "Design Assistant: Mark Ritzman" );
		GUI.Label(new Rect(300, 520, ((Screen.width / 3) - 20), 20), "Luke Miller: TBD" );
		
	}
	
}


