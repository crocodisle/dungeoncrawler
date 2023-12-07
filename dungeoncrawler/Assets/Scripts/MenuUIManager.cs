using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Author: [Cunanan, Joshua/Patrick McGee]
 * Last Updated: [12/07/2023]
 * [handles all the menu ui/functions like changing/loading scenes]
 */

public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Quits the application.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Changes the scene to the value of sceneIndex.
    /// </summary>
    /// <param name="sceneIndex">The index of the scene to change to.</param>
    public void ChangeScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
