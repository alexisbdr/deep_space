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

    public float[] labelOffset;

    private void Start()
    {
        //Get Parent planet informationm 
        parentPlanet = transform.parent.GetComponent<Planet>();
        AssignName(transform.parent.name);

        _textMesh = GetComponent<TextMesh>();
        _textMesh.text = population.ToString();

        labelOffset = planetData.labelOffset;
        Debug.Log(labelOffset);
    }

    void Update()
    {
        _textMesh.text = Math.Floor(parentPlanet.population).ToString();

        float new_posn_x = transform.parent.position.x + labelOffset[0];
        float new_posn_y = transform.parent.position.y + labelOffset[1];
        _textMesh.transform.position = new Vector2(new_posn_x, new_posn_y);
    }

    public void AssignName(string parentName)
    {
        string id = parentName.Substring(parentName.Length - 1, 1);
        gameObject.name = "planetLabel" + id;
    }
}