/*
 * HUD Script
 */
using UnityEngine;
using System.Collections;

public class HUDScript : MonoBehaviour {

    private static float NATIVE_WIDTH = 1026;
    private static float NATIVE_HEIGHT = 768;

    public GUISkin menu_skin;

    public GUISkin roll_skin;
    public GUISkin walk_skin;
    public GUISkin aim_skin;
    public GUISkin grasp_skin;

    public Font game_font;

    public static Texture2D roll_mode_image;
    public static Texture2D walk_mode_image;
    public static Texture2D aim_mode_image;
    public static Texture2D grasp_mode_image;

    private enum enMenuLevel { pause_menu, controls_menu, no_menu };

    private enMenuLevel menu_level = enMenuLevel.no_menu;

    private bool roll_toggle = true;
    private bool walk_toggle = false;
    private bool aim_toggle = false;
    private bool grasp_toggle = false;

    /*Compass Stuff*/
    //Compass Textures
    public Texture2D compass_bg;
    public Texture2D compass_north_bubble;
    public Texture2D compass_south_bubble;
    public Texture2D compass_east_bubble;
    public Texture2D compass_west_bubble;

    //0 for + Z axis, 90 for + X axis
    public float north;
    public float south;
    public float east;
    public float west;

    //where compass is placed
    public Vector2 center;

    //how big the compass should be (in pixels)
    public Vector2 compass_size;
    public Vector2 bubble_size;

    //where the bubble is inside the compass
    public float radius;

    private Rect compass_rect;
    private float rot_n, rot_e, rot_s, rot_w, xn, yn, xe, ye, xs, ys, xw, yw;

    //Camera Storage Variables
    private int culling_storage;
    private CameraClearFlags flags_storage;

    private bool is_game_paused;

    void Update() {

        /*track escape button press*/
        if (Input.GetKeyDown(KeyCode.Escape) && (menu_level == enMenuLevel.pause_menu)) {
            menu_level = enMenuLevel.no_menu;
            Time.timeScale = 1.0f;
            //ResetCameraValues();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && (menu_level == enMenuLevel.controls_menu)) {
            menu_level = enMenuLevel.pause_menu;
        }
        else if (Input.GetKeyDown(KeyCode.Escape)) {
            Time.timeScale = 0.0f;
            SaveCameraValues();
            menu_level = enMenuLevel.pause_menu;

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
        //North Bubble Position
        rot_n = (-90 + this.transform.eulerAngles.y - north) * Mathf.Deg2Rad;

        xn = radius * Mathf.Sin(rot_n);
        yn = radius * Mathf.Cos(rot_n);
        //South Bubble Position
        rot_s = (-90 + this.transform.eulerAngles.y - south) * Mathf.Deg2Rad;

        xs = radius * Mathf.Sin(rot_s);
        ys = radius * Mathf.Cos(rot_s);
        //East Bubble Position
        rot_e = (-90 + this.transform.eulerAngles.y - east) * Mathf.Deg2Rad;

        xe = radius * Mathf.Sin(rot_e);
        ye = radius * Mathf.Cos(rot_e);
        //West Bubble Position
        rot_w = (-90 + this.transform.eulerAngles.y - west) * Mathf.Deg2Rad;

        xw = radius * Mathf.Sin(rot_w);
        yw = radius * Mathf.Cos(rot_w);

    }//end Update()

    void OnGUI() {

        HUDScaleScript.BeginGUI();

        GUI.skin.font = game_font;

        /*Begin Pause Menu Switch*/
        switch (menu_level) {

            case enMenuLevel.pause_menu:
                is_game_paused = true;
                ShowPauseMenu();

                break;

            case enMenuLevel.controls_menu:
                ShowControlsMenu();

                break;

            case enMenuLevel.no_menu:

                if (is_game_paused == true) {
                    is_game_paused = false;
                    ResetCameraValues();
                }
                /*Begin HUD Code*/
                GUI.skin = roll_skin;
                GUI.Toggle(new Rect(0, (NATIVE_HEIGHT - 75), 75, 75), roll_toggle, roll_mode_image);

                GUI.skin = walk_skin;
                GUI.Toggle(new Rect(80, (NATIVE_HEIGHT - 75), 75, 75), walk_toggle, walk_mode_image);

                GUI.skin = aim_skin;
                GUI.Toggle(new Rect(160, (NATIVE_HEIGHT - 75), 75, 75), aim_toggle, aim_mode_image);

                GUI.skin = grasp_skin;
                GUI.Toggle(new Rect(240, (NATIVE_HEIGHT - 75), 75, 75), grasp_toggle, grasp_mode_image);

                GUI.skin = null; //reset skin for other stuff

                //Draw Compass Background
                GUI.DrawTexture(new Rect(center.x - compass_size.x / 2, center.y - compass_size.y / 2, compass_size.x, compass_size.y), compass_bg);

                //Draw North Bubble
                GUI.DrawTexture(new Rect(center.x + xn - bubble_size.x / 2, center.y + yn - bubble_size.y / 2, bubble_size.x, bubble_size.y), compass_north_bubble);
                //Draw South Bubble
                GUI.DrawTexture(new Rect(center.x + xs - bubble_size.x / 2, center.y + ys - bubble_size.y / 2, bubble_size.x, bubble_size.y), compass_south_bubble);
                //Draw East Bubble
                GUI.DrawTexture(new Rect(center.x + xe - bubble_size.x / 2, center.y + ye - bubble_size.y / 2, bubble_size.x, bubble_size.y), compass_east_bubble);
                //Draw West Bubble
                GUI.DrawTexture(new Rect(center.x + xw - bubble_size.x / 2, center.y + yw - bubble_size.y / 2, bubble_size.x, bubble_size.y), compass_west_bubble);
                /*End HUD Code*/
                break;

        }
        /*End Pause Menu Switch*/

        HUDScaleScript.EndGUI();

    }//end OnGUI()

    /*
     * ShowPauseMenu()
     * This Function handles the display of the pause menu
     * Includes: Resume, Controls, Exit to Main Menu
     * Removed: Save, Load, Options
     */
    void ShowPauseMenu() {

        GUI.skin = menu_skin;
        GUI.Box(new Rect(0, 0, NATIVE_WIDTH, NATIVE_HEIGHT), "Pause Menu");

        //make "Resume" button
        if (GUI.Button(new Rect((NATIVE_WIDTH / 2) - 250, (NATIVE_HEIGHT / 2) - 150, 480, 40), "Resume")) {
            menu_level = enMenuLevel.no_menu;
            Time.timeScale = 1.0f;
            //ResetCameraValues();
        }

        //make "Controls" button
        if (GUI.Button(new Rect((NATIVE_WIDTH / 2) - 250, (NATIVE_HEIGHT / 2) - 100, 480, 40), "Controls")) {
            menu_level = enMenuLevel.controls_menu;
        }

        //make "Exit To Main Menu" button
        if (GUI.Button(new Rect((NATIVE_WIDTH / 2) - 250, (NATIVE_HEIGHT / 2) - 50, 480, 40), "Exit To Main Menu")) {
            Application.LoadLevel(0);
        }
        GUI.skin = null;

    }//end GetPauseMenu()

    /*
     * ShowControlsMenu()
     * This function handles the display of the controls
     */
    void ShowControlsMenu() {

        GUI.skin = menu_skin;
        GUI.Box(new Rect(0, 0, NATIVE_WIDTH, NATIVE_HEIGHT), "Controls");

        GUI.Label(new Rect((NATIVE_WIDTH / 2) - 250, (NATIVE_HEIGHT / 2) - 200, 480, 20), "Keys 1 through 4 switch the player between 1: Roll 2: Walk 3: Aim 4: Grasp");
        GUI.Label(new Rect((NATIVE_WIDTH / 2) - 250, (NATIVE_HEIGHT / 2) - 180, 480, 20), "");
        GUI.Label(new Rect((NATIVE_WIDTH / 2) - 250, (NATIVE_HEIGHT / 2) - 160, 480, 20), "Roll Mode: W: Roll forward S: Roll backward A: Roll left D: Roll right Space: Speed boost'");
        GUI.Label(new Rect((NATIVE_WIDTH / 2) - 250, (NATIVE_HEIGHT / 2) - 140, 480, 20), "Mouse Look: Orbit camera around the character. Movement follows camera direction");
        GUI.Label(new Rect((NATIVE_WIDTH / 2) - 250, (NATIVE_HEIGHT / 2) - 120, 480, 20), "Mouse Scroll: Dolly camera toward or away from character");
        GUI.Label(new Rect((NATIVE_WIDTH / 2) - 250, (NATIVE_HEIGHT / 2) - 100, 480, 20), "");
        GUI.Label(new Rect((NATIVE_WIDTH / 2) - 250, (NATIVE_HEIGHT / 2) - 80, 480, 20), "Walk Mode: W: Walk forward S: Walk backward A: Strafe left D: Strafe right");
        GUI.Label(new Rect((NATIVE_WIDTH / 2) - 250, (NATIVE_HEIGHT / 2) - 60, 480, 20), "Space: Jump. Mouse look: Orbit camera around the character. ");
        GUI.Label(new Rect((NATIVE_WIDTH / 2) - 250, (NATIVE_HEIGHT / 2) - 40, 480, 20), "Mouse scroll: Dolly camera toward or away from character");
        GUI.Label(new Rect((NATIVE_WIDTH / 2) - 250, (NATIVE_HEIGHT / 2) - 20, 480, 20), "");
        GUI.Label(new Rect((NATIVE_WIDTH / 2) - 250, (NATIVE_HEIGHT / 2) + 0, 480, 20), "Aim mode: W: Move forward. S: Move backward. A: Strafe left. D: Strafe right");
        GUI.Label(new Rect((NATIVE_WIDTH / 2) - 250, (NATIVE_HEIGHT / 2) + 20, 480, 20), "Space: Jump. Mouse look: Rotate camera direction. Left Moude Button: Fire grapple.");
        GUI.Label(new Rect((NATIVE_WIDTH / 2) - 250, (NATIVE_HEIGHT / 2) + 40, 480, 20), "");
        GUI.Label(new Rect((NATIVE_WIDTH / 2) - 250, (NATIVE_HEIGHT / 2) + 60, 480, 20), "Grasp: W: Move forward. S: Move backward. A: Strafe left. D: Strafe right");
        GUI.Label(new Rect((NATIVE_WIDTH / 2) - 250, (NATIVE_HEIGHT / 2) + 80, 480, 20), "Mouse look: Change camera direction");
        GUI.Label(new Rect((NATIVE_WIDTH / 2) - 250, (NATIVE_HEIGHT / 2) + 100, 480, 20), "Left Mouse Button: Open/Close grasping mechanism. Toggle");
        GUI.Label(new Rect((NATIVE_WIDTH / 2) - 250, (NATIVE_HEIGHT / 2) + 120, 480, 20), "Right Mouse Button: Rotate grasping mechanism clockwise");
        GUI.Label(new Rect((NATIVE_WIDTH / 2) - 250, (NATIVE_HEIGHT / 2) + 140, 480, 20), "Shift W,S: Move arm and camera up or down vertically");
        GUI.Label(new Rect((NATIVE_WIDTH / 2) - 250, (NATIVE_HEIGHT / 2) + 160, 480, 20), "Shift A,D: Increase or decrease the distance between two of the claw digits");

        if (GUI.Button(new Rect((NATIVE_WIDTH / 2) - 250, (NATIVE_HEIGHT / 2) + 190, 100, 20), "Back")) {
            menu_level = enMenuLevel.pause_menu;
        }
        GUI.skin = null;

    }

    void ResetCameraValues() {
        camera.clearFlags = flags_storage;
        camera.cullingMask = culling_storage;
    }

    void SaveCameraValues() {
        flags_storage = camera.clearFlags;
        culling_storage = camera.cullingMask;
        camera.clearFlags = CameraClearFlags.Nothing;
        camera.cullingMask = 0;
    }
}
