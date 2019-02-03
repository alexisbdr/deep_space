using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClosePlanetDetails : MonoBehaviour {

    //GUI elements
    GameObject PlanetDetailsPanelObj;

	// Use this for initialization
	void Start ()
    {
        PlanetDetailsPanelObj = GameObject.Find("PlanetDetailsPanel");
        //TODO: make listener persistent?
        gameObject.GetComponent<Button>().onClick.AddListener(OnClickListener);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnClickListener()
    {
        PlanetDetailsPanelObj.SetActive(false);
    }
}
