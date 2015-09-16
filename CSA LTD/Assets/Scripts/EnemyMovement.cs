using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyMovement : MonoBehaviour {

    public Rigidbody rb;
	private List<Vector3> path;
    private float starttime;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
		path = GameManager.instance.boardScript.findShortestPath(transform.position);

    }
    void Update()
    {
        if(path.Count == 0)
        {
            path.Add(new Vector3(0, 0, -17.5F));
        }
        double dist = Vector3.Distance(transform.position, path[0]);
        /*string temp = "";
        for (int x = 0; x < path.Count; x++)
        {
            temp = temp + path[x];
        }
        Debug.Log("Currdist = " + dist + ", Path = " + temp);*/
        if(dist < 1)
        {         
            path.RemoveAt(0);
            starttime = Time.time;
            rb.velocity = Vector3.Normalize(new Vector3(path[0].x - transform.position.x, 0, path[0].z - transform.position.z)) * rb.velocity.magnitude;
        }
        rb.AddForce(Vector3.Normalize(new Vector3(path[0].x - transform.position.x, 0, path[0].z - transform.position.z)));
        int speed = 1;
        //Vector3 direction = new Vector3(path[0].x - transform.position.x, 0, path[0].z - transform.position.z);
        //transform.position = Vector3.Lerp(transform.position, path[0], (Time.time - starttime)/5);
        //transform.position.Set(transform.position.x, .5F, transform.position.z);
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
        Debug.Log("recalculating path");
        path = GameManager.instance.boardScript.findShortestPath(transform.position);
    }
}
