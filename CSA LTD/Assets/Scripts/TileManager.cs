﻿using UnityEngine;
using System.Collections;

public class TileManager : MonoBehaviour {

    Color initialColor;
    public GameObject tower;

	// Use this for initialization
	void Start () {
        Renderer rend = GetComponent<Renderer>();
        initialColor = rend.material.color;
	}
	
    void OnMouseOver()
    {
        Vector3 currPos = transform.position;

        if (GameManager.instance.boardScript.tileStatus(currPos) == 0)
        {
            Renderer rend = GetComponent<Renderer>();
            rend.material.color = Color.green;
        }
    }

	void OnMouseDown()
    {
        Vector3 currPos = transform.position;

        if (GameManager.instance.boardScript.tileStatus(currPos) == 0)
        {
            GameObject instance = Instantiate(tower, new Vector3(currPos.x + .5F, 1F, currPos.z - .5F), Quaternion.identity) as GameObject;

            GameManager.instance.boardScript.towerPlaced(currPos, 1);

            Renderer rend = GetComponent<Renderer>();
            rend.material.color = Color.gray;
        }
        

        /* Renderer rend = GetComponent<Renderer>();
        if(rend.material.color.Equals(initialColor) || rend.material.color.Equals(Color.green))
        {
            rend.material.color = Color.red;
        }
        else
        {
            rend.material.color = initialColor;
        }*/
    }

    void OnMouseExit()
    {
        Renderer rend = GetComponent<Renderer>();
        if (!rend.material.color.Equals(Color.gray))
        {
            rend.material.color = initialColor;
        }
    }
}
