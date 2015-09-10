using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

    public Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        rb.AddForce(new Vector3(0, 0, -1));
    }
}
