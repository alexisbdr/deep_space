using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlanetSpawner : MonoBehaviour {

    public Planet PlanetPrefab;

    // Use this for initialization
    void Start ()
    {

        int numPlanets = generalData.numPlanets;
        int startingPlanet = (int)Mathf.Floor(UnityEngine.Random.Range(0, numPlanets));

        for (int i = 0; i < numPlanets; i++) {
            Planet newPlanet = GameObject.Instantiate<Planet>(PlanetPrefab);
            InitializePlanetDetails(newPlanet, i, i == startingPlanet);
        }
	}   
    
    public void InitializePlanetDetails(Planet newPlanet, int planetID, bool is_first)
    {
        newPlanet.SetStartingSprite(is_first);
     
        if (is_first)
        {
            newPlanet.planetName = "Gaia";

            newPlanet.population = firstPlanetData.startingPopulation;
            newPlanet.taxRate = firstPlanetData.taxRate;
            newPlanet.popCapacity = firstPlanetData.populationCapacity;
            newPlanet.popIncreaseCost = firstPlanetData.popIncreaseCostBase;

        }
        else
        {
            newPlanet.planetName = GeneratePlanetName();

            newPlanet.population = planetData.startingPopulation;
            newPlanet.taxRate = planetData.taxRate;
            newPlanet.popCapacity = planetData.populationCapacity;
            newPlanet.popIncreaseCost = planetData.popIncreaseCostBase;
        }

        newPlanet.AssignID(planetID);

    }

    public string GeneratePlanetName()
    {
        string[] possibleStartingConsonants = {"B", "C", "D", "F","G","H","J","K","L","M","N",
            "P","Q","R","S","T","V","X","Z", "Ch", "St", "Tl","Ts","Sh","Dh","Ph","Kr",
            "Pl","Pr","Th","Zh","'" };
        string[] possibleVowels = { "a", "i", "o", "u", "e","a","i","o","u","e","a","i","o","u","e",
            "ai", "ou", "uo", "oi", "ae", "ao", "ei", "ua", "oe", "ui", "ue", "ia", "io", "iu", "ie" };
        string[] possibleMiddleConsonants = { "b", "c", "d", "f", "g", "j", "k", "l", "m", "n", "p", "q", "r", "s", "t", "v", "z" };
        return possibleStartingConsonants[(int)Math.Floor((double)UnityEngine.Random.Range(0, possibleStartingConsonants.Length - 0.1f))] +
            possibleVowels[(int)Math.Floor((double)UnityEngine.Random.Range(0, possibleVowels.Length - 0.1f))] +
            possibleMiddleConsonants[(int)Math.Floor((double)UnityEngine.Random.Range(0, possibleMiddleConsonants.Length - 0.1f))] +
            possibleVowels[(int)Math.Floor((double)UnityEngine.Random.Range(0, possibleVowels.Length - 0.1f))];
    }
   
}
