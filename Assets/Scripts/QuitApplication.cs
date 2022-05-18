using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApplication : MonoBehaviour
{
   
    // Update is called once per frame
    void Update()
    {
        ExitGame();
    }

    void ExitGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("YOU QUIT THE GAME LOL");
            Application.Quit();
        }
    }

}
