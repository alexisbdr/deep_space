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
    string PlanetName;
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
