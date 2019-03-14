using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncreasePopulationGrowth : MonoBehaviour {

    GameObject detailsObj;
    Color _buttonNotClickableColor;
    Color _buttonClickableColor;


    // Use this for initialization
    void Start ()
    {
        //TODO: make listener persistent?
        gameObject.GetComponent<Button>().onClick.AddListener(OnClickListener);
        detailsObj = GameObject.Find("DetailsCanvas");
        _buttonNotClickableColor = new Color(1f, 1f, 1f, 0.1f);
        _buttonClickableColor = Color.white;
    }

    void Update()
    {
        int ActivePlanetId = detailsObj.GetComponent<Details>().ActivePlanetId;
        var planet = GameObject.Find("planet" + ActivePlanetId.ToString()).GetComponent<Planet>();
        if (planet.cryptocoins > planet.autoGrowthCost)
        {
            //make button look clickable
            gameObject.GetComponent<Image>().color = _buttonClickableColor;
        } else
        {
            //make button look not clickable
            gameObject.GetComponent<Image>().color = _buttonNotClickableColor;
        }
    }

    void OnClickListener()
    {
        int ActivePlanetId = detailsObj.GetComponent<Details>().ActivePlanetId;
        var planet = GameObject.Find("planet" + ActivePlanetId.ToString()).GetComponent<Planet>();
        ClickIconSpawner iconSpawner = GameObject.Find("ClickIconSpawner").GetComponent<ClickIconSpawner>();
        if (planet.cryptocoins > planet.autoGrowthCost)
        {
            gameObject.GetComponent<AudioSource>().Play(0);
            iconSpawner.CreateIcon(gameObject);
            planet.cryptocoins -= planet.autoGrowthCost;
            planet.AddPopulationGrowth();
            if (GameObject.Find("TutorialText"))
            {
                GameObject.Find("TutorialText").SendMessage("OnBuyAutoClicked");
            }
        }
    }
}
