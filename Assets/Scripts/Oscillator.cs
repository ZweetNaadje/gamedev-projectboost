using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    //[SerializeField] [Range(0,1)] Range will create a slider which slides between 0 and 1 

    float movementFactor; 
    [SerializeField] float period = 2f;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position; // gets the current position of the object this script is attached to. Remember we defined this as a Vector3 at the beginning
        
    }

    // Update is called once per frame
    void Update() // Quite difficult... see video on mathfsin() for oscillation at gamedev course for the explanation
    {
        if (period <= Mathf.Epsilon) { return; } // remember return is a key word that says 'dont go any further than this method, this codeblock.
                                                 // Simply go back and redo until period != 0 
                                                 // mathf.epsilon is the smallest float. it's unpredictable to use == with floats so use epsilon instead (when avoiding NaN error)
        float cycles = Time.time / period; // continually growing over time
        
        const float tau = Mathf.PI * 2; //1 tau equals 2pi (2 * pi)
        float rawSinWave = Mathf.Sin(cycles * tau); // going from -1 to 1

        movementFactor = (rawSinWave + 1f) / 2f; // recalculated to go from 0 to 1 so it's cleaner. Now it goes from A --> B --> A etc. -1 to 1 will go from A --> B --> C --> A (visualize this for better example)

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
