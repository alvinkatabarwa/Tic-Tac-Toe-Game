using UnityEngine;
using UnityEngine.UI;

public class GAMEMANAGER : MonoBehaviour
{
    public Button[] Tiles; // the nine buttons in the grid
    private string currentPlayer = "X"; //  whose turn it is
    public Text winnercongrats; //  displays winner
    public GameObject winnerPanel; // show when someone wins

    void Start()
    {
        ResetBoard(); 
    }

    public void OnGridClick(Button button)
    {
        Text buttonText = button.GetComponentInChildren<Text>();

        // make sure the button is not selected already
        if (buttonText.text == "")
        {
            buttonText.text = currentPlayer; // Set X or O
            button.interactable = false; // no more clicks on the grid

            if (CheckWinner()) // Check if current player won
            {
                ShowWinner();
            }
            else
            {
                SwitchTurn(); // Switch turn to the next player
            }
        }
    }

    void SwitchTurn()
    {
        if (currentPlayer == "X")
        {
            currentPlayer = "O"; // Switch to O's turn
        }
        else
        {
            currentPlayer = "X"; // Switch to X's turn
        }
    }

    bool CheckWinner()
    {
        string[,] board = new string[3, 3];

        // Fill board with button text
        for (int i = 0; i < 9; i++)
        {
            board[i / 3, i % 3] = Tiles[i].GetComponentInChildren<Text>().text;
        }

        // Check for a line in the Rows
        for (int i = 0; i < 3; i++)
            if (board[i, 0] != "" && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
                return true;

        //  Check for a line in the Columns
        for (int i = 0; i < 3; i++)
            if (board[0, i] != "" && board[0, i] == board[1, i] && board[1, i] == board[2, i])
                return true;

        // Check for a line in the  Diagonals
        if (board[0, 0] != "" && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
            return true;

        if (board[0, 2] != "" && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
            return true;

        return false;
    }

    void ShowWinner()
    {
        winnercongrats.text = "Winner: " + currentPlayer;
        winnerPanel.SetActive(true); // Show winner message ldkd
    }

    public void ResetBoard()
    {
        foreach (Button button in Tiles)
        {
            button.GetComponentInChildren<Text>().text = "";
            button.interactable = true;
        }

        currentPlayer = "X";
        winnerPanel.SetActive(false);
    }
}
