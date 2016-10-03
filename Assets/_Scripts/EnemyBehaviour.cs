using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

    public int health = 2;

    // When the enemy dies, we play an explosion
    public Transform explosion;

    // sound to play when hit
    //public AudioClip hitSound;

    private GameController controller;

    // Use this for initialization
    void Start()
    {
        
        controller =
            GameObject
                .FindGameObjectWithTag("GameController")
                .GetComponent<GameController>();
    }

    void OnCollisionEnter2D(Collision2D theCollision)
    {
        // Uncomment this line to check for collision
        //Debug.Log("Hit"+ theCollision.gameObject.name);
        // this line looks for "laser" in the names of
        // anything collided.
        if (theCollision.gameObject.name.Contains("Ink"))
        {
            InkBehaviour Ink =
                theCollision.gameObject.GetComponent
                ("LaserBehaviour") as InkBehaviour;
            health -= Ink.damage;
            Destroy(theCollision.gameObject);

           
        }

        if (theCollision.gameObject.name.Contains("Octo1"))
        {
            controller.DecreaseLives(1);
        }

        if (health <= 0)
        {
            // Check if explosion was set
            if (explosion)
            {
                GameObject exploder = ((Transform)Instantiate(explosion, this.
                    transform.position, this.transform.rotation)).gameObject;
                Destroy(exploder, 2.0f);
            }

            controller.KilledEnemy();
            controller.IncreaseScore(10);


            Destroy(this.gameObject);
        }
    }
}
