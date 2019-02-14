using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncreasePopulation : MonoBehaviour {

    GameObject Details;

	// Use this for initialization
	void Start ()
    {
        //TODO: make listener persistent?
        gameObject.GetComponent<Button>().onClick.AddListener(OnClickListener);
        Details = GameObject.Find("DetailsCanvas");
    }

    void OnClickListener()
    {
        int ActivePlanetId = Details.GetComponent<Details>().ActivePlanetId;
        GameObject.Find("planet" + ActivePlanetId.ToString()).GetComponent<Planet>().AddPopulation();
    }
}
