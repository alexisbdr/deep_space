using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using UnityEngine.EventSystems;

public class Details : MonoBehaviour
{

    //GUI elements
    Canvas DetailsCanvas;
    GameObject PlanetDetailsPanelObj;
    GameObject ScienceGamblePanel;
    
    
    public Sprite buttonClickableSprite;
    private Color _buttonClickableColor = Color.white;
    private Color _buttonNotClickableColor;

    //player money
    private double _money;
    public double money
    {
        get { return _money; }
        set { _money = value; }
    }
    
    // science points
    public double science = 0;

    // general stats for state of game
    public double planetSpawnThreshold;
    public double clickUpgradeCost = 1;
    public double popClick;
        
    //which planet was clicked most recently
    public int ActivePlanetId;

    // Use this for initialization
    void Start()
    {
        popClick = 1;
        PlanetDetailsPanelObj = GameObject.Find("PlanetDetailsPanel");
        PlanetDetailsPanelObj.SetActive(false);
        ScienceGamblePanel = GameObject.Find("ScienceGamble");
        ScienceGamblePanel.SetActive(false);
        
        money = 0;
        science = 0;
        planetSpawnThreshold = generalData.planetSpawnThreshold;
        _buttonNotClickableColor = new Color(0f, 0f, 0f, 0.1f);
 
        foreach (var button in GameObject.FindGameObjectsWithTag("upgradeButton"))
        {
            var image = button.gameObject.GetComponent<Image>();
            image.color = Color.black;
        }
    }

    // Update is called once per frame
    //use this for ui updates
    void Update()
    {
        if (PlanetDetailsPanelObj.activeSelf)
        {
            Planet planet = GameObject.Find("planet" + ActivePlanetId.ToString()).GetComponent<Planet>();
            GameObject.Find("PopValue").GetComponent<Text>().text = GameUtils.formatLargeNumber(planet.population);
            GameObject.Find("ProductivityValue").GetComponent<Text>().text = "(+ $" + GameUtils.formatLargeNumber(planet.productivity) + "/person)";
            GameObject.Find("PlanetNameText").GetComponent<Text>().text = planet.planetName;
            GameObject.Find("PopRateValue").GetComponent<Text>().text = "(+ " + GameUtils.formatLargeNumber((planet.fixedPopGrowth*popClick)) + " /sec)";

            GameObject.Find("MoneyValue").GetComponent<Text>().text = GameUtils.formatLargeNumber(planet.cryptocoins);

            GameObject.Find("AutoGrowthCost").GetComponent<Text>().text = GameUtils.formatLargeNumber(planet.autoGrowthCost);
            GameObject.Find("IncProdCost").GetComponent<Text>().text = GameUtils.formatLargeNumber(planet.productivityGrowthCost);
            GameObject.Find("GetScienceCost").GetComponent<Text>().text = GameUtils.formatLargeNumber(planet.sciencePopCost);
            
        }
        
           
   
        //Productivity mechanics
        double globalProductivityRate = 0;
        double popSum = 0;
        double popGrowthSum = 0; 
        foreach (var planet in FindObjectsOfType<Planet>())
        {
            popSum += planet.population;
            globalProductivityRate += planet.population * planet.productivity;
            popGrowthSum += planet.fixedPopGrowth;
        }
        money += Time.deltaTime * globalProductivityRate;


        GameObject.Find("ScienceVal").GetComponent<Text>().text = science.ToString();
        GameObject.Find("GlobalPopValue").GetComponent<Text>().text = GameUtils.formatLargeNumber(popSum);

        if (science >= 1)
        {
            ScienceGamblePanel.SetActive(true);
        }
        
        if (GameObject.Find("PopCost"))
        {
            GameObject.Find("PopCost").GetComponent<Text>().text = GameUtils.formatLargeNumber(planetSpawnThreshold);
        }

        GameObject.Find("ClickUpgradeCost").GetComponent<Text>().text = GameUtils.formatLargeNumber(clickUpgradeCost) + " Science Points";
        //GameObject.Find("MoneyRateValue").GetComponent<Text>().text = globalProductivityRate.ToString("F2");
        //GameObject.Find("PopRateValue").GetComponent<Text>().text = Math.Floor(popGrowthSum).ToString();

        if (GameObject.Find("PlanetSpawner").GetComponent<PlanetSpawner>().numPlanetsSpawned >= generalData.numPlanetsMax)
        {
            if (GameObject.Find("SpawnPlanet"))
            {
                GameObject.Find("SpawnPlanet").SetActive(false);
            }
        }

    }

    //use this for value updates
    private void FixedUpdate()
    {
        

    }

    void SetActivePlanetID(int id)
    {
        ActivePlanetId = id;
        for (int i = 0; i < GameObject.Find("PlanetSpawner").GetComponent<PlanetSpawner>().numPlanetsSpawned; i++)
        {
            GameObject.Find("planet" + i.ToString()).SendMessage("SetSelected", ActivePlanetId);
        }
    }

    void DecrementActivePlanetID()
    {
        //Add numplanets to avoid negative remainder
        //In C#, % operator can return negative number
        ActivePlanetId = (GameObject.Find("PlanetSpawner").GetComponent<PlanetSpawner>().numPlanetsSpawned + ActivePlanetId - 1) % GameObject.Find("PlanetSpawner").GetComponent<PlanetSpawner>().numPlanetsSpawned;

        for (int i = 0; i < GameObject.Find("PlanetSpawner").GetComponent<PlanetSpawner>().numPlanetsSpawned; i++)
        {
            GameObject.Find("planet" + i.ToString()).SendMessage("SetSelected", ActivePlanetId);
        }
    }

    void IncrementActivePlanetID()
    {
        ActivePlanetId = (ActivePlanetId + 1)% GameObject.Find("PlanetSpawner").GetComponent<PlanetSpawner>().numPlanetsSpawned;

        for (int i = 0; i < GameObject.Find("PlanetSpawner").GetComponent<PlanetSpawner>().numPlanetsSpawned; i++)
        {
            GameObject.Find("planet" + i.ToString()).SendMessage("SetSelected", ActivePlanetId);
        }
    }

    public void PlanetClicked(int id)
    {
      GameObject.Find("planet" + id.ToString()).GetComponent<Planet>().population += popClick;
    }

}
