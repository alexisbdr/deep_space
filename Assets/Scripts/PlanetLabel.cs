using System.Collections;
using System.Collections.Generic;
using  UnityEngine; 
using UnityEngine.UI;
using System; 

public class PlanetLabel : MonoBehaviour
{
    private int population;
    private TextMesh _textMesh;

    private Planet parentPlanet;
    private string labelName;

    private bool _hover;
    public bool hover
    {
        get { return _hover; }
        set { _hover = value; }
    }

    private bool _click;
    public bool click 
    {
        get { return _click; }
        set { _click = value; }
    }

    private void Start()
    {
        //Get Parent planet informationm 
        parentPlanet = transform.parent.GetComponent<Planet>();
        Debug.Log(transform.parent.name);
        AssignName(transform.parent.name);

        _textMesh = GetComponent<TextMesh>();
        _textMesh.text = population.ToString();
    }

    void Update()
    {
        _textMesh.text = Math.Floor(parentPlanet.population).ToString();
        updateLabelPosn();
    }

    public void AssignName(string parentName)
    {
        int id = parentName[parentName.Length - 1];
        gameObject.name = "planetLabel" + id;
    }

    public void updateLabelPosn()
    {
        float new_posn_x = transform.parent.position.x + planetData.labelOffset[0];
        float new_posn_y = transform.parent.position.y + planetData.labelOffset[1];
        if(hover)
        {
            new_posn_y += parentPlanet.scalePlanetHover;
        }

        if(click)
        {
            new_posn_y += parentPlanet.scalePlanetClick;
        }
        _textMesh.transform.position = new Vector2(new_posn_x, new_posn_y);
    }
}