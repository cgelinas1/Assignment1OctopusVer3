using UnityEngine;
using System.Collections;

public class InkBehaviour : MonoBehaviour
{

    
    public float lifetime = 3.0f;
    
    public float speed = 4.0f;
    
    public int damage = 1;

    // Use this for initialization
    void Start()
    {
        
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
    }
}
