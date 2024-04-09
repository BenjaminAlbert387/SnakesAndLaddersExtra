using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTurnScript : MonoBehaviour
{
    Text PlayerText;
    // Start is called before the first frame update.
    void Start()
    {
        // When the game starts, output a start message
        PlayerText = GetComponent<Text>();
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
