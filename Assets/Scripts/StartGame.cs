using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Button>().onClick.AddListener(OnClickListener);
    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnClickListener()
    {
        GameObject.Find("MenuCanvas").SetActive(false);
    }
}
