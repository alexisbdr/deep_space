using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickUpgrade : MonoBehaviour {

    Details detailsObj;
    Color _buttonNotClickableColor;
    Color _buttonClickableColor;

    // Use this for initialization
    void Start () {
        gameObject.GetComponent<Button>().onClick.AddListener(OnClickListener);
        detailsObj = GameObject.Find("DetailsCanvas").GetComponent<Details>();
        _buttonNotClickableColor = new Color(1f, 1f, 1f, 0.1f);
        _buttonClickableColor = new Color(1f, 0.8431373f, 0f);
    }

    // Update is called once per frame
    void Update () {
        if (detailsObj.science >= detailsObj.clickUpgradeCost)
        {
            //make button look clickable
            gameObject.GetComponent<Image>().color = _buttonClickableColor;
        }
        else
        {
            //make button look not clickable
            gameObject.GetComponent<Image>().color = _buttonNotClickableColor;
        }
    }

    void OnClickListener()
    {
        if (detailsObj.science >= detailsObj.clickUpgradeCost)
        {
            gameObject.GetComponent<AudioSource>().Play(0);
            detailsObj.science -= detailsObj.clickUpgradeCost;
            detailsObj.clickUpgradeCost *= generalData.upgradeClickCostScale;
            detailsObj.popClick *= generalData.popClickScale;
            if (GameObject.Find("TutorialText"))
            {
                GameObject.Find("TutorialText").SendMessage("OnUpgradeClicks");
            }
        }
    }
}
