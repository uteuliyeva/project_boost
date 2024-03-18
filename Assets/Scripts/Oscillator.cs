using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    float movementFactor;
    const float tau = Mathf.PI * 2;
    [SerializeField] float period = 2f;
    float cycles;
    float rawSinWave;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        cycles = Time.time / period;
        rawSinWave = Mathf.Sin(cycles * tau); // Going from -1 to 1
        movementFactor = (rawSinWave + 1f) / 2f;   // Normalize from 0 to 1
        Vector3 offset = movementVector * movementFactor; 
        transform.position = startingPosition + offset;
    }
}
