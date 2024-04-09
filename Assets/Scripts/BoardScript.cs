using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Refreence: 

// The list of player colours available. P1 is Red, P2 is Blue.
public enum Player { RED,BLUE }

// Creates a public class called BoardScript
public class BoardScript : MonoBehaviour
{
  Dictionary<Player, int> playerPosition;
  int[] gridSquares;

  // Create a method that uses the snakes, ladders etc dictionary 
  public BoardScript(Dictionary<int, int> joints)
  {
    // Create a new dictonary for each player. 
    // Contains both the player and their position
    playerPosition = new Dictionary<Player, int>();

    // Create 100 grid squares, used for piece movement
    gridSquares = new int[100];

    // As our game is 2 player, two player positions will be set to 0
    for (int i = 0; i < 2; i++)
    {
      playerPosition[(Player)i] = 0;
    }

    /* As the player piece moves, the number of grid
     spaces remaining goes down */
    for (int i = 0; i < 100; i++)
    {
      gridSquares[i] = -1;
    }

    /* Joins each pair of integers in the SetUpSnakesLaddersBonus
    method dictionary together using a key */
    foreach(KeyValuePair<int,int> joint in joints)
    {
      gridSquares[joint.Key] = joint.Value;
    }
  }

  // Method that updates the board based on the user dice roll
  public List<int> UpdateBoard(Player player, int DiceRoll)
  {
    // Creates a list. Stored in the variable 
    List<int> result = new List<int>();

    /* For loop that updates the player piece position
    Only happens once per dice roll */
    for (int i = 0; i < DiceRoll; i++)
    {
      playerPosition[player] += 1;
      result.Add(playerPosition[player]);
    }

    /* If the player rolls over 100, then they move
    backwards. They need to roll exactly on 100 to win */
    if (result[result.Count - 1] > 99)
    {
      playerPosition[player] -= DiceRoll;
      return new List<int>();
    }

    // If loop that makes the indexing consistent with the board
    if (gridSquares[result[result.Count - 1]] != -1)
    {
      playerPosition[player] = gridSquares[result[result.Count - 1]];
      result.Add(playerPosition[player]);
    }

    return result;
  }
}