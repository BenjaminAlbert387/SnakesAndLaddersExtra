using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HelpScript : MonoBehaviour

{
    // Start is called before the first frame update
    public void StartHelp()
    {
        // When the Play Game button is pressed, loads the game
        SceneManager.LoadSceneAsync(2);
    }
}
