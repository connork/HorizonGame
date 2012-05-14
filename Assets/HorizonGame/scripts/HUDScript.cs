using UnityEngine;
using System.Collections;

public class HUDScript : MonoBehaviour {

    public Texture2D map_image;

    public GUISkin roll_skin;
    public GUISkin walk_skin;
    public GUISkin aim_skin;
    public GUISkin grasp_skin;

    public Font game_font;

    public static Texture2D roll_mode_image;
    public static Texture2D walk_mode_image;
    public static Texture2D aim_mode_image;
    public static Texture2D grasp_mode_image;

    /* Upgrades (Currently Not implemented)
    public Texture2D speed_increase_image;
    public Texture2D jump_boost_image;
    public Texture2D extended_grapple_image;
    public Texture2D better_grapple_image;
    public Texture2D grounded_circuit_image;
    */

    private enum enMenuLevel {pause_menu, save_menu, load_menu, options_menu, controls_menu, no_menu};

    private enMenuLevel menu_level = enMenuLevel.no_menu;

    private bool roll_toggle = true;
    private bool walk_toggle = false;
    private bool aim_toggle = false;
    private bool grasp_toggle = false;

    /*Removed toolbar*/
    //private int toolbar_int = 0;
    //public Texture2D[] toolbar_strings = { roll_mode_image, walk_mode_image, aim_mode_image, grasp_mode_image };

    private int selectgrid_int = 0;
    private string[] selectgrid_strings = { "Map", "Menu" };

    /*Compass Stuff*/
    //Compass Textures
    public Texture2D compass_bg;
    public Texture2D compass_bubble;

    //0 for + Z axis, 90 for + X axis
    public float north;

    //where compass is placed
    public Vector2 center;

    //how big the compass should be (in pixels)
    public Vector2 compass_size;
    public Vector2 bubble_size;

    //where the bubble is inside the compass
    public float radius;

    private Rect compass_rect;
    private float rot, x, y;

    void Update() {

        /*track escape button press*/
        if (Input.GetKeyDown(KeyCode.Escape) && (menu_level == enMenuLevel.pause_menu)) {
            menu_level = enMenuLevel.no_menu;
            Time.timeScale = 1.0f;
        }
        else if (Input.GetKeyDown(KeyCode.Escape)) {
            menu_level = enMenuLevel.pause_menu;
            Time.timeScale = 0.0f;
        }

        /*track 1-4 button press*/
        if (Time.timeScale != 0.0f) {
            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                roll_toggle = true;
                walk_toggle = false;
                aim_toggle = false;
                grasp_toggle = false;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2)) {
                roll_toggle = false;
                walk_toggle = true;
                aim_toggle = false;
                grasp_toggle = false;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3)) {
                roll_toggle = false;
                walk_toggle = false;
                aim_toggle = true;
                grasp_toggle = false;
            }
            if (Input.GetKeyDown(KeyCode.Alpha4)) {
                roll_toggle = false;
                walk_toggle = false;
                aim_toggle = false;
                grasp_toggle = true;
            }
        }

        /*Compass Stuff*/
        rot = (-90 + this.transform.eulerAngles.y - north) * Mathf.Deg2Rad;

        //Bubble position
        x = radius * Mathf.Cos(rot);
        y = radius * Mathf.Sin(rot);

    }//end Update()

    void OnGUI() {

        GUI.skin.font = game_font;

        /*Begin HUD Code*/
        GUI.skin = roll_skin;
        GUI.Toggle(new Rect(0, (Screen.height - 75), 75, 75), roll_toggle, roll_mode_image);

        GUI.skin = walk_skin;
        GUI.Toggle(new Rect(80, (Screen.height - 75), 75, 75), walk_toggle, walk_mode_image);

        GUI.skin = aim_skin;
        GUI.Toggle(new Rect(160, (Screen.height - 75), 75, 75), aim_toggle, aim_mode_image);

        GUI.skin = grasp_skin;
        GUI.Toggle(new Rect(240, (Screen.height - 75), 75, 75), grasp_toggle, grasp_mode_image);

        GUI.skin = null; //reset skin for other stuff
        /*End HUD Code*/

        /*Begin Pause Menu Switch*/
        switch(menu_level) {

            case enMenuLevel.pause_menu:
                ShowPauseMenu();
                break;

            case enMenuLevel.save_menu:
                ShowSaveMenu();
                break;

            case enMenuLevel.load_menu:
                ShowLoadMenu();
                break;

            case enMenuLevel.options_menu:
                ShowOptionsMenu();
                break;

            case enMenuLevel.controls_menu:
                ShowControlsMenu();
                break;

            case enMenuLevel.no_menu:
                break;

        }
        /*End Pause Menu Switch*/

        //Draw Compass Background
        GUI.DrawTexture(new Rect(center.x - compass_size.x / 2, center.y - compass_size.y / 2, compass_size.x, compass_size.y), compass_bg);
        //Draw Bubble
        GUI.DrawTexture(new Rect(center.x + x - bubble_size.x / 2, center.y + y - bubble_size.y / 2, bubble_size.x, bubble_size.y), compass_bubble);

    }//end OnGUI()

    void ShowPauseMenu() {

        GUI.Box(new Rect((Screen.width / 6), 100, (Screen.width / 1.5f), 350), "Pause Menu");

        selectgrid_int = GUI.SelectionGrid(new Rect(((Screen.width / 6) + 10), 120, 100, 200), 
                                                                        selectgrid_int, selectgrid_strings, 1);

        //Show map
        if (selectgrid_int == 0) {

            GUI.DrawTexture(new Rect(((Screen.width / 3) + 10), 140, ((Screen.width / 2.2f)), 300), map_image, ScaleMode.StretchToFill);

        }
        //Show Inventory
        /*if (selectgrid_int == 1) {

            GUI.Label(new Rect(((Screen.width / 3) + 10), 140, ((Screen.width / 3) - 20), 20), "Inventory");
            GUI.DrawTexture(new Rect(((Screen.width / 3) + 10), 160, 60, 60), speed_increase_image, ScaleMode.StretchToFill);
            GUI.DrawTexture(new Rect(((Screen.width / 3) + 10) + 65, 160, 60, 60), jump_boost_image, ScaleMode.StretchToFill);
            GUI.DrawTexture(new Rect(((Screen.width / 3) + 10) + 130, 160, 60, 60), extended_grapple_image, ScaleMode.StretchToFill);
            GUI.DrawTexture(new Rect(((Screen.width / 3) + 10) + 195, 160, 60, 60), better_grapple_image, ScaleMode.StretchToFill);
            GUI.DrawTexture(new Rect(((Screen.width / 3) + 10) + 260, 160, 60, 60), grounded_circuit_image, ScaleMode.StretchToFill);

        }*/
        //Show options
        if (selectgrid_int == 1) {

            //make "Resume" button
            if (GUI.Button(new Rect(((Screen.width / 3) + 10), 140, ((Screen.width / 3) - 20), 20), "Resume")) {
                menu_level = enMenuLevel.no_menu;
            }

            //make "Save" button
            if (GUI.Button(new Rect(((Screen.width / 3) + 10), 180, ((Screen.width / 3) - 20), 20), "Save")) {
                menu_level = enMenuLevel.save_menu;
            }

            //make "Load" button
            if (GUI.Button(new Rect(((Screen.width / 3) + 10), 220, ((Screen.width / 3) - 20), 20), "Load")) {
                menu_level = enMenuLevel.load_menu;
            }

            //make "Options" button
            if (GUI.Button(new Rect(((Screen.width / 3) + 10), 260, ((Screen.width / 3) - 20), 20), "Options")) {
                menu_level = enMenuLevel.options_menu;
            }

            //make "Controls" button
            if (GUI.Button(new Rect(((Screen.width / 3) + 10), 300, ((Screen.width / 3) - 20), 20), "Controls")) {
                menu_level = enMenuLevel.controls_menu;
            }

            //make "Exit To Main Menu" button
            if (GUI.Button(new Rect(((Screen.width / 3) + 10), 340, ((Screen.width / 3) - 20), 20), "Exit To Main Menu")) {
                Application.LoadLevel(0);
            }

        }

    }//end GetPauseMenu()


    void ShowSaveMenu() {

        //make background box
        GUI.Box(new Rect((Screen.width / 3), 100, (Screen.width / 3), 100), "Save Menu");

        GUI.Label(new Rect(((Screen.width / 3) + 10), 140, ((Screen.width / 3) - 20), 20), "<Options Go Here>");

    }//end GetOptionsMenu()

    void ShowLoadMenu() {

        //make background box
        GUI.Box(new Rect((Screen.width / 3), 100, (Screen.width / 3), 100), "Load Menu");

        GUI.Label(new Rect(((Screen.width / 3) + 10), 140, ((Screen.width / 3) - 20), 20), "<Options Go Here>");

    }//end GetOptionsMenu()

    void ShowOptionsMenu() {

        //make background box
        GUI.Box(new Rect((Screen.width / 3), 100, (Screen.width / 3), 100), "Options Menu");

        GUI.Label(new Rect(((Screen.width / 3) + 10), 140, ((Screen.width / 3) - 20), 20), "<Options Go Here>");

    }//end GetOptionsMenu()

    void ShowControlsMenu() {

        //make background box
        GUI.Box(new Rect((Screen.width / 3), 100, (Screen.width / 3), 100), "Controls Menu");

        GUI.Label(new Rect(((Screen.width / 3) + 10), 140, ((Screen.width / 3) - 20), 20), "<Controls Go Here>");

    }//end GetControlsMenu()
}
