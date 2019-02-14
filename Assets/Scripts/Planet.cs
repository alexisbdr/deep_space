using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Planet : MonoBehaviour {

    public Sprite StartingPlanetSprite;
    public Sprite UninhabitedPlanetSprite;

    //GUI elements
    Canvas DetailsCanvas;
    GameObject PlanetDetailsPanelObj;

    //Planet Data that varies by planet basis 
    public string planetName;
    int planetID;
    float Theta;
    float R=1;

    public double population;
    public double taxRate;
    public double popCapacity;
    public double popIncreaseCost;
    public double popGrowthRate = planetData.popGrowthRate;

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
        Theta += Time.fixedDeltaTime / (10 * R * R);
        Theta = Theta % (2 * Mathf.PI);

        //Sigmoid approximation of population growth //Could try to refine this
        population += popGrowthRate * population *
                      (Mathf.Log10(Mathf.Max(1, popCapacity)) - Mathf.Log10(Mathf.Max(1, population))) *
                      Time.fixedDeltaTime;
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

    public void SetStartingSprite(bool is_first)
    {
        if (is_first)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = StartingPlanetSprite;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = UninhabitedPlanetSprite;
        }
    }

    public void AddPopulation()
    {
        Debug.Log("I was found");
        /***
        if (population >= PopIncreaseThreshold && money > PopIncreaseCosts[ActivePlanetId])
        {
            Populations[ActivePlanetId] += PopIncreaseModifier;
            money -= PopIncreaseCosts[ActivePlanetId];
            PopIncreaseCosts[ActivePlanetId] *= 1.05;
        }
        else if (Populations[ActivePlanetId] < PopIncreaseThreshold && money > ColonizeMoneyCost)
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
        ***/
    }
}
