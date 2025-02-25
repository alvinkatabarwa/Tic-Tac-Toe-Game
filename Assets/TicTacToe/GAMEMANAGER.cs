using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GAMEMANAGER : MonoBehaviour
{
    public int WhoseTurn; // 0 = o, 1 = x
    public int turnCount;
    public Sprite[] PlayerIcons; // X and O images
    public Button[] tictactoeSpaces;
    public GameObject strikeThrough; 
    private GameObject currentStrikethrough;
    
    //

    private int[,] winningCombinations = new int[,]
    {
        { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 }, // the possible combinations for the rows
        { 0, 3, 6 }, { 1, 4, 7 }, { 2, 5, 8 }, // same for the Columns
        { 0, 4, 8 }, { 2, 4, 6 }  // same for the Diagonals
    };

    private void Start()
    {
        GameSetup();
    }

    void GameSetup()
    {
        WhoseTurn = 0;
        turnCount = 0;
        if (currentStrikethrough != null)
        {
            Destroy(currentStrikethrough);
        }

        for (int i = 0; i < tictactoeSpaces.Length; i++) //
        {
            tictactoeSpaces[i].interactable = true;
            tictactoeSpaces[i].GetComponent<Image>().sprite = null;
        }
    }

    public void TicTacToeButton(int WhichNumber)
    {
        if (WhichNumber < 0 || WhichNumber >= tictactoeSpaces.Length)
        {
            Debug.LogError("Invalid Button Index: " + WhichNumber);
            return;
        }

        tictactoeSpaces[WhichNumber].image.sprite = PlayerIcons[WhoseTurn];
        tictactoeSpaces[WhichNumber].interactable = false;
        turnCount++;

        CheckWinner(); // checks if somebody has won after every move has been played

        WhoseTurn = (WhoseTurn == 0) ? 1 : 0;
    }

    void CheckWinner()
    {
        for (int i = 0; i < winningCombinations.GetLength(0); i++)
        {
            int a = winningCombinations[i, 0];
            int b = winningCombinations[i, 1];
            int c = winningCombinations[i, 2];

            if (tictactoeSpaces[a].image.sprite != null &&
                tictactoeSpaces[a].image.sprite == tictactoeSpaces[b].image.sprite &&
                tictactoeSpaces[b].image.sprite == tictactoeSpaces[c].image.sprite)
            {
                Debug.Log("Player " + (WhoseTurn == 0 ? "O" : "X") + " is the Ultimate  TICTACTOE CHAMPION");
                ShowStrikethrough(a, b, c);
                DisableBoard();
                return;
            }
        }

        if (turnCount >= 9)
        {
            Debug.Log("It's a Draw!");
            DisableBoard();
        }
    }

    void DisableBoard()
    {
        foreach (Button button in tictactoeSpaces)
        {
            button.interactable = false;
        }
    }

    void ShowStrikethrough(int a, int b, int c)
    {
        if (strikeThrough == null)
        {
           // Debug.LogError("Strikethrough not assigned!");
            //return;
        }

        //Debug.Log("Strikethrough has been called");

        if (currentStrikethrough != null)
        {
            Destroy(currentStrikethrough);
        }

        
        Transform canvasTransform = tictactoeSpaces[a].transform.root; // making sure that this damn line is in the canvas nkt
        currentStrikethrough = Instantiate(strikeThrough, canvasTransform);
        Vector3 posA = tictactoeSpaces[a].transform.position;
        Vector3 posC = tictactoeSpaces[c].transform.position;
        Vector3 midPoint = (posA + posC) / 2f;

        currentStrikethrough.transform.position = midPoint;

        Vector3 direction = posC - posA;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        currentStrikethrough.transform.rotation = Quaternion.Euler(0, 0, angle);

        
        float lineLength = Vector3.Distance(posA, posC) + 40f;
        RectTransform rt = currentStrikethrough.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(lineLength, 10);

       
        currentStrikethrough.transform.SetAsLastSibling();
    }

    
    public void ResetGame() // scrapping literally everything
    {
        
        WhoseTurn = 0;
        turnCount = 0;

        
        if (currentStrikethrough != null)
        {
            Destroy(currentStrikethrough);
        }

        for (int i = 0; i < tictactoeSpaces.Length; i++)
        {
            tictactoeSpaces[i].interactable = true; 
            tictactoeSpaces[i].GetComponent<Image>().sprite = null; 
        }

        Debug.Log("Game has been reset!");
    }
}
