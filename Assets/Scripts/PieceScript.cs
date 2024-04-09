using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Piece : MonoBehaviour
{
    // SerializeField is used, so Unity can view and edit non public assets
    [SerializeField]

    // List the colours used for the player counters
    List<Color> colors;

    // Boolean variable that determines if a piece can move or not
    bool canMove;

    // Interger variable that stores the position index
    int moveIndex;

    List<int> movePosition;

    // Float variable that determines how quickly the piece moves
    float pieceSpeed = 10f;

    // Called before the first frame update
    void Start()
    {
        canMove = false;
        moveIndex = 0;
    }

    // Called once per frame
    void Update()
    {
        // If a piece can't move, do nothing
        if (canMove == false)
        {
            return;
        }

        // Else run the code below this comment
        // Float variable that multiplies the piece speed by seconds
        float pieceStep = pieceSpeed * Time.deltaTime;

        // Determine the value the piece needs to go to
        UnityEngine.Vector3 targetPos = GameManager.instance.position[movePosition[moveIndex]];

        // Visually move the piece on the game board
        transform.position = UnityEngine.Vector3.MoveTowards(transform.position, targetPos, pieceStep);

        // If the piece begins to move, start counting up index values
        // When it is equal to movePosition, stop the piece from moving
        if(UnityEngine.Vector3.Distance(transform.position, targetPos) < 0.001f)
        {
            moveIndex++;
            if (moveIndex == movePosition.Count)
            {
                // Resets move index to 0 so it can be used again
                moveIndex = 0;
                canMove = false;
            }
        }
    }

    // Create a function that sets the colour of the player pieces
    public void SetPlayerColors(Player player)
    {
        // Uses Unity's built in function to get the piece sprite
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();

        // The colour for each player is determined by player number
        renderer.color = colors[(int)player];
    }

    // Create a function that sets the movement of pieces
    public void SetMovement(List<int> result)
    {
        movePosition = result;
        canMove = true;
    }
}
