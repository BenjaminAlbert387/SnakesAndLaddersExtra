using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BuildVScript : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadBuildNoteScene()
    {
        // When the Build Notes button is pressed, loads that scene
        SceneManager.LoadSceneAsync(5);
    }
}
