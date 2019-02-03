using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpawner : MonoBehaviour {

    public Planet PlanetPrefab;

    const int numPlanets = 8;
    const long startingPopulation = 1000000;

	// Use this for initialization
	void Start ()
    {
        //random starting planet
        int startingPlanet = (int)Mathf.Floor(Random.Range(0, numPlanets));
        for (int i = 0; i < numPlanets; i++)
        {
            Planet newPlanet = GameObject.Instantiate<Planet>(PlanetPrefab);
            newPlanet.AssignID(i);
            if (i == startingPlanet)
            {

                newPlanet.SetStarting();
            } else
            {
                newPlanet.SetNonStarting();
            }
        }

        GameObject.Find("DetailsCanvas").GetComponent<Details>().InitializePlanetDetails(startingPlanet, numPlanets, startingPopulation);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
