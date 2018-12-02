using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ThPrPhoneScript : MonoBehaviour {

    //Public variables for buttons
    public ThPrButtonScript numBut1;
    public ThPrButtonScript numBut2;
    public ThPrButtonScript numBut3;
    public ThPrButtonScript numBut4;
    public ThPrButtonScript numBut5;
    public ThPrButtonScript numBut6;
    public ThPrButtonScript numBut7;
    public ThPrButtonScript numBut8;
    public ThPrButtonScript numBut9;
    public ThPrButtonScript numBut0;
    public ThPrButtonScript numButStar;
    public ThPrButtonScript numButPound;

    //Generated phone number display
    public Text generatedNumText;

    //Input phone number display
    public Text inputNumText;

    //Text for win or loss
    public Text endText;

    //GameObjects for victory
    public GameObject cthulhu;
    public ParticleSystem particleSystem;

    //Sounds
    private AudioSource music;
    public AudioClip boop;
    public AudioClip victorySound;
    public AudioClip buzz;

    //Array of Button Scripts
    ThPrButtonScript[,] buttonArray = new ThPrButtonScript[4, 3];

    //Variables to traverse the array
    int row = 0;
    int col = 0;

    int MAXROW = 3;
    int MAXCOL = 2;

    //Game Timer
    private float timer = 0;

    //Bools for stopping multi click
    private bool canMove = true;
    private bool uHeld = false;
    private bool canPressU = true;
    private bool incompletePhoneNumber = true;

    //Variables for generating random phone number
    private int currentRandNum;
    private string displayGeneratedPhoneNumber = "(";
    private string bareGeneratedPhoneNumber = "";

    //Variable for player's number to be displayed
    private string displayPlayerPhoneNumber = "(";
    private string barePlayerPhoneNumber = "";
    private int numberCount = 0;

    // Use this for initialization
    void Start () {

        //Stops the particle effect from playing
        particleSystem.Pause();

        music = GetComponent<AudioSource>();

        //endText.text = "";
        
        //Populate first row
        buttonArray[0, 0] = numBut1;
        buttonArray[0, 1] = numBut2;
        buttonArray[0, 2] = numBut3;

        //Populate second row
        buttonArray[1, 0] = numBut4;
        buttonArray[1, 1] = numBut5;
        buttonArray[1, 2] = numBut6;

        //Populate third row
        buttonArray[2, 0] = numBut7;
        buttonArray[2, 1] = numBut8;
        buttonArray[2, 2] = numBut9;

        //Populate fourth row
        buttonArray[3, 0] = numButStar;
        buttonArray[3, 1] = numBut0;
        buttonArray[3, 2] = numButPound;

        //Starting button set to hilight
        buttonArray[row, col].isHilighted();

        //For loop to generate random phone number
        for (int i = 0; i < 10; i++)
        {

            if (i == 3)
            {
                displayGeneratedPhoneNumber += ") ";

            } else if (i == 6)
            {
                displayGeneratedPhoneNumber += "-";
            }

            currentRandNum = Random.Range(0, 12);

            if (currentRandNum == 10)
            {
                displayGeneratedPhoneNumber += "*";
                bareGeneratedPhoneNumber += "*";

            } else if (currentRandNum == 11)
            {
                displayGeneratedPhoneNumber += "#";
                bareGeneratedPhoneNumber += "#";

            } else
            {
                displayGeneratedPhoneNumber += currentRandNum.ToString();
                bareGeneratedPhoneNumber += currentRandNum.ToString();
            }
        }

        generatedNumText.text = displayGeneratedPhoneNumber;
        inputNumText.text = "";

    }
	
	// Update is called once per frame
	void Update () {

        //This does a timer before ending the game after 10 seconds.
        timer = timer + Time.deltaTime;
        if (timer >= 10 && incompletePhoneNumber == true)
        {
            endText.text = "You Lose!";
            canMove = false;
            canPressU = false;
            GameLoader.AddScore(numberCount);
            StartCoroutine(ByeAfterDelay(2));
        }

        //Checks if player has u held
        if (canMove == true && incompletePhoneNumber == true)
        {
            //Movement through array
            if (Input.GetKeyDown("up"))
            {
                if (row - 1 < 0)
                {
                    row = 3;
                    buttonArray[0, col].isNormal();
                }
                else
                {
                    row--;
                    buttonArray[row + 1, col].isNormal();
                }
            }
            if (Input.GetKeyDown("down"))
            {
                if (row + 1 > MAXROW)
                {
                    row = 0;
                    buttonArray[3, col].isNormal();
                }
                else
                {
                    row++;
                    buttonArray[row - 1, col].isNormal();
                }
            }
            if (Input.GetKeyDown("left"))
            {
                if (col - 1 < 0)
                {
                    col = 2;
                    buttonArray[row, 0].isNormal();
                }
                else
                {
                    col--;
                    buttonArray[row, col + 1].isNormal();
                }
            }
            if (Input.GetKeyDown("right"))
            {
                if (col + 1 > MAXCOL)
                {
                    col = 0;
                    buttonArray[row, 2].isNormal();
                }
                else
                {
                    col++;
                    buttonArray[row, col - 1].isNormal();
                }
            }
        }

        //Every frame, if u is held then current button is set to be pressed, or hilighted if not pressed
        //Stops players from moving if u is held
        //Gets value from current button if u is pressed
        if (Input.GetButton("u") && canPressU == true)
        {
            buttonArray[row, col].isPressed();
            canMove = false;

            if (uHeld == false)
            {
                //Add to playerPhoneNumber strings
                barePlayerPhoneNumber += buttonArray[row, col].value;

                if (numberCount == 3)
                {
                    displayPlayerPhoneNumber += ") ";

                } else if (numberCount == 6)
                {
                    displayPlayerPhoneNumber += "-";
                }

                displayPlayerPhoneNumber += buttonArray[row, col].value;
                inputNumText.text = displayPlayerPhoneNumber;

                //Check if current number is correct
                //If not, restart
                char playerTemp = barePlayerPhoneNumber[numberCount];
                char randomTemp = bareGeneratedPhoneNumber[numberCount];
                if (playerTemp != randomTemp)
                {
                    music.PlayOneShot(buzz);
                    displayPlayerPhoneNumber = "(";
                    barePlayerPhoneNumber = "";
                    numberCount = 0;
                    uHeld = false;
                    return;
                }

                music.PlayOneShot(boop);

                numberCount++;

                uHeld = true;

                //Check victory at 10 characters
                if (numberCount == 10)
                {
                    incompletePhoneNumber = false;
                    canPressU = false;
                    cthulhu.SetActive(true);
                    particleSystem.Play();
                    music.PlayOneShot(victorySound);
                    endText.text = "You Win!";
                    GameLoader.AddScore(numberCount);
                    ByeAfterDelay(2);
                }
            }
        }
        else
        {
            buttonArray[row, col].isHilighted();
            canMove = true;
            uHeld = false;
        }

        //Escape
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

	}

    //Coroutine to end game
    IEnumerator ByeAfterDelay(float time)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        GameLoader.gameOn = false;
    }

}
