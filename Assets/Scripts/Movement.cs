using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    
    [SerializeField] float thrustSpeed = 100f;
    [SerializeField] float thrustRotation = 100f;
    [SerializeField] AudioClip mainEngineClip;

    [SerializeField] ParticleSystem mainThrustParticle;
    [SerializeField] ParticleSystem leftThrustParticle;
    [SerializeField] ParticleSystem rightThrustParticle;

    Rigidbody rb;
    AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }
    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if(Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrustSpeed * Time.deltaTime);
        if(!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngineClip);
        }
        if(!mainThrustParticle.isPlaying)
        {
            mainThrustParticle.Play();
        }
    }
    
    void StopThrusting()
    {
        audioSource.Stop();
        mainThrustParticle.Stop();
    }

    void RotateLeft()
    {
        ApplyRotation(thrustRotation);
        if(!rightThrustParticle.isPlaying)
        {
            rightThrustParticle.Play();
        }
    }

    void RotateRight()
    {
        ApplyRotation(-thrustRotation);
        if(!leftThrustParticle.isPlaying)
        {
            leftThrustParticle.Play();
        }
    }

    void StopRotating()
    {
        rightThrustParticle.Stop();
        leftThrustParticle.Stop();
    }
    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing rotation so the physics system can take over
    }
}
