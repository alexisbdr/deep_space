using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetScience : MonoBehaviour {

	GameObject detailsObj;
	Color _buttonNotClickableColor;
	Color _buttonClickableColor;

    private int _sciencePoints = 1;
    public int sciencePoints
    {
        get { return _sciencePoints; }
        set { _sciencePoints = value; }
    }
	// Use this for initialization
	void Start ()
	{
		//TODO: make listener persistent?
		gameObject.GetComponent<Button>().onClick.AddListener(OnClickListener);
		detailsObj = GameObject.Find("DetailsCanvas");
		_buttonNotClickableColor = new Color(1f, 1f, 1f, 0.1f);
		_buttonClickableColor = new Color(1f, 0.8431373f, 0f);
	}

	void Update()
	{
		int ActivePlanetId = detailsObj.GetComponent<Details>().ActivePlanetId;
		var planet = GameObject.Find("planet" + ActivePlanetId.ToString()).GetComponent<Planet>();
		if (planet.population > planet.sciencePopCost)
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
		if (planet.population > planet.sciencePopCost)
        {
            gameObject.GetComponent<AudioSource>().Play(0);
            detailsObj.GetComponent<Details>().science += sciencePoints;
			planet.sciencePopCost *= planetData.planetSciencePopCostScale;
		}
	}
}
