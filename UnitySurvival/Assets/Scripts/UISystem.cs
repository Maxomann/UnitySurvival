using UnityEngine;
using System.Collections;
using UnityEngine.UI; //required when using Unity UI functions.

public class UISystem : MonoBehaviour
{

    //text objects, I want to control, which are child objects of the buttons.
    public Text textEnterMenu;
    public Text textRespawnButton;
    public Text textQuitButton;
    public Text textResumeButton;
    public Text textMinimapButton;

    public Image imageMenuBackground;

    public Image hunger;
    public Image thirst;
    public Image tiredness;

    //Buttons I want to control, which include the images since they are a component of my UI buttons.
    public Button enterMenu;
    public Button respawnButton;
    public Button quitButton;
    public Button resumeButton;
    public Button minimapButton;

    //The position of the UI menu elements.
    private Vector3 posEnterMenuButton = new Vector3(100f, 700f, 0f);
    private Vector3 posRespawnButton = new Vector3(640f, 385f, 0f);
    private Vector3 posQuitButton = new Vector3(640f, 335f, 0f);
    private Vector3 posResumeButton = new Vector3(640f, 435f, 0f);
    private Vector3 posMinimapButton = new Vector3(640f, 285f, 0f);
    private Vector3 posMenuBackground = new Vector3(657f, 450f, 0f);


    //The position of the player's UI elements.
    private Vector3 posHunger = new Vector3(960f, 620f, 0f);
    private Vector3 posThirst = new Vector3(1070f, 570f, 0f);
    private Vector3 posTiredness = new Vector3(840f, 570f, 0f);

    private int lifetimeHunger = 0;
    private int lifetimeThirst = 0;
    private int lifetimeTiredness = 0;


    public PlayerStatus Player;


    void Start() //Initializes the UI state at the start of the game.
    {

        //Setting the seeable text of our Buttons.
        textEnterMenu.text = "Menu";
        textRespawnButton.text = "Respawn";
        textQuitButton.text = "Quit Game";
        textResumeButton.text = "Resume Game";
        textMinimapButton.text = "Minimap";

        //Setting the font Size of the text objects.
        textEnterMenu.fontSize = 18;
        textRespawnButton.fontSize = 18;
        textQuitButton.fontSize = 18;
        textResumeButton.fontSize = 18;
        textMinimapButton.fontSize = 18;

        //sets the state of the Menu
        resetMenu();

        //Positions all the buttons.
        posButton(enterMenu, posEnterMenuButton);
        posButton(respawnButton, posRespawnButton);
        posButton(resumeButton, posResumeButton);
        posButton(quitButton, posQuitButton);
        posButton(minimapButton, posMinimapButton);

        //Positioning all the images.
        posImage(imageMenuBackground, posMenuBackground);
        posImage(tiredness, posTiredness);
        posImage(thirst, posThirst);
        posImage(hunger, posHunger);
    }

    //The update function gets called every frame.
    void Update()
    {
        checkStatus();
    }

    //Quits the whole application.
    public void QuitGame()
    {
        Application.Quit();
    }

    //Displays a message in the consule, which informs the player of having successfully resetted the game to it's starting point.
    public void Respawning()
    {
        Debug.Log("You respawned at your starting point, and also have resetted all your properties, such as hunger.");
        resetMenu();
    }

    //Sets the menu back to the default style, what means closing the full menu while enabling the button which opens the menu.
    public void resetMenu()
    {
        turnOnButton(textEnterMenu, enterMenu);
        turnOffButton(textRespawnButton, respawnButton);
        turnOffButton(textResumeButton, resumeButton);
        turnOffButton(textQuitButton, quitButton);
        turnOffButton(textMinimapButton, minimapButton);
        imageMenuBackground.enabled = false;
    }

    //opens the full menu, while disabling the button, which opens the menu.
    public void openMenu()
    {
        turnOffButton(textEnterMenu, enterMenu);
        turnOnButton(textRespawnButton, respawnButton);
        turnOnButton(textResumeButton, resumeButton);
        turnOnButton(textQuitButton, quitButton);
        turnOnButton(textMinimapButton, minimapButton);
        imageMenuBackground.enabled = true;
    }

    [HideInInspector]  //turns on all seeable components of a button, and the button's child object.
    public void turnOnButton(Text text, Button button)
    {
        text.enabled = true;
        button.enabled = true;
        button.GetComponent<Image>().enabled = true;
    }

    [HideInInspector]  //turns off all seeable components of a button, and the button's child object.
    public void turnOffButton(Text text, Button button)
    {
        text.enabled = false;
        button.enabled = false;
        button.GetComponent<Image>().enabled = false;
    }

    [HideInInspector]  //Sets the position of a button.
    public void posButton(Button button, Vector3 pos)
    {
        button.transform.position = pos;
    }

    //positions one variabel image.
    public void posImage(Image image, Vector3 pos)
    {
        image.transform.position = pos;
    }

    //Checks the player's hunger, thirst and tiredness status, and reacts by flashing an icon for each status.
    public void checkStatus()
    {
        if (Player.getThirst() >= 30f)
        {
            if (lifetimeThirst <= 10f / Player.getThirst() * 100f)
            {
                lifetimeThirst++;
                thirst.enabled = true;
            }
            else if (lifetimeThirst > 10f / Player.getThirst() * 100f && lifetimeThirst <= 20f / Player.getThirst() * 100f)
            {
                thirst.enabled = false;
                lifetimeThirst++;
            }
            else
            {
                thirst.enabled = false;
                lifetimeThirst = 0;
            }
        }
        else
        {
            thirst.enabled = false;
        }


        if (Player.getHunger() >= 30f)
        {
            if (lifetimeHunger <= 10f / Player.getHunger() * 100f)
            {
                lifetimeHunger++;
                hunger.enabled = true;
            }
            else if (lifetimeHunger > 10f / Player.getHunger() * 100f && lifetimeHunger <= 20f / Player.getHunger() * 100f)
            {
                hunger.enabled = false;
                lifetimeHunger++;
            }
            else
            {
                lifetimeHunger = 0;
            }
        }
        else
        {
            hunger.enabled = false;
        }

        if (Player.getTiredness() >= 30f)
        {
            if (lifetimeTiredness <= 10f / Player.getTiredness() * 100f)
            {
                lifetimeTiredness++;
                tiredness.enabled = true;
            }
            else if (lifetimeTiredness > 10f && lifetimeTiredness <= 20f / Player.getTiredness() * 100f)
            {
                tiredness.enabled = false;
                lifetimeTiredness++;
            }
            else
            {
                lifetimeTiredness = 0;
            }
        }
        else
        {
            tiredness.enabled = false;
        }
    }

    public void stillInProgress()
    {
        {
            Debug.Log("The Minimap Image has yet not been fully created.");
        }
    }

}

