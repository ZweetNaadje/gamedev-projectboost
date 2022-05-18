using System.Collections; // you can delete these. We tend not to use them at all.
using System.Collections.Generic; // you can delete these. We tend not to use them at all.
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Collision
{
    public class CollisionHandler : MonoBehaviour
    {
        [SerializeField] float delay = 1f;
        [SerializeField] AudioClip succesSfx;
        [SerializeField] AudioClip crashSfx;

        [SerializeField] ParticleSystem succesParticles;
        [SerializeField] ParticleSystem crashParticles;

        AudioSource audioSource;

        bool isTransitioning = false;
        bool collisionDisabled = false;

        void Start() // to cache a reference add the void start(). see internet why if youre curious
        {
            audioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            RespondToDebugKeys();
        }

        void RespondToDebugKeys()
        {
            if (Input.GetKeyDown(KeyCode.L)) // getkeydown vs getkey still unclear to me
            {
                LoadNextLevel();
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                collisionDisabled = !collisionDisabled; // this will toggle collision. Need more explanations
            }
        }

        void OnCollisionEnter(UnityEngine.Collision collision)
        {
            if (isTransitioning || collisionDisabled) { return; }   // if(isTransitioning) automatically means it is true, without explicitly saying so.
                                                                    // == true couldve also been used.
                                                                    // return is a key word that says 'dont go any further than this method, this codeblock.
                                                                    // Just return and go back to your business. Still a bit vague imo. Need more explanations

            switch (collision.gameObject.tag)
            {
                case "Friendly":
                    Debug.Log("Let us begin our journey!");
                    break;


                case "Finish":
                    StartSuccesSequence();
                    Debug.Log("You have reached the finish!");
                    break;

                default:
                    StartCrashSequence();
                    Debug.Log("You have bumped into an obstacle");
                    break;

            }
        }           // let erop dat je de {} goed gebruikt. De code zat eerst in het OnCollisionEnter block. Moet dus daarbuiten zitten

        void StartSuccesSequence()
        {
            isTransitioning = true;
            audioSource.Stop();
            audioSource.PlayOneShot(succesSfx);
            succesParticles.Play(); //why not use a getcomponent reference? 
            GetComponent<Movement>().enabled = false;
            Invoke("LoadNextLevel", 1f);
        }

        void StartCrashSequence()
        {
            isTransitioning = true;
            audioSource.Stop();
            audioSource.PlayOneShot(crashSfx);
            crashParticles.Play(); //why not use a getcomponent reference? 
            GetComponent<Movement>().enabled = false;
            Invoke("ReloadScene", delay);
        }

        void LoadNextLevel()
        {
            int nextIndex = SceneManager.GetActiveScene().buildIndex;
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentSceneIndex + 1;
            if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) // if you finished the last level, start again at the first level
            {
                nextSceneIndex = 0;
            }
            SceneManager.LoadScene(nextSceneIndex); // why add this? To finish what the IF started?

        }

        void ReloadScene()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
            // SceneManager.LoadScene("Over"); // Dit is wat ik deed, tutorial deed het anders. check video Respawn using SceneManager
        }
    }
}