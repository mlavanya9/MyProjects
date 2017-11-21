/* MAKE A DEAL 
  
  author: LAVANYA  */  
   


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MakeADeal
{
    public partial class GameScreen : Form
    {
        Random random = new Random();
        int carPosition;// for showing the position of car.
        int myDoor;// for the selected door
        int montysDoorChoice;// door which is selected by Monty
        int doorToSwitch;// for switching option
        int totalGamesCount = 0, totalWinsCount = 0, totalLostCount = 0;
        public GameScreen()
        {
            InitializeComponent();// This is the very first statement to run in the form GameScreen
            GameStart();
            totalGames.Text = "0";
            gamesLost.Text = "0";
            gamesWon.Text = "0";
        }
     /*Here, we should have 3 doors and the only the option to select one door from
     the three doors should be enabled, all other should not function. According to this, only
     selectDoorLabel visibility is kept true and all others are made false.*/

        private void GameStart()
        {
            switchDoorButton.Visible = false;
            RevealButton.Visible = false;
            resetToReplay.Visible = false;
            selectDoorLabel.Visible = true;
            youWonOrLostLabel.Visible = false;
            EnablePanelClicks();
            door_1.BackgroundImage = Properties.Resources.door;
            door_2.BackgroundImage = Properties.Resources.door;
            door_3.BackgroundImage = Properties.Resources.door;
        }
    /*This method allows to reset the game. Here we again set all doors back to their original image, 
      and set the car position, selected door and switched door to 0(i.e) back to the very beginning stage.
      The components wins count, lost count and total number of games played are not involved in this as they 
      should count the number each time and get updated with each game.*/
        private void ResetGame()
        {
            door_1.BackgroundImage = Properties.Resources.door;
            door_2.BackgroundImage = Properties.Resources.door;
            door_3.BackgroundImage = Properties.Resources.door;
            carPosition = 0;
            myDoor = 0;
            montysDoorChoice = 0;
            doorToSwitch = 0;
            GameStart();
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {

        }
      /*This method describes the changes to be done, when initially a door is selected from the three doors.
      In this, initailly when  door1 is selected the image of the door1 should be changed, the buttons switchdoor
      and reveal the selected door should be activated.*/
        private void door_1_Click(object sender, EventArgs e)
        {
            door_1.BackgroundImage = Properties.Resources.selectedDoor;
            switchDoorButton.Visible = true;
            RevealButton.Visible = true;
            selectDoorLabel.Visible = false;
            DisablePanelClicks();
            MakeADeal(1);
        }
      /*This method describes the changes to be done, when initially a door is selected from the three doors.
       In this, initailly when  door2 is selected the image of the door2 should be changed, the buttons switchdoor
       and reveal the selected door should be activated.*/
        private void door_2_Click(object sender, EventArgs e)
        {
            door_2.BackgroundImage = Properties.Resources.selectedDoor;
            switchDoorButton.Visible = true;
            RevealButton.Visible = true;
            selectDoorLabel.Visible = false;
            DisablePanelClicks();
            MakeADeal(2);
        }
     /*This method describes the changes to be done, when initially a door is selected from the three doors.
      In this, initailly when  door3 is selected the image of the door3 should be changed, the buttons switchdoor
      and reveal the selected door should be activated.*/
        private void door_3_Click(object sender, EventArgs e)
        {
            door_3.BackgroundImage = Properties.Resources.selectedDoor;
            switchDoorButton.Visible = true;
            RevealButton.Visible = true;
            selectDoorLabel.Visible = false;
            DisablePanelClicks();
            MakeADeal(3);
        }

        private void MakeADeal(int myDoorNumber)
        {
            carPosition = random.Next(1 , 4);
            myDoor = myDoorNumber;
            switch (myDoorNumber)
            {
                case 1:
                    door_2.BackgroundImage = Properties.Resources.door;
                    door_3.BackgroundImage = Properties.Resources.door;
                    break;
                case 2:
                    door_1.BackgroundImage = Properties.Resources.door;
                    door_3.BackgroundImage = Properties.Resources.door;
                    break;
                case 3:
                    door_1.BackgroundImage = Properties.Resources.door;
                    door_2.BackgroundImage = Properties.Resources.door;
                    break;
            }
      /*A method called generateMontysdoorchoice is called. This gives the door that is selected by Monty.
      The door which Monty selects should have a goat behind that. So, we use a switch case here.*/
            montysDoorChoice = generateMontysDoorChoice(carPosition, myDoor);
            switch (montysDoorChoice)
            {
                case 1:
                    door_1.BackgroundImage = Properties.Resources.goat;
                    break;
                case 2:
                    door_2.BackgroundImage = Properties.Resources.goat;
                    break;
                case 3:
                    door_3.BackgroundImage = Properties.Resources.goat;
                    break;
            }
        }
      /* Generating Monty's door choice:
      If selection is the car position or the door choice, then continue as it is in the previous state else 
      choose that as Monty's door.*/
        private int generateMontysDoorChoice(int carPosition, int doorChoice)
        {
            int i = 0;
            int montyDoor = 0;
            for (i = 1; i <= 3; i++)
            {
                if (i == carPosition || i == doorChoice)
                { continue; }
                montyDoor = i;
            }
            return montyDoor;
        }
     /* This method deals with switching case. If switched door number is my selected door choice or Monty's 
      choice then we leave the door without selecting, else we select that door.*/
        private int SwitchDoor(int myDoorChoice, int montyDoorChoice)
        {
            int switchedDoorNum = 0;
            for (switchedDoorNum = 1; switchedDoorNum <= 3; switchedDoorNum++)
            {
                if (switchedDoorNum == myDoorChoice || switchedDoorNum == montyDoorChoice)
                { continue; }
                myDoor = switchedDoorNum;
            }
            return myDoor;
        }
     /* Here the proprties of the door changes. If we select the switch door, then then remaining door 
     other than Monty's door should change to the selected door, and then previous door should be back to 
     it's original form.*/
        private void switchDoorButton_Click(object sender, EventArgs e)
        {
            int myOldSelectedDoor = myDoor;
            doorToSwitch = SwitchDoor(myDoor, montysDoorChoice);
            switch (doorToSwitch)
            {
                case 1:
                    door_1.BackgroundImage = Properties.Resources.selectedDoor;
                    break;
                case 2:
                    door_2.BackgroundImage = Properties.Resources.selectedDoor;
                    break;
                case 3:
                    door_3.BackgroundImage = Properties.Resources.selectedDoor;
                    break;
            }
            switch (myOldSelectedDoor)
            {
                case 1:
                    door_1.BackgroundImage = Properties.Resources.door;
                    break;
                case 2:
                    door_2.BackgroundImage = Properties.Resources.door;
                    break;
                case 3:
                    door_3.BackgroundImage = Properties.Resources.door;
                    break;
            }
        }
        /* This method reveals the selection, we place the car in three different possible places.
        Then after revealing is done, we have two options, we can replay the game or exit the game. */
        private void RevealButton_Click(object sender, EventArgs e)
        {
            switch (carPosition)
            {
                case 1:
                    door_1.BackgroundImage = Properties.Resources.car;
                    door_2.BackgroundImage = Properties.Resources.goat;
                    door_3.BackgroundImage = Properties.Resources.goat;
                    resetToReplay.Visible = true;
                    switchDoorButton.Visible = false;
                    RevealButton.Visible = false;
                    ScoreCount();
                    break;
                case 2:
                    door_1.BackgroundImage = Properties.Resources.goat;
                    door_2.BackgroundImage = Properties.Resources.car;
                    door_3.BackgroundImage = Properties.Resources.goat;
                    resetToReplay.Visible = true;
                    switchDoorButton.Visible = false;
                    RevealButton.Visible = false;
                    ScoreCount();
                    break;
                case 3:
                    door_1.BackgroundImage = Properties.Resources.goat;
                    door_2.BackgroundImage = Properties.Resources.goat;
                    door_3.BackgroundImage = Properties.Resources.car;
                    resetToReplay.Visible = true;
                    switchDoorButton.Visible = false;
                    RevealButton.Visible = false;
                    ScoreCount();
                    break;
            }
        }

        private void ScoreCount()
        {
            if (myDoor == carPosition)
            {
                totalWinsCount++;
                totalGamesCount++;
                totalGames.Text = totalGamesCount.ToString();
                gamesWon.Text = totalWinsCount.ToString();
                youWonOrLostLabel.Visible = true;
                youWonOrLostLabel.Text = "Hurray..!! You Won...!!!";
            }
            else
            {
                totalLostCount++;
                totalGamesCount++;
                totalGames.Text = totalGamesCount.ToString();
                gamesLost.Text = totalLostCount.ToString();
                youWonOrLostLabel.Visible = true;
                youWonOrLostLabel.Text = "OOPS..!! You Lost..!!!";
            }

        }
        /* This allows only single selection of doors instead of multiple selections.*/
        private void DisablePanelClicks()
        {
            door_1.Click -= door_1_Click;
            door_2.Click -= door_2_Click;
            door_3.Click -= door_3_Click;
        }
        /* This allows the door selection*/
        private void EnablePanelClicks()
        {
            door_1.Click += door_1_Click;
            door_2.Click += door_2_Click;
            door_3.Click += door_3_Click;
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            ResetGame();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
