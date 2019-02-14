using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Details : MonoBehaviour
{

    //GUI elements
    Canvas DetailsCanvas;
    GameObject PlanetDetailsPanelObj;

    //player money
    private double _money;
    public double money
    {
        get { return _money; }
        set { _money = value; }
    }

    //which planet was clicked most recently
    public int ActivePlanetId;

    // Use this for initialization
    void Start()
    {
        DetailsCanvas = gameObject.GetComponent<Canvas>();
        PlanetDetailsPanelObj = GameObject.Find("PlanetDetailsPanel");
        PlanetDetailsPanelObj.SetActive(false);
        money = 0;
    }

    // Update is called once per frame
    //use this for ui updates
    void Update()
    {
        if (PlanetDetailsPanelObj.activeSelf)
        {
            Planet planet = GameObject.Find("planet" + ActivePlanetId.ToString()).GetComponent<Planet>();
            GameObject.Find("PopValue").GetComponent<Text>().text = Math.Floor(planet.population).ToString();
            GameObject.Find("PlanetNameText").GetComponent<Text>().text = planet.planetName;
            if (planet.population >= planetData.popIncreaseThreshold)
            {
                GameObject.Find("PopCost").GetComponent<Text>().text =
                    "Cost: " + Math.Ceiling(planet.popIncreaseCost).ToString() + " Money";
                GameObject.Find("PopButtonText").GetComponent<Text>().text = "Spawn Clones";
            }
            else
            {
                GameObject.Find("PopButtonText").GetComponent<Text>().text = "Colonize";
                GameObject.Find("PopCost").GetComponent<Text>().text =
                    "Cost: " + Math.Ceiling(planetData.colonizeMoneyCost).ToString() + " Money and " +
                    Math.Ceiling(planetData.colonizePopCost).ToString() + " Population";
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
        foreach (var planet in FindObjectsOfType<Planet>())
        {
            TaxSum += planet.population * planet.taxRate;
        }

        money += Time.fixedDeltaTime * TaxSum;

    }

    void SetActivePlanetID(int id)
    {
        ActivePlanetId = id;
    }

    void DecrementActivePlanetID()
    {
        //Add numplanets to avoid negative remainder
        //In C#, % operator can return negative number
        ActivePlanetId = (generalData.numPlanets + ActivePlanetId - 1) % generalData.numPlanets;
    }

    void IncrementActivePlanetID()
    {
        ActivePlanetId = (ActivePlanetId + 1) % generalData.numPlanets;
    }

}
