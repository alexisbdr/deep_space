using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using Image = UnityEngine.UI.Image;

public class NextLvl : MonoBehaviour
{

	Details detailsObj;
	Color _buttonNotClickableColor;
	Color _buttonClickableColor;
	private GameObject star;
	List<GameObject> openPanelsBeforeNewLvl = new List<GameObject>();
	private PlanetSpawner planetSpawner;
	private GameObject investScience;
	private GameObject clonePanel;
	
	private bool is_animating = false;
	private float animTimePassed = 0;
	private float turnLargeAnimDuration = 1;
	private float pauseDuration = 2;
	private float flyAnimDuration = 0.3f;
	private Vector3 largestSize;
	private bool rotateRight = true;


	// Use this for initialization
	void Start()
	{
		gameObject.GetComponent<Button>().onClick.AddListener(OnClickListener);
		detailsObj = GameObject.Find("DetailsCanvas").GetComponent<Details>();
		_buttonNotClickableColor = new Color(0f, 0f, 0f, 0.1f);
		_buttonClickableColor = new Color(1f, 0.8431373f, 0f);
		star = GameObject.Find("star" + detailsObj.level);
		planetSpawner = GameObject.Find("PlanetSpawner").GetComponent<PlanetSpawner>();
		investScience = GameObject.Find("InvestScience");
		clonePanel = GameObject.Find("ClonePanel");
		investScience.SetActive(false);
		clonePanel.SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{
		if (planetSpawner.numPlanetsSpawned >= generalData.numPlanetsMax)
		{
			gameObject.GetComponent<Image>().color = _buttonClickableColor;
		}
		else
		{
			gameObject.GetComponent<Image>().color = _buttonNotClickableColor;
		}

		if (is_animating)
		{
			AnimateStar(); // calls EndAnimation when done, where new level actually begins
		}
	}

	void OnClickListener()
	{
		if (planetSpawner.numPlanetsSpawned >= generalData.numPlanetsMax)
		{
			star = GameObject.Find("star" + detailsObj.level);
			StartNewLvl();
			is_animating = true;
		}
	}

	void StartNewLvl()
	{
		openPanelsBeforeNewLvl.Clear();
		openPanelsBeforeNewLvl.Add(GameObject.Find("PlanetDetailsPanel"));
		foreach (var go in openPanelsBeforeNewLvl)
		{
			if (go != null)
			{
				go.SetActive(false);
			}
		}
		
		foreach (var planet in FindObjectsOfType<Planet>())
		{
			Destroy(planet.gameObject);
		}
		
		foreach (var ship in FindObjectsOfType<Ship>())
		{
			Destroy(ship.gameObject);
		}
	}

	void EndAnimation()
	{
		detailsObj.ActivePlanetId = 0;
		detailsObj.popFromPreviousSystems = detailsObj.universalPopulation;
		PlanetSpawner planetSpawner = GameObject.Find("PlanetSpawner").GetComponent<PlanetSpawner>();
		planetSpawner.numPlanetsSpawned = 0;
		planetSpawner.CreateNewPlanet();
		detailsObj.CreateStar();
		detailsObj.ActivateSpawnPlanetUpgrade();
		foreach (var go in openPanelsBeforeNewLvl)
		{
			if (go != null)
			{
				go.SetActive(true);
			}
		}

		if (detailsObj.level == 2)
		{
			investScience.SetActive(true);
			
		}

		// change upgrade to level 3 for future versions of game
		if (detailsObj.level == 2)
		{
			clonePanel.SetActive(true);
		}
		
		if (clonePanel.activeSelf)
		{
			clonePanel.GetComponent<ClonePanel>().cloningPossibleBasedOnPop = true;
			GameObject.Find("CloneResult").GetComponent<Text>().text = "Try cloning your people!";
		}
	}

	void AnimateStar()
	{
		
		if (animTimePassed < turnLargeAnimDuration)
		{
			star.transform.localScale += new Vector3(0.3f*animTimePassed, 0.3f*animTimePassed, 0.3f*animTimePassed);
			largestSize = star.transform.localScale;
			WiggleStar();
			
		}
			
		else if (animTimePassed < turnLargeAnimDuration + pauseDuration)
		{
			WiggleStar();
		}
		
		else
		{
			float remainingTime = animTimePassed - turnLargeAnimDuration - pauseDuration;
			float normalizedTime = remainingTime / flyAnimDuration;
			star.transform.Rotate(0, 0, 2);
			Vector3 endPoint = new Vector3(-9 + detailsObj.level, 4.1f, 0);
			star.transform.position = Vector3.Lerp(new Vector3(0,0,0), endPoint, normalizedTime);
			star.transform.localScale = Vector3.Lerp(largestSize, new Vector3(1.5f, 1.5f, 1.5f), normalizedTime);
		}
		
		animTimePassed += Time.deltaTime;

		if (animTimePassed > turnLargeAnimDuration + flyAnimDuration + pauseDuration)
		{
			star.transform.localEulerAngles = new Vector3(0, 0, 0);
			is_animating = false;
			animTimePassed = 0;
			EndAnimation();
		}
	}

	void WiggleStar()
	{
		if (rotateRight)
		{
			star.transform.Rotate(0, 0, 2f);
		}
		else
		{
			star.transform.Rotate(0, 0, -2f);
		}
				
				
		if (star.transform.eulerAngles.z > 10 || star.transform.eulerAngles.z < -10)
		{
			rotateRight = !rotateRight;
		}
	}
}

