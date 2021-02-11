using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsTrust = 100f;
    [SerializeField] float mainTrust = 1f;

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

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("friendly");
                break;
            default:
                print("Die");
                break;
        }
    }

    private void Trust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!audio.isPlaying)
            {
                audio.Play();
            }

            rigidbody.AddRelativeForce(Vector3.up * mainTrust);
        }
        else
        {
            audio.Stop();
        }
    }

    private void Rotate()
    {
        rigidbody.freezeRotation = true;
        float ratationThisFrame = rcsTrust * Time.deltaTime;


        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(-Vector3.forward * ratationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.forward * ratationThisFrame);
        }

        rigidbody.freezeRotation = false;

    }


}
