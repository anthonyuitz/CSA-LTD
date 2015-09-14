using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

    public Rigidbody rb;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        rb.AddForce(new Vector3(0, 0, -1));
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "End Zone")
        {
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "Tower")
            calculatePath();
    }

    public void calculatePath()
    {

    }
}
