using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Reference: 

public class MainMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartGame()
    {
        // When the Play Game button is pressed, loads the game
        SceneManager.LoadSceneAsync(1);
    }
}
