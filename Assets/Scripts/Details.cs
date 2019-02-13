using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Details : MonoBehaviour {

    //GUI elements
    Canvas DetailsCanvas;
    GameObject PlanetDetailsPanelObj;

    //player money
    double money;

    //planet data
    int NumPlanets;
    double[] Populations;
    double[] TaxRates;
    double[] PopCapacities;
    double[] PopIncreaseCosts;
    string[] PlanetNames;

    //which planet was clicked most recently
    int ActivePlanetId;

    //modifier for pop gain when population increase button is added
    double PopIncreaseModifier = 100000;
    //money cost for pop gain when population increase button is pushed
    double PopIncreaseCostBase = 1000;
    //threshold for pop base necessary to press gain pop button
    double PopIncreaseThreshold = 100000;
    //modifier for pop growth on updates
    double PopGrowthRate = 0.0003;
    //money cost for colonization of new planet
    double ColonizeMoneyCost = 10000;
    //pop cost for colonization of new planet
    double ColonizePopCost = 10000;


	// Use this for initialization
	void Start () {
        DetailsCanvas = gameObject.GetComponent<Canvas>();
        PlanetDetailsPanelObj = GameObject.Find("PlanetDetailsPanel");
        PlanetDetailsPanelObj.SetActive(false);
        money = 0;
	}

    // Update is called once per frame
    //use this for ui updates
    void Update () {
        if (PlanetDetailsPanelObj.activeSelf)
        {
            GameObject.Find("PopValue").GetComponent<Text>().text = Math.Floor(Populations[ActivePlanetId]).ToString();
            GameObject.Find("PlanetNameText").GetComponent<Text>().text = PlanetNames[ActivePlanetId];
            if (Populations[ActivePlanetId] >= PopIncreaseThreshold) {
                GameObject.Find("PopCost").GetComponent<Text>().text = "Cost: " + Math.Ceiling(PopIncreaseCosts[ActivePlanetId]).ToString() + " Money";
                GameObject.Find("PopButtonText").GetComponent<Text>().text = "Spawn Clones";
            } else
            {
                GameObject.Find("PopButtonText").GetComponent<Text>().text = "Colonize";
                GameObject.Find("PopCost").GetComponent<Text>().text = "Cost: " + Math.Ceiling(ColonizeMoneyCost).ToString() + " Money and " + Math.Ceiling(ColonizePopCost).ToString() + " Population";
            }
        }
        GameObject.Find("MoneyValue").GetComponent<Text>().text = Math.Floor(money).ToString();
        double popSum = 0;
        for (int i = 0; i < NumPlanets; i++)
        {
            popSum += Populations[i];
        }
        GameObject.Find("GlobalPopValue").GetComponent<Text>().text = Math.Floor(popSum).ToString();
    }

    //use this for value updates
    private void FixedUpdate()
    {
        //Taxation mechanics
        double TaxSum = 0;
        for (int i = 0; i < NumPlanets; i++)
        {
            TaxSum += Populations[i] * TaxRates[i];
        }
        money += Time.fixedDeltaTime * TaxSum;

        //Natural Population Change
        for (int i = 0; i < NumPlanets; i++)
        {
            //Model 1: growing rate of change
            //Populations[i] += PopGrowthRate * Populations[i] * Time.fixedDeltaTime;

            //Model 2: growth towards capacity - may be negative
            Populations[i] += PopGrowthRate * Populations[i]*(Math.Log10(Math.Max(1,PopCapacities[i])) - Math.Log10(Math.Max(1, Populations[i]))) * Time.fixedDeltaTime;
        }
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
        string[] possibleMiddleConsonants = { "b","c","d","f","g","j","k","l","m","n","p","q","r","s","t","v","z"};
        return possibleStartingConsonants[(int)Math.Floor((double)UnityEngine.Random.Range(0, possibleStartingConsonants.Length - 0.1f))] +
            possibleVowels[(int)Math.Floor((double)UnityEngine.Random.Range(0, possibleVowels.Length - 0.1f))] +
            possibleMiddleConsonants[(int)Math.Floor((double)UnityEngine.Random.Range(0, possibleMiddleConsonants.Length - 0.1f))] +
            possibleVowels[(int)Math.Floor((double)UnityEngine.Random.Range(0, possibleVowels.Length - 0.1f))];
    }

    public void AddPopulation()
    {
        if (Populations[ActivePlanetId] >= PopIncreaseThreshold && money > PopIncreaseCosts[ActivePlanetId])
        {
            Populations[ActivePlanetId] += PopIncreaseModifier;
            money -= PopIncreaseCosts[ActivePlanetId];
            PopIncreaseCosts[ActivePlanetId] *= 1.05;
        } else if (Populations[ActivePlanetId] < PopIncreaseThreshold && money > ColonizeMoneyCost)
        {
            for (int i = 0; i < NumPlanets; i++)
            {
                if (i != ActivePlanetId && Populations[i] >= PopIncreaseThreshold && Populations[i] - PopIncreaseThreshold >= ColonizePopCost)
                {
                    Populations[i] -= ColonizePopCost;
                    money -= ColonizeMoneyCost;
                    Populations[ActivePlanetId] += ColonizePopCost;
                    ColonizeMoneyCost *= 1.1;
                    break;
                }
            }
        }
    }

    void SetActivePlanetID(int id)
    {
        ActivePlanetId = id;
        for (int i = 0; i < NumPlanets; i++)
        {
            GameObject.Find("planet" + i.ToString()).SendMessage("SetSelected", ActivePlanetId);
        }
    }

    void DecrementActivePlanetID()
    {
        //Add numplanets to avoid negative remainder
        //In C#, % operator can return negative number
        ActivePlanetId = (NumPlanets+ActivePlanetId - 1) % NumPlanets;
        for (int i = 0; i < NumPlanets; i++)
        {
            GameObject.Find("planet" + i.ToString()).SendMessage("SetSelected", ActivePlanetId);
        }
    }

    void IncrementActivePlanetID()
    {
        ActivePlanetId = (ActivePlanetId + 1)% NumPlanets;
        for (int i = 0; i < NumPlanets; i++)
        {
            GameObject.Find("planet" + i.ToString()).SendMessage("SetSelected", ActivePlanetId);
        }
    }
}
