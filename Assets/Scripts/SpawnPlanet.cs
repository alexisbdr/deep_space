using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnPlanet : MonoBehaviour {

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
		var planet = GameObject.Find("planet" + ActivePlanetId.ToString()).GetComponent<Planet>();
		planet.newPlanetSpawned = true;
		if (planet.population >= planetData.newPlanetPopThreshold)
		{
			planet.newPlanetPopThreshold *= planetData.newPlanetPopScale;
			GameObject.Find("PlanetSpawner").GetComponent<PlanetSpawner>().CreateNewPlanet();
		}
	}
}
