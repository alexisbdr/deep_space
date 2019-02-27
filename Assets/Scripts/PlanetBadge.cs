using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlanetBadge : MonoBehaviour
{
    //GUI Elements - need to attach sprite and text to it
    Canvas DetailsCanvas;

    private Text badgeText;
    private RectTransform badgeTransform;

    private Planet parentPlanet;
    private string badgeName;

    private float[] badgeOffset;

    private void Start()
    {
        //Initialize GUI elements
        //DetailsCanvas = GameObject.Find("DetailsCanvas").GetComponent<Canvas>();

        badgeTransform = gameObject.GetComponent<RectTransform>();
        parentPlanet = badgeTransform.parent.GetComponent<Planet>();
        badgeText = badgeTransform.GetChild(0).GetComponent<Text>();
        badgeText.text = Math.Floor(parentPlanet.popGrowthRate).ToString(); 
        
        AssignName(badgeTransform.parent.name);

        badgeOffset = planetData.prodBadgeOffset; 
    }

    public void Update()
    {
        badgeText.text = Math.Floor(parentPlanet.popGrowthRate).ToString();

        Vector2 parentPosn = badgeTransform.parent.position;
        float new_posn_x = parentPosn.x + badgeOffset[0];
        float new_posn_y = parentPosn.y + badgeOffset[1];
        gameObject.GetComponent<PlanetBadge>().transform.position = new Vector2(new_posn_x, new_posn_y);
    }

    public void AssignName(string parentName)
    {
        string id = parentName.Substring(parentName.Length - 1, 1);
        gameObject.name = "planetBadge" + id;
    }
}