using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnPlanet : MonoBehaviour
{
	
	Details detailsScript;
	Color _buttonNotClickableColor;
	Color _buttonClickableColor;

	// Use this for initialization
	void Start ()
	{
		//TODO: make listener persistent?
		gameObject.GetComponent<Button>().onClick.AddListener(OnClickListener);
		detailsScript = GameObject.Find("DetailsCanvas").GetComponent<Details>();
		_buttonNotClickableColor = new Color(0f, 0f, 0f, 0.1f);
		_buttonClickableColor = Color.white;
	}
	
	
	void Update()
	{
		double popsum = 0;
		foreach (var p in FindObjectsOfType<Planet>())
		{
			popsum += p.population;
		}
		
		if (popsum >= detailsScript.planetSpawnThreshold)
		{
			gameObject.GetComponent<Image>().color = _buttonClickableColor;
		}
		else
		{
			gameObject.GetComponent<Image>().color = _buttonNotClickableColor;
		}
	}

	void OnClickListener()
	{
		
		double popsum = 0;
		foreach (var p in FindObjectsOfType<Planet>())
		{
			popsum += p.population;
		}
		
		if (popsum >= detailsScript.planetSpawnThreshold)
		{
			GameObject.Find("PlanetSpawner").GetComponent<PlanetSpawner>().CreateNewPlanet();
			detailsScript.planetSpawnThreshold *= generalData.planetSpawnThresholdScale;
		}
		
	}

}
