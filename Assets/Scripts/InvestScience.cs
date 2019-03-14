using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class InvestScience : MonoBehaviour
{
	GameObject loadingIcon1;
	GameObject loadingIcon2;
	GameObject loadingIcon3;
	GameObject investButton;
	Details detailsObj;
	Text investResultText;

	private bool canInvest = false;
	private bool isInvesting = false;
	private float investTimePassed = 0;
	private float loadingAnimationDuration = 4; // seconds
	private float totalAnimationDuration = 12; // make sure it's a multiple of loadingAnimationDuration 
	
	Color _buttonNotClickableColor;
	Color _buttonClickableColor;
	
	
	
	
	// Use this for initialization
	void Start () {
		loadingIcon1 = GameObject.Find("science-loading-1");
		loadingIcon2 = GameObject.Find("science-loading-2");
		loadingIcon3 = GameObject.Find("science-loading-3");
		loadingIcon1.SetActive(false);
		loadingIcon2.SetActive(false);
		loadingIcon3.SetActive(false);
		
		detailsObj = GameObject.Find("DetailsCanvas").GetComponent<Details>();

		investResultText = GameObject.Find("InvestResult").GetComponent<Text>();

		investButton = GameObject.Find("InvestButton");
		investButton.GetComponent<Button>().onClick.AddListener(InvestButtonClicked);
		
		_buttonNotClickableColor = new Color(1f, 1f, 1f, 0.1f);
		_buttonClickableColor = new Color(1f, 0.8431373f, 0f);
		
	}
	
	// Update is called once per frame
	void Update () {

		if (!isInvesting)
		{
			canInvest = true;
		}
		else
		{
			canInvest = false;
		}

		if (canInvest)
		{
			investButton.GetComponent<Image>().color = _buttonClickableColor;
		}
		else
		{
			investButton.GetComponent<Image>().color = _buttonNotClickableColor;
		}

		if (isInvesting)
		{
			UpdateLoadingAnimation();
		}
		
	}

	void InvestButtonClicked()
	{
		if (canInvest)
		{
			StartInvesting();
		}
	}

	void StartInvesting()
	{
		isInvesting = true;
		canInvest = false;
		investResultText.text = "Investing in Science...";
	}

	void EndInvesting()
	{
		isInvesting = false;
		investTimePassed = 0;
		loadingIcon1.SetActive(false);
		loadingIcon2.SetActive(false);
		loadingIcon3.SetActive(false);
		
		int scienceReceived = Random.Range((int) detailsObj.science, (int) (2 * detailsObj.science + 1));
		detailsObj.science += scienceReceived;

		investResultText.text = "You gained " + GameUtils.formatLargeNumber(scienceReceived) + " science!";
	}

	void UpdateLoadingAnimation()
	{
		loadingIcon1.SetActive(true);
		float animationPhase = investTimePassed % loadingAnimationDuration;
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
			
		investTimePassed += Time.deltaTime;
		if (investTimePassed > totalAnimationDuration)
		{
			EndInvesting();
		}
	}
	
	
	
	
}
