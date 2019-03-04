using UnityEngine;
using System;


public class PlanetBadgeProd : MonoBehaviour
{

    private float[] badgeOffset;

    private void Start()
    {
        badgeOffset = planetData.prodBadgeOffset;

        float parentScale_x = transform.parent.localScale.x;
        gameObject.transform.localPosition = new Vector2(badgeOffset[0] * parentScale_x, badgeOffset[1]);

        AssignName(transform.parent.name);
    }

    public void Update()
    {
        float parentScale_x = transform.parent.localScale.x;
        gameObject.transform.localPosition = new Vector2(badgeOffset[0] * parentScale_x, badgeOffset[1]);
    }

    public void AssignName(string parentName)
    {
        string id = parentName.Substring(parentName.Length - 1, 1);
        gameObject.name = "planetBadgeProd" + id;
    }
}