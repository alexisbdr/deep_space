using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpawner : MonoBehaviour {

    public Planet PlanetPrefab;

    // Use this for initialization
    void Start ()
    {
        int numPlanets = planetData.numPlanets;
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
   
    public void InitializePlanetDetails(int startingPlanet, int nP, long startingPopulation)
    {
        NumPlanets = nP;
        ActivePlanetId = startingPlanet;
        Populations = new double[NumPlanets];
        TaxRates = new double[NumPlanets];
        PopCapacities = new double[NumPlanets];
        PopIncreaseCosts = new double[NumPlanets];
        PlanetNames = new string[NumPlanets];
        for (int i = 0; i < NumPlanets; i++)
        {
            Populations[i] = 0;
            PopCapacities[i] = 1000000000;
            TaxRates[i] = 0;
            PopIncreaseCosts[i] = PopIncreaseCostBase;
            PlanetNames[i] = GeneratePlanetName();
            if (i == startingPlanet)
            {
                TaxRates[i] = 0.0001;
                Populations[i] = startingPopulation;
                PopCapacities[i] = 6000000000;
                PlanetNames[i] = "Gaia";
            }
        }
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
