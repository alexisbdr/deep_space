using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class ScienceGamble : MonoBehaviour
{
	GameObject loadingIcon1;
	GameObject loadingIcon2;
	GameObject loadingIcon3;
	Button lowRiskButton;
	Button mediumRiskButton;
	Button highRiskButton;
	GameObject gambleButton;
	GameObject detailsObj;
	Text gambleResultText;

	private int scienceTaken = 0;
	private int scienceReceived = 0;

	string currRisk;
	private bool canGamble = false;
	private bool isGambling = false;
	private float gambleTimePassed = 0;
	private float loadingAnimationDuration = 4; // seconds
	private float totalAnimationDuration = 12; // make sure it's a multiple of loadingAnimationDuration 
	
	Color _gambleButtonNotClickableColor;
	Color _gambleButtonClickableColor;
	
	
	
	
	// Use this for initialization
	void Start () {
		loadingIcon1 = GameObject.Find("gamble-loading-1");
		loadingIcon2 = GameObject.Find("gamble-loading-2");
		loadingIcon3 = GameObject.Find("gamble-loading-3");
		loadingIcon1.SetActive(false);
		loadingIcon2.SetActive(false);
		loadingIcon3.SetActive(false);
		
		detailsObj = GameObject.Find("DetailsCanvas");

		gambleResultText = GameObject.Find("GambleResult").GetComponent<Text>();
		
		currRisk = "low";

		gambleButton = GameObject.Find("GambleButton");
		gambleButton.GetComponent<Button>().onClick.AddListener(GambleButtonClicked);
		
		_gambleButtonNotClickableColor = new Color(0f, 0f, 0f, 0.1f);
		_gambleButtonClickableColor = Color.white;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (!isGambling && detailsObj.GetComponent<Details>().science >= 1)
		{
			canGamble = true;
		}

		if (canGamble)
		{
			gambleButton.GetComponent<Image>().color = _gambleButtonClickableColor;
		}
		else
		{
			gambleButton.GetComponent<Image>().color = _gambleButtonNotClickableColor;
		}

		if (isGambling)
		{
			UpdateLoadingAnimation();
		}
		
	}

	void GambleButtonClicked()
	{
		if (canGamble)
		{
			StartGambling();
		}
	}

	void StartGambling()
	{
		isGambling = true;
		canGamble = false;
		
		scienceTaken = (int) Mathf.Ceil((float) detailsObj.GetComponent<Details>().science / 2);
		detailsObj.GetComponent<Details>().science -= scienceTaken;
		
		scienceReceived = Random.Range(0, 2 * scienceTaken + 1);
		gambleResultText.text = "Gambling " + scienceTaken + " Science...";
	}

	void EndGambling()
	{
		isGambling = false;
		gambleTimePassed = 0;
		loadingIcon1.SetActive(false);
		loadingIcon2.SetActive(false);
		loadingIcon3.SetActive(false);
		
		detailsObj.GetComponent<Details>().science += scienceReceived;
		
		if (scienceReceived > scienceTaken)
		{
			gambleResultText.text = "Congrats! You gained " + (scienceReceived - scienceTaken) + " points.";
		}
		else if (scienceReceived < scienceTaken)
		{
			gambleResultText.text = "Uh oh! You lost " + (scienceTaken - scienceReceived) + " points.";
		}
		else
		{
			gambleResultText.text = "You gained 0 points. Try again!";
		}
		
	}

	void UpdateLoadingAnimation()
	{
		loadingIcon1.SetActive(true);
		float animationPhase = gambleTimePassed % loadingAnimationDuration;
		float phase1 = loadingAnimationDuration / 4;
		float phase2 = loadingAnimationDuration * 2 / 4;
		float phase3 = loadingAnimationDuration * 3 / 4;
			
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
			
		gambleTimePassed += Time.deltaTime;
		if (gambleTimePassed > totalAnimationDuration)
		{
			EndGambling();
		}
	}
	
	
	
	
}
