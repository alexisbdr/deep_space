﻿using System.Collections;
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

    //which planet was clicked most recently
    int ActivePlanetId;

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
            Planet planet = GameObject.Find("planet" + ActivePlanetId.ToString()).GetComponent<Planet>();
            GameObject.Find("PopValue").GetComponent<Text>().text = Math.Floor(planet.population).ToString();
            GameObject.Find("PlanetNameText").GetComponent<Text>().text = planet.planetName;
            if (planet.population >= planetData.popIncreaseThreshold) {
                GameObject.Find("PopCost").GetComponent<Text>().text = "Cost: " + Math.Ceiling(planet.popIncreaseCost).ToString() + " Money";
                GameObject.Find("PopButtonText").GetComponent<Text>().text = "Spawn Clones";
            } else
            {
                GameObject.Find("PopButtonText").GetComponent<Text>().text = "Colonize";
                GameObject.Find("PopCost").GetComponent<Text>().text = "Cost: " + Math.Ceiling(planetData.colonizeMoneyCost).ToString() + " Money and " + Math.Ceiling(ColonizePopCost).ToString() + " Population";
            }
        }
        GameObject.Find("MoneyValue").GetComponent<Text>().text = Math.Floor(money).ToString();
        double popSum = 0;
        foreach (var planet in FindObjectsOfType<Planet>())
        {
            popSum += planet.population;
        }
        GameObject.Find("GlobalPopValue").GetComponent<Text>().text = Math.Floor(popSum).ToString();
    }

    //use this for value updates
    private void FixedUpdate()
    {
        //Taxation mechanics
        double TaxSum = 0;
        foreach(var planet in FindObjectsOfType<Planet>())
        {
            TaxSum += planet.population * planet.taxRate;
        }
        money += Time.fixedDeltaTime * TaxSum;

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
    }

    void DecrementActivePlanetID()
    {
        //Add numplanets to avoid negative remainder
        //In C#, % operator can return negative number
        ActivePlanetId = (NumPlanets+ActivePlanetId - 1) % NumPlanets;
    }

    void IncrementActivePlanetID()
    {
        ActivePlanetId = (ActivePlanetId + 1)% NumPlanets;
    }
}
