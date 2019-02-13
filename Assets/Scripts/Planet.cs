using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Planet : MonoBehaviour {

    public Sprite InhabitedPlanetSprite;
    public Sprite InhabitedPlanetSelectedSprite;
    public Sprite UninhabitedPlanetSprite;
    public Sprite UninhabitedPlanetSelectedSprite;

    //GUI elements
    Canvas DetailsCanvas;
    GameObject PlanetDetailsPanelObj;

    //planet data
    int PlanetID;
    float Theta;
    float R=1;
    bool IsSelected=false;
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
}
