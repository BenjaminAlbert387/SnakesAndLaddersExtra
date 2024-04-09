using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

//Reference: 
public class DiceScript : MonoBehaviour
{
    // Initialise an integer.
    // Using its value for player movement on the game board
    int DiceRollValue;

    // SerializeField is used, so Unity can view 
    // And edit non public assets
    [SerializeField]

    // List the six dice sprites. 
    List<Sprite> die;

    // Create a function that simulates the dice "rolling" 
    // And randomly selecting a dice face
    public void DiceRollImage()
    {
        // Use Unity's SpriteRender built in function to go through each dice face
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();

        // The dice selects a dice face randomly, between 1 to 6
        renderer.sprite = die[Random.Range(0, die.Count)];
    }
    
    public void DiceRollSetImage()
    {
        // Sets up the dice face selected that will be outputted
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();

        // As the dice only goes up to 6, take away one
        renderer.sprite = die[DiceRollValue - 1];

        // Allows the dice face to be clicked
        GameManager.instance.canClick = true;

        // Uses the GameManager class to move the player piece
        GameManager.instance.MovePlayerPiece();
    }

    // Create a function that plays the dice roll animation
    public void DiceRollPlayAnimation(int TemporaryValue)
    {
        // DiceRollValue becomes equal to the temporary value
        DiceRollValue = TemporaryValue;

        // Plays the dice roll animation created in Unity
        Animator animator = GetComponent<Animator>();
        animator.Play("RollAnimation", -1, 0f);
    }
}
