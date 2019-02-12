using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class planetData {

    public const int numPlanets = 8;

    public const double taxRate = 0.0001;

    //Planet Population Constants 
    public const int startingPopulation = 1000000;
    public const long populationCapacityMain = 1000000000000;
    public const long populationCapacity = 6000000000000;

    //modifier for pop gain when population increase button is added
    public const double PopIncreaseModifier = 100000;
    //money cost for pop gain when population increase button is pushed
    public const double PopIncreaseCostBase = 1000;
    //threshold for pop base necessary to press gain pop button
    public const double PopIncreaseThreshold = 100000;
    //modifier for pop growth on updates
    public const double PopGrowthRate = 0.0003;
    //money cost for colonization of new planet
    public const double ColonizeMoneyCost = 10000;
    //pop cost for colonization of new planet
    public const double ColonizePopCost = 10000;
}

public class Planet : MonoBehaviour {

    public Sprite StartingPlanetSprite;
    public Sprite UninhabitedPlanetSprite;

    //GUI elements
    Canvas DetailsCanvas;
    GameObject PlanetDetailsPanelObj;

    

    string PlanetName;

    //planet data
    int PlanetID;
    float Theta;
    float R=1;

    // Use this for initialization
    void Start () {
        //Initialize GUI elements
        DetailsCanvas = GameObject.Find("DetailsCanvas").GetComponent<Canvas>();
        PlanetDetailsPanelObj = DetailsCanvas.transform.Find("PlanetDetailsPanel").gameObject;
        //Theta
        Theta = Random.value * 2*Mathf.PI;
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 position = new Vector2(R*Mathf.Cos(Theta), R*Mathf.Sin(Theta));
        transform.position = position;
	}

    private void FixedUpdate()
    {
        //simplified astrophysics
        Theta += Time.fixedDeltaTime / (10*R * R);
        Theta = Theta % (2 * Mathf.PI);
    }

    private void OnMouseDown()
    {
        if (!PlanetDetailsPanelObj.activeSelf)
        {
            PlanetDetailsPanelObj.SetActive(true);
            GameObject.Find("DetailsCanvas").SendMessage("SetActivePlanetID", PlanetID);
        }
    }

    public void AssignID(int id)
    {
        PlanetID = id;
        R = (1.5f + id)*0.6f;
        gameObject.name = "planet" + PlanetID.ToString();
    }

    public void SetStarting()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = StartingPlanetSprite;
    }

    public void SetNonStarting()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = UninhabitedPlanetSprite;
    }
}
