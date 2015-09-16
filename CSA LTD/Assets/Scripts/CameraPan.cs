using UnityEngine;
using System.Collections;

public class CameraPan : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		var scrollArea = 5; 
		var scrollSpeed = 70;
		var mPosX = Input.mousePosition.x;
		var mPosY = Input.mousePosition.y;
		var xBountryRadius = 10;
        var yBountryRadius = 15;
        var zBountryRadius = 20;
		
		
		// Do camera movement by mouse position
		if (mPosX < scrollArea) {transform.Translate(Vector3.right*-scrollSpeed*Time.deltaTime);}
		if (mPosX >= Screen.width-scrollArea) {transform.Translate(Vector3.right*scrollSpeed*Time.deltaTime);}
		if (mPosY < scrollArea) {transform.Translate(Vector3.up*-scrollSpeed*Time.deltaTime);}
		if (mPosY >= Screen.height-scrollArea) {transform.Translate(Vector3.up*scrollSpeed*Time.deltaTime);}
		
		
		// Do camera movement by keyboard
		if(Input.GetKey(KeyCode.RightArrow))
		{
			transform.Translate(new Vector3(scrollSpeed * Time.deltaTime,0,0));
		}
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			transform.Translate(new Vector3(-scrollSpeed * Time.deltaTime,0,0));
		}
		if(Input.GetKey(KeyCode.DownArrow))
		{
			transform.Translate(new Vector3(0,-scrollSpeed * Time.deltaTime,0));
		}
		if(Input.GetKey(KeyCode.UpArrow))
		{
			transform.Translate(new Vector3(0,scrollSpeed * Time.deltaTime,0));
		}

		transform.position = new Vector3(
			Mathf.Clamp(transform.position.x, -xBountryRadius, xBountryRadius),
			Mathf.Clamp(transform.position.y, -yBountryRadius, yBountryRadius),
			Mathf.Clamp(transform.position.z, -zBountryRadius, zBountryRadius));

	}
}
