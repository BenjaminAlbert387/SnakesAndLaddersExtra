using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

// Video watched: https://youtu.be/WvJ4_xAxw0U?feature=shared
public class GameManager : MonoBehaviour
{
    // Boolean that checks whether the dice is able to be pressed or not
    public bool hasGameFinished, canClick;

    // Creates an instance of GameManager. Instance is a "copy" of something
    public static GameManager instance;

    // Create an integer that will be used to store the value of the dice roll
    public int DiceRoll;

    // SerializeField is used, so Unity can view and edit non public assets
    [SerializeField]

    // Gets the object GamePiece. Used to create a player piece
    GameObject gamePiece;

    [SerializeField]

    // Initalise the starting position. Used for starting the game
    UnityEngine.Vector3 startingPosition;

    // Creates the playing board using BoardScript class
    public BoardScript playingBoard;

    // Lists each player. 2 Players for now
    List<Player>players;

    // Creates an integer. Used for current player movement
    int currentPlayer;

    // Creates an array of vector positions
    public UnityEngine.Vector3[]position;

    /* Creates a dictonary. Used for snakes, ladders
    SuperSnake and bonus spaces. First int is the space
    the player is currently on, the second int is the
    space they will end on*/
    Dictionary<int, int> joints;

    /* Creates another dictonary. Used to connect each player
    with their respective piece*/
    Dictionary<Player, GameObject> pieces;

    // Output which player turn it is
    // Does not work, so decided to just comment it out
    
    // public delegate void UpdateMessage(Player player);
    // public event UpdateMessage Message; 

    // Quits the game when the Quit Game button is pressed
    public void QuitGame()
    {
        #if UNITY_EDITOR
        // Close the Unity Editor
            UnityEditor.EditorApplication.isPlaying = false;

        #else
        // Close the application
            Application.Quit();

        #endif
    }

    void Start()
    {
        // Sets game resolution. Tested on university desktop and works.
        Screen.SetResolution(1366, 768, true);
    }

    // Initalises the dice when the game starts
    private void Awake()
    {
        // If the game manager is equal to nothing
        if (instance == null)
        {
            // Then initalise it
            instance = this;
        }
        else
        {
            // Else remove it. If the game is over, 
            // Then we don't need it to be initialised
            Destroy(gameObject);
        }
        
        // Allows the dice to be clicked when the game is running
        canClick = true;

        // This is only true when the game is finished
        hasGameFinished = false;

        // Sets the variable of current players to 0
        currentPlayer = 0;

        // Calls the method that sets up the positions
        SetUpPositions();
        
        // Calls the method that sets up snakes, ladders etc
        SetUpSnakesLaddersBonus();

        /* Creates a new board and set player positions.
         Using the BoardScript method */
        playingBoard = new BoardScript(joints);

        // Creates a new list of players
        players = new List<Player>();

        /* Creates a new set of player pieces 
        that get linked to players */ 
        pieces = new Dictionary<Player, GameObject>();

        // For loop that creates as many player pieces as players
        for (int i = 0; i < 2; i++)
        {
            // Add a new player
            players.Add((Player)i);

            // Creates the player piece, stored in variable temp (Instantiates it)
            GameObject temp = Instantiate(gamePiece);

            // Stores a newly created piece into the temp variable
            // Try catch from earlier versions
            try 
            {
                pieces[(Player)i] = temp;
            }

            catch (NullReferenceException)
            {
                UnityEngine.Debug.Log("Counter not stored successfully");
            }

            // Player piece is then moved to the beginning of the grid board
            temp.transform.position = startingPosition;

            // Player piece colour is set. Try catch for errors
            try
            {
                temp.GetComponent<Piece>().SetPlayerColors((Player)i);
            }
            catch (NullReferenceException)
            {
                UnityEngine.Debug.Log("Player counter not set");
            }
        }
    }
    // Method that initalises positions based on the grid
    void SetUpPositions()
    {
        // Create an array of 100 possible positions 
        position = new UnityEngine.Vector3[100];

        // How far each grid space is relative to each other
        // CHANGE THIS IF WE UPDATE THE GAME BOARD!!!
        float diff = 0.657f;

        // Set the starting position to the beginning of the array
        position[0] = startingPosition;

        // Set the index to 1. This is so the nested for loop works
        int index = 1;

        /* Nested for loop that moves the piece either left to right
         or right to left depending on the board. Repeats 5 times */
        for (int i = 0; i < 5; i++)
        {
            // Piece movement, left to right 
            for(int j = 0; j < 9; j++)
            {
                position[index] = new UnityEngine.Vector3(position[index - 1].x + diff, position[index - 1].y, position[index - 1].z);
                index++;
            }

            position[index] = new UnityEngine.Vector3(position[index - 1].x, position[index - 1].y + diff, position[index - 1].z);
            index++;

            // Piece movement, right to left
            for (int j = 0; j < 9; j++)
            {
                position[index] = new UnityEngine.Vector3(position[index - 1].x - diff, position[index - 1].y, position[index - 1].z);
                index++;
            }

            // If a piece reaches the end, prevent it from moving
            if (index == 100) return;

            position[index] = new UnityEngine.Vector3(position[index - 1].x, position[index - 1].y + diff, position[index - 1].z);
            index++;
         }
    }

    /* Method that simulates the player counter moving due to
     snakes, ladders and bonus spaces */
    void SetUpSnakesLaddersBonus()
    {
        joints = new Dictionary<int, int> 
        {
            // These represent ladders
            {17-1, 36-1},
            {30-1, 68-1},
            {61-1, 84-1},
            {72-1, 91-1},

            // These represent snakes
            {34-1, 6-1},
            {42-1, 20-1},
            {77-1, 45-1},
            {95-1, 68-1},

            // These represent +2 Boost Spaces
            {21-1, 23-1},
            {38-1, 40-1},
            {56-1, 58-1},
            {70-1, 72-1},

            // This represents Super Snake
            {69-1, 1-1},
        };
    }

    private void Update()
    {
        // If the game has finished, then the dice won't do anything. (Won't animate or return values)
        if (hasGameFinished || !canClick ) return;

        // If the right mouse click is used while the game is running, the code below will run.
        if (Input.GetMouseButtonDown(0))
        {
            // Reads the postion of the mouse relative to the game display
            UnityEngine.Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Creates a new vector based on the 2D postion of the mouse
            UnityEngine.Vector2 mousePosition2D = new UnityEngine.Vector2(mousePosition.x, mousePosition.y);

            // Detects whether the mouse is directly on the dice or not, comparing it to coordinates 0,0
            RaycastHit2D hit = Physics2D.Raycast(mousePosition2D, UnityEngine.Vector2.zero);

            // If the mouse IS NOT on the dice when right clicked, then nothing happens
            if (!hit.collider) return;

            // If the mouse IS on the dice when right clicked, then it will animate and return values
            if (hit.collider.CompareTag("Dice"))
            {
                // Overwrite the DiceRoll integer
                // Add 1 as (0, 6) means values 1 to 5 would be generated, and dices go to 6
                DiceRoll = 1 + UnityEngine.Random.Range(0, 6);

                // Output the dice face based on the value generated
                // Uses the DiceRollPLayAnimation fuction from the DiceScript.cs file for this
                hit.collider.gameObject.GetComponent<DiceScript>().DiceRollPlayAnimation(DiceRoll);

                // Stops the user from clicking the dice multiple times
                canClick = false; 
            }
        }
    }

    // Method that moves the player piece
    public void MovePlayerPiece()
    {
        // Updates the player position relative to the board
        List<int> result =  playingBoard.UpdateBoard(players[currentPlayer], DiceRoll);

        // This gives the movement ability to players when game starts
        if(result.Count == 0)
        {
            canClick = true;
            currentPlayer = (currentPlayer + 1) % players.Count;
            return;
        }

        pieces[players[currentPlayer]].GetComponent<Piece>().SetMovement(result);
        canClick = true;

        // This stops movement ability to players when they reach the end
        if (result[result.Count - 1] == 99)
        {
            // Prevents the player from using their turn
            players.RemoveAt(currentPlayer);
            currentPlayer %= currentPlayer;

            // Allows the game to be reset after it ends
            if (players.Count == 1) hasGameFinished = true;
            return;
        }

        {
            // Players keep playing their turn until they don't roll a 6
            currentPlayer = DiceRoll == 6 ? currentPlayer : (currentPlayer + 1) % players.Count;
        }
    }
}
