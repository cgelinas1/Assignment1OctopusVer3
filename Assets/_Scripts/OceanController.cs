using UnityEngine;
using System.Collections;

public class OceanController : MonoBehaviour {

    private int _speed;
    private Transform _transform;

    // PUBLIC PROPERTIES
    public int Speed
    {
        get
        {
            return this._speed;
        }
        set
        {
            this._speed = value;
        }
    }


    // Use this for initialization
    void Start()
    {
        this._transform = this.GetComponent<Transform>();

        this._reset();
    }

    // Update is called once per frame
    void Update()
    {
        this._move();
        this._checkBounds();
    }

    /**
	 * this method moves the game object down the screen by _speed px every frame
	 */
    private void _move()
    {
        Vector2 newPosition = this._transform.position;

        newPosition.x -= this._speed;

        this._transform.position = newPosition;
    }

    /**
	 * checks to see if the background meets the top-border of the screen
	 */
    private void _checkBounds()
    {
        if (this._transform.position.x <= -290f)
        {
            this._reset();
        }
    }

    /**
	 * resets game object to the original position
	 */
    private void _reset()
    {
        this._speed = 1;
        this._transform.position = new Vector2(0f, 0f);
    }
}
