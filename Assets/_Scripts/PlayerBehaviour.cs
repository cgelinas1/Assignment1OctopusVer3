﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerBehaviour : MonoBehaviour {
    public float playerSpeed = 4.0f;
   
    private float currentSpeed = 0.0f;
    
    private Vector3 lastMovement = new Vector3();
    
    public Transform ink;
    // How far from the center of the octopus the ink appears
    public float inkDistance = .2f;

    public float timeBetweenFires = .3f;
    // If value is less than or equal 0, we can fire
    private float timeTilNextFire = 0.0f;
    // The buttons that we can use to shoot lasers
    public List<KeyCode> shootButton;

    // What sound to play when we're shooting
    public AudioClip shootSound;

    // Reference to our AudioSource component
    private AudioSource audioSource;

    public Camera mainCamera;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
      
        Rotation();
       
        Movement();

      
        foreach (KeyCode element in shootButton)
        {
            if (Input.GetKey(element) && timeTilNextFire < 0)
            {
                timeTilNextFire = timeBetweenFires;
                ShootInk();
                break;
            }
        }
        timeTilNextFire -= Time.deltaTime;


    }

    // Will rotate the ship to face the mouse.
    void Rotation()
    {
        // We need to tell where the mouse is relative to the
        // player
        Vector3 worldPos = Input.mousePosition;
        worldPos = Camera.main.ScreenToWorldPoint(worldPos);
        /*
		* Get the differences from each axis (stands for
		* deltaX and deltaY)
		*/
        float dx = this.transform.position.x - worldPos.x;
        float dy = this.transform.position.y - worldPos.y;
        // Get the angle between the two objects
        float angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        /*
		* The transform's rotation property uses a Quaternion,
		* so we need to convert the angle in a Vector
		* (The Z axis is for rotation for 2D).
		*/
        Quaternion rot = Quaternion.Euler(new Vector3(0, 0, angle + 90));
        // Assign the ship's rotation
        this.transform.rotation = rot;
    }

    // Will move the player based off of keys pressed
    void Movement()
    {
        // The movement that needs to occur this frame
        Vector3 movement = new Vector3();
        // Check for input
        movement.x += Input.GetAxis("Horizontal");
        movement.y += Input.GetAxis("Vertical");
        /*
		* If we pressed multiple buttons, make sure we're only
		* moving the same length.
		*/
        movement.Normalize();
        // Check if we pressed anything
        if (movement.magnitude > 0)
        {
            // If we did, move in that direction
            currentSpeed = playerSpeed;
            this.transform.Translate(movement * Time.deltaTime *
                playerSpeed, Space.World);
            lastMovement = movement;

            mainCamera.transform.Translate(movement * Time.deltaTime * playerSpeed, Space.World);
        }
        else
        {
            // Otherwise, move in the direction we were going
            this.transform.Translate(lastMovement * Time.deltaTime *
                currentSpeed, Space.World);

            mainCamera.transform.Translate(lastMovement * Time.deltaTime *
                currentSpeed, Space.World);

            // Slow down over time
            currentSpeed *= .9f;
        }
    }

    // Creates a laser and gives it an initial position in
    // front of the ship.
    void ShootInk()
    {
       
        Vector3 inkPos = this.transform.position;
        // angle away from the center
        float rotationAngle = transform.localEulerAngles.z - 90;
        // Calculate the position right in front of octpus
        
        inkPos.x += (Mathf.Cos((rotationAngle) *
            Mathf.Deg2Rad) * -inkDistance);
        inkPos.y += (Mathf.Sin((rotationAngle) *
            Mathf.Deg2Rad) * -inkDistance);
        Instantiate(ink, inkPos, this.transform.rotation);
    }
}
