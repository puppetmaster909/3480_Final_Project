using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneScript : MonoBehaviour {

    //Public variables for buttons
    public ButtonScript numBut1;
    public ButtonScript numBut2;
    public ButtonScript numBut3;
    public ButtonScript numBut4;
    public ButtonScript numBut5;
    public ButtonScript numBut6;
    public ButtonScript numBut7;
    public ButtonScript numBut8;
    public ButtonScript numBut9;
    public ButtonScript numBut0;
    public ButtonScript numButStar;
    public ButtonScript numButPound;

    //Array of Button Scripts
    ButtonScript[,] buttonArray = new ButtonScript[4, 3];

    //Variables to traverse the array
    int row = 0;
    int col = 0;

    int MAXROW = 3;
    int MAXCOL = 2;

    private bool canMove = true;
    private bool spaceHeld = false;
    private bool canPressSpace = true;
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

        print(displayGeneratedPhoneNumber);
        print(bareGeneratedPhoneNumber);
    }
	
	// Update is called once per frame
	void Update () {
        
        //Checks if player has space held
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

        //Every frame, if space is held then current button is set to be pressed, or hilighted if not pressed
        //Stops players from moving if space is held
        //Gets value from current button if space is pressed
        if (Input.GetButton("space") && canPressSpace == true)
        {
            buttonArray[row, col].isPressed();
            canMove = false;

            if (spaceHeld == false)
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
                numberCount++;

                spaceHeld = true;

                //Delete
                //print(displayPlayerPhoneNumber);

                if (numberCount == 10)
                {
                    incompletePhoneNumber = false;
                    canPressSpace = false;
                    print(displayPlayerPhoneNumber);
                }

            }
        }
        else
        {
            buttonArray[row, col].isHilighted();
            canMove = true;
            spaceHeld = false;
        }

        //Escape
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

	}
}
