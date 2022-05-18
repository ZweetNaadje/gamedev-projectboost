using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CheatKeys : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DisableCollisions();
    }

    // Update is called once per frame
    void Update()
    {
        SkipLevel();        
    }

    void SkipLevel()
    {
        if (Input.GetKey(KeyCode.L))
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentSceneIndex + 1;
            if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) // if you finished the last level, start again at the first level
            {
                nextSceneIndex = 0;
            }
            SceneManager.LoadScene(nextSceneIndex); // why add this? To finish what the IF started?

            SceneManager.LoadScene(currentSceneIndex + 1); // why no ++X or X++?

            Debug.Log(SceneManager.GetActiveScene());
        }        
    }
    
    void DisableCollisions()
    {
        
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("test");
            GetComponent<Collider>().enabled = false;
        }
    }

}
