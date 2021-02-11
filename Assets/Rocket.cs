using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{

    Rigidbody rigidbody;
    AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Trust();
        Rotate();
    }

    private void Trust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!audio.isPlaying)
            {
                audio.Play();
            }

            rigidbody.AddRelativeForce(Vector3.up);
        }
        else
        {
            audio.Stop();
        }
    }

    private void Rotate()
    {
        rigidbody.freezeRotation = true;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(-Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.forward);

        }

        rigidbody.freezeRotation = false;

    }


}
