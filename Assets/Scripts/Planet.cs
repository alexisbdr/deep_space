using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Security.Permissions;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.UIElements;


public class Planet : MonoBehaviour {

    //Sprites
    //public Sprite InhabitedPlanetSprite;
    //public Sprite InhabitedPlanetSelectedSprite;
    //public Sprite UninhabitedPlanetSprite;
    //public Sprite UninhabitedPlanetSelectedSprite;
    public List<Sprite> inhabitedSprites;
    public List<Sprite> uninhabitedSprites;

    //Materials
    public Material defaultMaterial;

    //GUI elements
    Canvas DetailsCanvas;
    GameObject PlanetDetailsPanelObj;

    //whether or not this planet appears in planet details
    bool IsSelected=false;
    
    //set this to true when population is high enough
    //dictates whether inhabited or uninhabited sprite is used
    bool IsTerraformed=false;

    //Planet Data that varies by planet basis 
    public string planetName;

    private int _planetID;
    public int planetID
    {
        get { return _planetID; }
        set { _planetID = value; }
    }

    // use for when player hovers over planet and planet should stop moving
    private bool _planetMoving = true;
        
    private float _Theta; 
    public float Theta
    {
      get { return _Theta; }
        set { _Theta = value; }
    }
    private float _R = 1; 
    public float R
    {
      get { return _R; }
        set { _R = value; }
    }

    private double _population;
    public double population
    {
        get { return _population; }
        set { _population = value; }
    }

 
    // text like "+100" that will fly above planet when clicked
    public GameObject planetClickAnimation;
    // how much to scale planet when hovered
    public float scalePlanetHover;
    public float scalePlanetClick;


    public bool newPlanetSpawned = false;
    public double newPlanetPopThreshold;
    public double productivity;
    public double popCapacity;
    public double popIncreaseCost;
    public double popGrowthRate;
    public double colonizeMoneyCost;
    public double fixedPopGrowth;
    public double autoGrowthCost;
    public double productivityGrowthCost;
    public double cryptocoins;
    public double sciencePopCost;

    //Label Stuff - Just in case
    public GameObject planetLabel; 
    
    //Badge Stuff 
    public GameObject planetBadgeClick;
    private GameObject _planetBadgeClick;   
    public GameObject planetBadgeProd;
    private GameObject _planetBadgeProd;
    public GameObject planetBadgeScience;
    private GameObject _planetBadgeScience;

    private bool clickBadge = false;
    private bool prodBadge = false;
    private bool scienceBadge = false; 
 
    // Use this for initialization
    void Start()
    {
        newPlanetPopThreshold = planetData.newPlanetPopThreshold;
        popGrowthRate = planetData.popGrowthRate;
        colonizeMoneyCost = planetData.colonizeMoneyCost;
        autoGrowthCost = planetData.popAutoGrowthCostBase;
        scalePlanetHover = planetData.scalePlanetHover;
        scalePlanetClick = planetData.scalePlanetClick;


        productivityGrowthCost = (_planetID+1) * productivity;
        
        cryptocoins = _planetID * planetData.planetStartCryptoScale;
        sciencePopCost = planetData.planetSciencePopCost;

        //Initialize GUI elements
        DetailsCanvas = GameObject.Find("DetailsCanvas").GetComponent<Canvas>();
        PlanetDetailsPanelObj = DetailsCanvas.transform.Find("PlanetDetailsPanel").gameObject;

        //Theta
        Theta = UnityEngine.Random.value * 2 * Mathf.PI;
        fixedPopGrowth = 0;

        //Initialize label and badge elements as children of the current planet
        Instantiate(planetLabel).transform.parent = gameObject.transform;

        inhabitedSprites = new List<Sprite>(Resources.LoadAll<Sprite>("Sprites/inhabited"));
        uninhabitedSprites = new List<Sprite>(Resources.LoadAll<Sprite>("Sprites/uninhabited"));
        gameObject.GetComponent<SpriteRenderer>().sprite = uninhabitedSprites[UnityEngine.Random.Range(0,uninhabitedSprites.Count)];

        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        int numSegments = 500;
        lineRenderer.positionCount = numSegments+1;
        lineRenderer.widthMultiplier = 0.02f;
        lineRenderer.sortingOrder = -1;
        lineRenderer.startColor = new Color(1, 1, 1);
        lineRenderer.endColor = new Color(1, 1, 1);
        defaultMaterial = Resources.Load<Material>("Materials/defaultMaterial");
        lineRenderer.material = new Material(defaultMaterial);
        
        float theta_orbit = 0f;
        
        for (int i = 0; i < numSegments+1; i++)
        {
            Vector2 position = new Vector2(R * Mathf.Cos(theta_orbit), R * Mathf.Sin(theta_orbit));
            theta_orbit += Mathf.PI * 2 / numSegments;
            gameObject.GetComponent<LineRenderer>().SetPosition(i, position);
        }
    }

    // Update is called once per frame
    void Update () {
	    if (_planetMoving)
	    {
	        Vector2 position = new Vector2(R*Mathf.Cos(Theta), R*Mathf.Sin(Theta));
	        transform.parent.position = position;
	    }

        if (cryptocoins > autoGrowthCost && !clickBadge)
        {
            _planetBadgeClick = planetBadgeClick;
            _planetBadgeClick = Instantiate(_planetBadgeClick);
            _planetBadgeClick.transform.parent = gameObject.transform;
            clickBadge = true;
        }
        else if(cryptocoins < autoGrowthCost && clickBadge)
        {
            Destroy(_planetBadgeClick.gameObject);
            clickBadge = false;
        }

        if (cryptocoins > productivityGrowthCost && !prodBadge)
        {
            _planetBadgeProd = planetBadgeProd;
            _planetBadgeProd = Instantiate(_planetBadgeProd);
            _planetBadgeProd.transform.parent = gameObject.transform;
            prodBadge = true;
        }
        else if (cryptocoins < productivityGrowthCost && prodBadge)
        {
            Destroy(_planetBadgeProd.gameObject);
            prodBadge = false;
        }

        if (population > sciencePopCost && !scienceBadge)
        {
            _planetBadgeScience = planetBadgeScience;
            _planetBadgeScience = Instantiate(_planetBadgeScience);
            _planetBadgeScience.transform.parent = gameObject.transform;
            scienceBadge = true;
        }
        else if (population < sciencePopCost && scienceBadge)
        {
            Destroy(_planetBadgeScience);
            scienceBadge = false;
        }
      
        
    }

    private void FixedUpdate()
    {
        if (_planetMoving)
        {
            //simplified astrophysics
            Theta += Time.fixedDeltaTime / (10 * R * R);
            Theta = Theta % (2 * Mathf.PI);
        }
        
        //Sigmoid approximation of population growth //Could try to refine this
        double morePop = popGrowthRate * population *
                         (Math.Log10(Math.Max(1, popCapacity)) - Math.Log10(Math.Max(1, population))) *
                         Time.fixedDeltaTime +
                         fixedPopGrowth * Time.deltaTime * DetailsCanvas.GetComponent<Details>().popClick;
        
        population += morePop;
        DetailsCanvas.GetComponent<Details>().universalPopulation += morePop;
        
        //Updating population
        cryptocoins += productivity * Time.deltaTime;
    }

    //Hovering behavior
    private void OnMouseEnter()
    {
        //Update GameObject
        gameObject.transform.localScale = new Vector3(scalePlanetHover, scalePlanetHover, scalePlanetHover);
        _planetMoving = false;
    }

    private void OnMouseExit()
    {
        if (!IsSelected)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        } else
        {
            gameObject.transform.localScale = new Vector3(scalePlanetHover, scalePlanetHover, scalePlanetHover);
        }
        _planetMoving = true;
    }

    private void OnMouseUpAsButton()
    {
        gameObject.transform.localScale = new Vector3(scalePlanetHover, scalePlanetHover, scalePlanetHover);
    }

    private void OnMouseDown()
    {
        PlanetDetailsPanelObj.SetActive(true);
        DetailsCanvas.SendMessage("SetActivePlanetID", planetID);

        // Logic and Animation for each Planet Click
        if (GameObject.Find("TutorialText"))
        {
            GameObject.Find("TutorialText").SendMessage("OnPlanetClicked");
        }
        DetailsCanvas.SendMessage("PlanetClicked", planetID);
        Instantiate(planetClickAnimation).transform.parent = gameObject.transform;
        gameObject.transform.localScale = new Vector3(scalePlanetClick, scalePlanetClick, scalePlanetClick);
    }

    public void AssignID(int id)
    {
        planetID = id;
        R = (1.3f + id) * 0.8f;
        gameObject.name = "planet" + planetID.ToString();
    }

    //Call either SetStarting or SetNonStarting on spawn
    public void SetStarting()
    {
        //gameObject.GetComponent<SpriteRenderer>().sprite = InhabitedPlanetSprite;
        //IsTerraformed = true;
        IsTerraformed = false;
        IsSelected = false;
    }

    public void SetNonStarting() 
    {
        //gameObject.GetComponent<SpriteRenderer>().sprite = UninhabitedPlanetSprite;
        IsTerraformed = false;
        IsSelected = false;
    }

    /*
    public void AddPopulation()
    {
        Details gameDetails = GameObject.Find("DetailsCanvas").GetComponent<Details>();
        int ActivePlanetId = gameDetails.ActivePlanetId;
        Planet activePlanet = GameObject.Find("planet" + ActivePlanetId.ToString()).GetComponent<Planet>();
        if (population >= planetData.popIncreaseThreshold && gameDetails.money > activePlanet.popIncreaseCost)
        {
            activePlanet.population += planetData.popIncreaseModifier;
            gameDetails.money -= popIncreaseCost; 
            popIncreaseCost *= planetData.popIncreaseCostScale;
        }
        else if (activePlanet.population < planetData.popIncreaseThreshold && gameDetails.money > planetData.colonizeMoneyCost)
        {
            for (int i = 0; i < generalData.numPlanets; i++)
            {
                Planet selectedPlanet = GameObject.Find("planet" + i.ToString()).GetComponent<Planet>();
                selectedPlanet.population -= planetData.colonizePopCost;
                if (i != ActivePlanetId 
                    && selectedPlanet.population >= planetData.popIncreaseThreshold 
                    && (selectedPlanet.population - planetData.popIncreaseThreshold) >= planetData.colonizePopCost)
                {
                    selectedPlanet.population -= planetData.colonizePopCost;
                    gameDetails.money -= planetData.colonizeMoneyCost;
                    activePlanet.population += planetData.colonizePopCost;
                    colonizeMoneyCost *= planetData.colonizeMoneyCostScale;
                    break;
                }
            }
        }
    }
    */

    public void AddPopulationGrowth()
    {
        fixedPopGrowth += 1;
        autoGrowthCost *= planetData.popAutoGrowthCostScale;
    }

    public void AddProductivityGrowth()
    {
        productivityGrowthCost *= planetData.productivityGrowthCostScale;
        productivity *= planetData.productivityUpgradeScale;
    }

    //The planet is selected in the details component
    public void SetSelected(int id)
    {
        if (id == planetID)
        {
            IsSelected = true;
            gameObject.transform.localScale = new Vector3(scalePlanetHover, scalePlanetHover, scalePlanetHover);
        }
        else
        {
            IsSelected = false;
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        /**
        if (id == planetID)
        {
            IsSelected = true;
            if (IsTerraformed)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = InhabitedPlanetSelectedSprite;
            } else
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = UninhabitedPlanetSelectedSprite;
            }
        } else
        {
            IsSelected = false;
            if (IsTerraformed)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = InhabitedPlanetSprite;
            } else
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = UninhabitedPlanetSprite;
            }
        }
        **/
    }
}
