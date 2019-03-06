using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

    int stage;

	// Use this for initialization
	void Start () {
        stage = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (stage == 3)
        {
            if (GameObject.Find("DetailsCanvas").GetComponent<Details>().universalPopulation >= 100)
            {
                stage = 4;
                gameObject.GetComponent<Text>().text = "With enough population, you can colonize more planets and get science points. Try spawning a planet now.";
            }
        } else if (stage == 4)
        {
            if (GameObject.Find("PlanetSpawner").GetComponent<PlanetSpawner>().numPlanetsSpawned > 1)
            {
                stage = 5;
                gameObject.GetComponent<Text>().text = "Good job! More planets means more population and more science points! Try getting some science points now.";
            }
        } else if (stage == 5)
        {
            if (GameObject.Find("DetailsCanvas").GetComponent<Details>().science >= 1)
            {
                stage = 6;
                gameObject.GetComponent<Text>().text = "Science points are necessary for important upgrades help your population grow faster. Try spending some science points upgrading your clicks.";
            }
        } else if (stage == 7)
        {
            if (GameObject.Find("PlanetSpawner").GetComponent<PlanetSpawner>().numPlanetsSpawned > 4)
            {
                stage = 5;
                gameObject.GetComponent<Text>().text = "You colonized this system in no time! When you're ready, click next level to colonize a new system and rebuild humanity.";
            }
        }
	}

    void OnPlanetClicked()
    {
        if (stage == 0)
        {
            stage = 1;
            gameObject.GetComponent<Text>().text = "Good! Clicking on a planet increases its population. Now click on the Increase Productivity button.";
        }
    }

    void OnIncProdClicked()
    {
        if (stage == 1)
        {
            stage = 2;
            gameObject.GetComponent<Text>().text = "Productivity is the rate at which population generates cryptocoins for a planet. Cryptocoins are used to improve your planets. Spend some cryptocoin by clicking the Buy Autoclicker button.";
        }
    }

    void OnBuyAutoClicked()
    {
        if (stage == 2)
        {
            stage = 3;
            gameObject.GetComponent<Text>().text = "Autoclickers make your planet's population grow. Improve your planets often to generate more population and cryptocoin. Try to reach 100 population.";
        }
    }

    void OnUpgradeClicks()
    {
        if (stage == 6)
        {
            stage = 7;
            gameObject.GetComponent<Text>().text = "Now your planets are growing faster! Continue improving your planets and buying upgrades until you have colonized the entire system. (5 planets required)";
        }
    }

    void OnNextLvlClicked()
    {
        stage = 8;
        gameObject.GetComponent<Text>().text = "";
    }
}
