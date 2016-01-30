using UnityEngine;
using System.Collections;

public class FollowMouse : MonoBehaviour
{
	bool followActive = false;
	public float xOffset = 0f;
	public float yOffset = 0f;
	public GameObject startSpot;

	// Update is called once per frame
	void Update ()
	{
		if (followActive == true){
			transform.position.x = xOffset;//Get mouse position
			transform.position.y = yOffset;//Get mouse position
		}
	}

	void PickUp (){
		if (followActive == false){
			followActive = true;
		}
	}

	void PutDown (){
		if (followActive == true){
			followActive = false;
			transform.position = startSpot.transform.position;
		}
	}
}

