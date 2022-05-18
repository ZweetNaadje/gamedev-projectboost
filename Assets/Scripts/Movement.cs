using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // PARAMETERS - For tuning, typically set in the editor (ex: SerializeField)
    // CACHE - ex references for readability or speed (ex: Rigidbody rb;)
    // STATE - private instance (member) variables (ex: bool Isalive;)

    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 1f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainBoosterParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;

    Rigidbody rb; // caching a reference
    AudioSource audioSource; 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    } 

    // Update is called once per frame
    void Update()
    {
        ProcessThrust(); // I summon thee, ProcessThrust!
                         // This way you don't need to add massive lines of code in the void update.
                         //  Looks cleaner now.
        ProcessRotation(); // I summon thee, ProcessRotation!
    }

   void ProcessThrust() // Here you explain what ProcessThrust is supposed to be and do.
                        // You can't summon something if you don't know the spell ;)
    {
        if(Input.GetKey(KeyCode.Space)) // when space is pressed add a relative force to the object to which this script has been attached to.
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void ProcessRotation() // Here you explain what ProcessRotation is supposed to be and do.
                           // You can't summon something if you don't know the spell ;)
    {
        if (Input.GetKey(KeyCode.A)) // executed only IF "input.getkey(keycode.A))" is true
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D)) // executed only if keycode.A was false and the keycode.D is true
        {
            RotateRight();
        }
        else // executed only if both keycode.a and keycode.B were false
        {
            StopRotating();
        }
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainBoosterParticles.isPlaying)
        {
            mainBoosterParticles.Play();
        }
    }

    void StopThrusting()
    {
        audioSource.Stop(); // make sure the else is under the first IF and not the second one
        mainBoosterParticles.Stop();
    }

    void RotateLeft()
    {
        ApplyRotation(rotationThrust);
        if (!rightThrusterParticles.isPlaying)
        {
            rightThrusterParticles.Play();
        }
    }

    void RotateRight()
    {
        ApplyRotation(-rotationThrust); // the - sign will make it able to spin the opposite direction.
                                        // If you don't add the - you can only spin into one direction
        if (!leftThrusterParticles.isPlaying)
        {
            leftThrusterParticles.Play();
        }
    }
    void StopRotating()
    {
        rightThrusterParticles.Stop();
        leftThrusterParticles.Stop();
    }

    void ApplyRotation(float rotationThisFrame) // remember, code executes from top to bottom.
                                                // In this part it says: IF you press this button THEN freezeRotation,
                                                // do your vector3 magic and then STOP freezeRotation. Top to bottom 
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing rotation so the physics system can take over
    }
}
