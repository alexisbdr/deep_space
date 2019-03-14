using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickIconSpawner : MonoBehaviour
{
    public GameObject autoclickIconPrefab;
    public GameObject prodIconPrefab;
    public GameObject scienceIconPrefab; 

    public List<GameObject> createdIcons = new List<GameObject>();

    public void CreateIcon(GameObject parent)
    {
        if (parent.name == "AutoGrowthButton")
        {
            GameObject icon = (GameObject)Instantiate(autoclickIconPrefab);
            icon.transform.parent = parent.transform;
            createdIcons.Add(icon);
        }

        if (parent.name == "IncProdButton")
        {
            GameObject icon = (GameObject)Instantiate(prodIconPrefab);
            icon.transform.parent = parent.transform;
            createdIcons.Add(icon);
        }

        if (parent.name == "GetScienceButton")
        {
            GameObject icon = (GameObject) Instantiate(scienceIconPrefab);
            icon.transform.parent = parent.transform;
            createdIcons.Add(icon);
        }
    }
}