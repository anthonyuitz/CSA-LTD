using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyMovement : MonoBehaviour {

    public Rigidbody rb;
	private List<Vector3> path;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
		path = new List<Vector3> ();
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
