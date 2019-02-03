using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLeftPlanet : MonoBehaviour {

    //GUI elements
    GameObject DetailsCanvasObj;

    // Use this for initialization
    void Start () {
        DetailsCanvasObj = GameObject.Find("DetailsCanvas");
        gameObject.GetComponent<Button>().onClick.AddListener(OnClickListener);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnClickListener()
    {
        DetailsCanvasObj.SendMessage("DecrementActivePlanetID");
    }
}
