﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public AudioClip CoinSound = null;
    private Rigidbody rb;
    private AudioSource mAudioSource = null;
    private Queue<string> Commands = new Queue<string>();

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        controllerMovement(); 

        
    }

    public void addCommands(string[] input)
    {

        foreach (string command in input)
        {

            Commands.Enqueue(command);

        }

    }

    void controllerMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        MoveInDirection(movement);

        if( Commands.Count > 0)
        {

            processCommand();

        }

    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag.Equals("Coin"))
        {
            if (mAudioSource != null && CoinSound != null)
            {
                mAudioSource.PlayOneShot(CoinSound);
            }
            Destroy(other.gameObject);
        }

    }

    void MoveInDirection(Vector3 direc)
    {

        rb.AddForce(direc * speed);

    }

    void processCommand()
    {

        var command = Commands.Dequeue();
        var direc = new Vector3();
        switch (command)
        {

            case "Left":
                direc = new Vector3(-1, 0, 0);
                break;
            case "Right":
                direc = new Vector3(1, 0, 0);
                break;
            case "Up":
                direc = new Vector3(0, 0, 1);
                break;
            case "Down":
                direc = new Vector3(0, 0, -1);
                break;

        }

        MoveInDirection(direc);
    }




}
