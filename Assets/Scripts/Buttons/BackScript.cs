using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackScript : MonoBehaviour
{
    // Start is called before the first frame update
    public void GoBack()
    {
        // When the back button is pressed, go back to the main menu
        SceneManager.LoadSceneAsync(0);
    }
}
