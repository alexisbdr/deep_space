using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

public class Details : MonoBehaviour
{

    //GUI elements
    Canvas DetailsCanvas;
    GameObject PlanetDetailsPanelObj;

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

    //which planet was clicked most recently
    public int ActivePlanetId;

    // Use this for initialization
    void Start()
    {
        PlanetDetailsPanelObj = GameObject.Find("PlanetDetailsPanel");
        PlanetDetailsPanelObj.SetActive(false);
        money = 0;
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
            GameObject.Find("PopValue").GetComponent<Text>().text = Math.Floor(planet.population).ToString();
            GameObject.Find("ProductivityValue").GetComponent<Text>().text = planet.productivity.ToString();
            GameObject.Find("PlanetNameText").GetComponent<Text>().text = planet.planetName;

            GameObject.Find("SpawnPlanet").transform.Find("PopCost").GetComponent<Text>().text =
                planet.newPlanetPopThreshold.ToString();

            if (!planet.newPlanetSpawned && planet.population >= planetData.newPlanetPopThreshold)
            {
                var imageButton = GameObject.Find("SpawnPlanet").transform.Find("PopButton").GetComponent<Image>();
                imageButton.color = _buttonClickableColor;
                imageButton.sprite = buttonClickableSprite;
            }
            else
            {
                var imageButton = GameObject.Find("SpawnPlanet").transform.Find("PopButton").GetComponent<Image>();
                imageButton.color = _buttonNotClickableColor;
                imageButton.sprite = null;
            }
            
            
        }
        
           
   
        //Productivity mechanics
        double globalProductivityRate = 0;
        double popSum = 0;
        foreach (var planet in FindObjectsOfType<Planet>())
        {
            popSum += planet.population;
            globalProductivityRate += planet.population * planet.productivity;
        }
        money += Time.deltaTime * globalProductivityRate;

        GameObject.Find("MoneyValue").GetComponent<Text>().text = Math.Floor(money).ToString();

        GameObject.Find("GlobalPopValue").GetComponent<Text>().text = Math.Floor(popSum).ToString();
        GameObject.Find("MoneyRateValue").GetComponent<Text>().text = globalProductivityRate.ToString("F2");
    }

    //use this for value updates
    private void FixedUpdate()
    {
        

    }

    void SetActivePlanetID(int id)
    {
        ActivePlanetId = id;
        
        GameObject.Find("planet" + id.ToString()).SendMessage("SetSelected", ActivePlanetId);

    }

    void DecrementActivePlanetID()
    {
        //Add numplanets to avoid negative remainder
        //In C#, % operator can return negative number
        ActivePlanetId = (generalData.numPlanets+ActivePlanetId - 1) % generalData.numPlanets;
        
        GameObject.Find("planet" + ActivePlanetId.ToString()).SendMessage("SetSelected", ActivePlanetId);
    }

    void IncrementActivePlanetID()
    {
        ActivePlanetId = (ActivePlanetId + 1)% generalData.numPlanets;
        
        GameObject.Find("planet" + ActivePlanetId.ToString()).SendMessage("SetSelected", ActivePlanetId);
    }

    public void PlanetClicked(int id)
    {
      GameObject.Find("planet" + id.ToString()).GetComponent<Planet>().population += planetData.popClick;
    }

}
