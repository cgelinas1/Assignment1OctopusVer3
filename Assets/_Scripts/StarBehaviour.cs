﻿using UnityEngine;
using System.Collections;

public class StarBehaviour : MonoBehaviour {

    public int health = 2;

    // sound to play when hit
    public AudioClip hitSound;
   

    // create an AudioSource variable
    private AudioSource audio;

    private GameController controller;

    // Use this for initialization
    void Start()
    {
        audio = GetComponent<AudioSource>();
        controller =
            GameObject
                .FindGameObjectWithTag("GameController")
                .GetComponent<GameController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Uncomment this line to check for collision
        Debug.Log("Hit" + other.gameObject.name);

        if (other.gameObject.name.Contains("Ink"))
        {
            InkBehaviour ink =
            other.gameObject.GetComponent
            ("InkBehaviour") as InkBehaviour;
            health -= ink.damage;
            Destroy(this.gameObject);


        }
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }

        if (other.gameObject.name.Contains("Octo1"))
        {
            Destroy(this.gameObject);
        }


    }
}
