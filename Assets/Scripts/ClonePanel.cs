using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class ClonePanel : MonoBehaviour
{
	GameObject loadingIcon1;
	GameObject loadingIcon2;
	GameObject loadingIcon3;
	GameObject loadingIcon4;
	GameObject loadingIcon5;
	Button lowRiskButton;
	Button mediumRiskButton;
	Button highRiskButton;
	GameObject cloneButton;
	Details detailsObj;
	Text cloneResultText;

	private double popTaken = 0;
	private double popReceived = 0;

	string currRisk;
	private bool canClone = false;
	private bool isCloning = false;
	public bool cloningPossibleBasedOnPop;
	private float cloneTimePassed = 0;
	private float loadingAnimationDuration = 4; // seconds
	private float totalAnimationDuration = 12; // make sure it's a multiple of loadingAnimationDuration 
	
	Color _cloneButtonNotClickableColor;
	Color _cloneButtonClickableColor;

	private Color _riskButtonSelectedColor = Color.white;
	private Color _riskButtonNotSelectedColor = new Color(1f, 1f, 1f, 0.3921569f);
	
	
	// Use this for initialization
	void Start () {
		loadingIcon1 = GameObject.Find("man-loading-1");
		loadingIcon2 = GameObject.Find("man-loading-2");
		loadingIcon3 = GameObject.Find("man-loading-3");
		loadingIcon4 = GameObject.Find("man-loading-4");
		loadingIcon5 = GameObject.Find("man-loading-5");
		loadingIcon1.SetActive(false);
		loadingIcon2.SetActive(false);
		loadingIcon3.SetActive(false);
		loadingIcon4.SetActive(false);
		loadingIcon5.SetActive(false);
		
		detailsObj = GameObject.Find("DetailsCanvas").GetComponent<Details>();

		cloneResultText = GameObject.Find("CloneResult").GetComponent<Text>();
		
		currRisk = "low";
		lowRiskButton = GameObject.Find("LowRiskButton").GetComponent<Button>();
		mediumRiskButton = GameObject.Find("MediumRiskButton").GetComponent<Button>();
		highRiskButton = GameObject.Find("HighRiskButton").GetComponent<Button>();
		lowRiskButton.onClick.AddListener(delegate { RiskButtonClicked("low"); });
		mediumRiskButton.onClick.AddListener(delegate { RiskButtonClicked("medium"); });
		highRiskButton.onClick.AddListener(delegate { RiskButtonClicked("high"); });
		
		cloneButton = GameObject.Find("CloneButton");
		cloneButton.GetComponent<Button>().onClick.AddListener(cloneButtonClicked);
		
		_cloneButtonNotClickableColor = new Color(1f, 1f, 1f, 0.1f);
		_cloneButtonClickableColor = Color.white;
		cloningPossibleBasedOnPop = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (!isCloning && cloningPossibleBasedOnPop)
		{
			canClone = true;
		}
		else
		{
			canClone = false;
		}

		if (canClone)
		{
			cloneButton.GetComponent<Image>().color = _cloneButtonClickableColor;
		}
		else
		{
			cloneButton.GetComponent<Image>().color = _cloneButtonNotClickableColor;
		}

		if (isCloning)
		{
			UpdateLoadingAnimation();
		}
	}

	void cloneButtonClicked()
	{
		if (canClone)
		{
			StartCloning();
		}
	}

	void RiskButtonClicked(string risk)
	{
		lowRiskButton.gameObject.GetComponent<Image>().color = _riskButtonNotSelectedColor;
		mediumRiskButton.gameObject.GetComponent<Image>().color = _riskButtonNotSelectedColor;
		highRiskButton.gameObject.GetComponent<Image>().color = _riskButtonNotSelectedColor;
		if (risk == "low")
		{
			currRisk = "low";
			lowRiskButton.gameObject.GetComponent<Image>().color = _riskButtonSelectedColor;
		}
		
		if (risk == "medium")
		{
			currRisk = "medium";
			mediumRiskButton.gameObject.GetComponent<Image>().color = _riskButtonSelectedColor;
		}
		
		if (risk == "high")
		{
			currRisk = "high";
			highRiskButton.gameObject.GetComponent<Image>().color = _riskButtonSelectedColor;
		}
	}

	void StartCloning()
	{
		isCloning = true;
		canClone = false;

		double riskFactor = 0.1;
		if (currRisk == "low")
		{
			riskFactor = 0.1;
		}

		if (currRisk == "medium")
		{
			riskFactor = 0.5;
		}

		if (currRisk == "high")
		{
			riskFactor = 0.9;
		}
 
		popTaken = detailsObj.popFromPreviousSystems * riskFactor;
		detailsObj.popFromPreviousSystems -= popTaken;
		detailsObj.universalPopulation -= popTaken;

		popReceived = Random.Range(0, (float) (2 * popTaken) + 1);
		
		cloneResultText.text = "Cloning " + GameUtils.formatLargeNumber(popTaken) + " Population...";
	}

	void EndCloning()
	{
		isCloning = false;
		cloneTimePassed = 0;
		loadingIcon1.SetActive(false);
		loadingIcon2.SetActive(false);
		loadingIcon3.SetActive(false);
		loadingIcon4.SetActive(false);
		loadingIcon5.SetActive(false);
		
		detailsObj.popFromPreviousSystems += popReceived;
		detailsObj.universalPopulation += popReceived;
		
		if (popReceived >= popTaken)
		{
			cloneResultText.text = "Cloning gained " + GameUtils.formatLargeNumber(popReceived - popTaken) + " people.";
		}
		else if (popReceived < popTaken)
		{
			cloneResultText.text = "Cloning lost " + GameUtils.formatLargeNumber(popTaken - popReceived) + " people.";
		}

		if (detailsObj.popFromPreviousSystems < 100)
		{
			cloningPossibleBasedOnPop = false;
			cloneResultText.text = "Cannot clone until next level.";
		}
		
	}

	void UpdateLoadingAnimation()
	{
		loadingIcon1.SetActive(true);
		float animationPhase = cloneTimePassed % loadingAnimationDuration;
		float phase1 = loadingAnimationDuration / 6;
		float phase2 = loadingAnimationDuration * 2 / 6;
		float phase3 = loadingAnimationDuration * 3 / 6;
		float phase4 = loadingAnimationDuration * 4 / 6;
		float phase5 = loadingAnimationDuration * 5 / 6;
			
		if (animationPhase > phase1)
		{
			loadingIcon1.SetActive(true);
		}
		else
		{
			loadingIcon1.SetActive(false);
		}
			
		if (animationPhase > phase2)
		{
			loadingIcon2.SetActive(true);
		}
		else
		{
			loadingIcon2.SetActive(false);
		}
			
		if (animationPhase > phase3)
		{
			loadingIcon3.SetActive(true);
		}
		else
		{
			loadingIcon3.SetActive(false);
		}
			
		if (animationPhase > phase4)
		{
			loadingIcon4.SetActive(true);
		}
		else
		{
			loadingIcon4.SetActive(false);
		}
		
		if (animationPhase > phase5)
		{
			loadingIcon5.SetActive(true);
		}
		else
		{
			loadingIcon5.SetActive(false);
		}
		
		cloneTimePassed += Time.deltaTime;
		if (cloneTimePassed > totalAnimationDuration)
		{
			EndCloning();
		}
	}
}
