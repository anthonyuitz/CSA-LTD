using UnityEngine;
using System.Collections;

public class TileManager : MonoBehaviour {

    Color initialColor;

	// Use this for initialization
	void Start () {
        Renderer rend = GetComponent<Renderer>();
        initialColor = rend.material.color;
	}
	
	void OnMouseOver()
    {
        Renderer rend = GetComponent<Renderer>();
        rend.material.color = Color.red;
    }

    void OnMouseExit()
    {
        Renderer rend = GetComponent<Renderer>();
        rend.material.color = initialColor;
    }
}
