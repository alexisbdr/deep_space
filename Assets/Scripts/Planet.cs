using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Planet : MonoBehaviour {

    //Sprites
    public Sprite InhabitedPlanetSprite;
    public Sprite InhabitedPlanetSelectedSprite;
    public Sprite UninhabitedPlanetSprite;
    public Sprite UninhabitedPlanetSelectedSprite;

    //GUI elements
    Canvas DetailsCanvas;
    GameObject PlanetDetailsPanelObj;

    //planet data
    //should be unique to each planet
    int PlanetID;

    //planet positioning data
    float Theta;
    float R=1;

    //whether or not this planet appears in planet details
    bool IsSelected=false;
    
    //set this to true when population is high enough
    //dictates whether inhabited or uninhabited sprite is used
    bool IsTerraformed=false;

	// Use this for initialization
	void Start () {
        DetailsCanvas = GameObject.Find("DetailsCanvas").GetComponent<Canvas>();
        PlanetDetailsPanelObj = DetailsCanvas.transform.Find("PlanetDetailsPanel").gameObject;
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
        PlanetDetailsPanelObj.SetActive(true);
        GameObject.Find("DetailsCanvas").SendMessage("SetActivePlanetID", PlanetID);
        GameObject.Find("DetailsCanvas").SendMessage("PlanetClicked", PlanetID);
    }
    
    public void AssignID(int id)
    {
        PlanetID = id;
        R = (1.5f + id)*0.6f;
        gameObject.name = "planet" + PlanetID.ToString();
    }

    //Call either SetStarting or SetNonStarting on spawn
    public void SetStarting()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = InhabitedPlanetSprite;
        IsTerraformed = true;
        IsSelected = false;
    }

    public void SetNonStarting()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = UninhabitedPlanetSprite;
        IsTerraformed = false;
        IsSelected = false;
    }

    //The planet is selected in the details component
    public void SetSelected(int id)
    {
        if (id == PlanetID)
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
    }
   
    public float GetR()
    {
        return R;
    }

    public float GetTheta()
    {
        return Theta;
    }
}
