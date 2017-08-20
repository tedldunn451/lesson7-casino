using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lesson7_casino
{
    public partial class Default : System.Web.UI.Page
    {
        Random randomNum = new Random();

        string[] imageURLArray = new string[]
        {
            "~/Images/Bar.png",
            "~/Images/Bell.png",
            "~/Images/Cherry.png",
            "~/Images/Clover.png",
            "~/Images/Diamond.png",
            "~/Images/HorseShoe.png",
            "~/Images/Lemon.png",
            "~/Images/Orange.png",
            "~/Images/Plum.png",
            "~/Images/Seven.png",
            "~/Images/Strawberry.png",
            "~/Images/Watermellon.png",
        };

        string[] imgStringArray = new string[3];
 
        protected void Page_Load(object sender, EventArgs e)
        {
            // Set initial state of game
            if (!Page.IsPostBack)
            {
                int playerMoney = 100;
                ViewState.Add("PlayerMoney", playerMoney);
                setDialImages();
                displayPlayerMoney(playerMoney);
            }
        }

        protected void pullLeverButton_Click(object sender, EventArgs e)
        {
            int playerMoney = (int)ViewState["PlayerMoney"];
            int wager = getWager(playerMoney);
            int multiplier;
            int winnings;

            // If the player has entered a valid wager, "spin" the dials, display the resulting images
            // determine if the player had a win, calculate any winnings and display a win or lose message as appropriate
            if (wager != 0)
            {
                string[] imgArray = setDialImages();
                multiplier = determineResultMultiplier(imgArray);
                winnings = calculateWinnings(wager, multiplier);
                displayResult(wager, winnings);
                playerMoney = calculateRemainingMoney(playerMoney, wager, winnings);
                ViewState.Add("PlayerMoney", playerMoney);
                displayPlayerMoney(playerMoney);
            }
            else
                resultLabel.Text = "You must enter a valid wager amount.";
        }

        // Determine if value entered into wager text box is valid
        private int getWager(int playerMoney)
        {
            // Attempt to parse value entered in wager text box
            int wager;
            bool validWager = int.TryParse(wagerTextBox.Text, out wager);

            // If user entered an invalid wager: a wager that is not an integer, a wager less than 1
            // or a wager greater than the user's current money balance return 0
            if (!validWager || wager <= 0 || wager > playerMoney)
            {
                return 0;
            }

            return wager;
        }

        // Generate and return a pseudo-random number from 0 to length of the array of image strings
        private int generateRandomNumber()
        {
            return randomNum.Next(0, imageURLArray.Length);
        }

        // Return an image string element from the image URL array
        private string determineDialImage(int number)
        {
            return imageURLArray[number];
        }

        // Set the three images repesenting the slot machine dials
        private string[] setDialImages()
        {
            string dialOne = determineDialImage(generateRandomNumber());
            dialOneImage.ImageUrl = dialOne;
            string dialTwo = determineDialImage(generateRandomNumber());
            dialTwoImage.ImageUrl = dialTwo;
            string dialThree = determineDialImage(generateRandomNumber());
            dialThreeImage.ImageUrl = dialThree;

            imgStringArray[0] = dialOne;
            imgStringArray[1] = dialTwo;
            imgStringArray[2] = dialThree;

            return imgStringArray;
        }

        // Determine the number of bars, cherries or 7's displayed to calculate the multiplier
        // to apply to player's wager
        private int determineResultMultiplier(string[] imgArray)
        {
            int cherryCount = 0;
            int sevenCount = 0;

            // Iterate through each of the three images to determine its value; if any image is
            // a bar return 0, otherwise count the number of cherry or 7 images
            for (var i = 0; i < imgArray.Length; i++)
            {
                if (imgArray[i] == "~/Images/Bar.png")
                    return 0;
                else if (imgArray[i] == "~/Images/Cherry.png")
                    cherryCount++;
                else if (imgArray[i] == "~/Images/Seven.png")
                    sevenCount++;
            }

            // Assign the multiplier based on game rules: thee 7's is a jackpot (x100), 
            // one cherry (x2), two cherries (x3) and three cherries (x4)
            if (sevenCount == 3) return 100;

            if (cherryCount == 3) return 4;
            else if (cherryCount == 2) return 3;
            else if (cherryCount == 1) return 2;

            return 0;
        }

        // Calculate amount won, if any
        private int calculateWinnings(int wager, int multiplier)
        {
            return wager * multiplier;   
        }

        // Display the results of a particular "pull" to the screen
        private void displayResult(int wager, int winnings)
        {
            if (winnings == 0)
                resultLabel.Text = String.Format("Sorry, you lost {0:C2}. Better luck next time.", wager);
            else
                resultLabel.Text = String.Format("You bet {0:C2} and won {1:C2}!", wager, winnings);
        }

        // Calculate the remaining balance of money available to the player 
        private int calculateRemainingMoney(int playerMoney, int wager, int winnings)
        {
            return playerMoney - wager + winnings;
        }

        // Display the remaining balance of money avaialable to the player
        private void displayPlayerMoney(int playerMoney)
        {
            moneyLabel.Text = String.Format("Player's money: {0:C2}", playerMoney);
        }
    }
}
