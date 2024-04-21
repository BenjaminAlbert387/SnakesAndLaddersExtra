using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Video watched: https://youtu.be/WvJ4_xAxw0U?feature=shared

/* public class PlayerTurnScript : MonoBehaviour
{
    Text PlayerText;
    void Start()
    {
        // When the game starts, output a start message
        PlayerText = GetComponent<Text>();

        // Text outputed when the same starts
        PlayerText.text = "Game Start";

        // During the game, use the UpdateMessage function
        GameManager.instance.Message += UpdateMessage;
    }

    void UpdateMessage(Player player)
    {
        // If the game ends, output game over message
        PlayerText.text = GameManager.instance.hasGameFinished ? "Game Over!" 
        
        // Else, output player turn message
        :player.ToString() + "'s Turn";
    }
}

*/
