using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneLoadingAnim : MonoBehaviour
{

	private bool rotateRight = true;
	
	// Update is called once per frame
	void Update () {
		WiggleMan();
	}
	
	void WiggleMan()
	{
		if (rotateRight)
		{
			transform.Rotate(0, 0, 1.5f);
		}
		else
		{
			transform.Rotate(0, 0, -1.5f);
		}
				
				
		if (transform.eulerAngles.z > 10 || transform.eulerAngles.z < -10)
		{
			rotateRight = !rotateRight;
		}
	}
}
