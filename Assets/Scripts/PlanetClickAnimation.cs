using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

// Controls color and y pos movement animation, destroys game object when animation finished.
public class PlanetClickAnimation : MonoBehaviour {

	//global
	private TextMesh _textMesh;
	private Color _startColor;
	private Color _endColor;
	private float _timeElapsed;
	
	private static float _animationColorTime = 1.4f;

    Details detailsObj;
    private Planet parentPlanet; 

    // Use this for initialization
    void Start ()
    {
        parentPlanet = transform.parent.GetComponent<Planet>();

        detailsObj = GameObject.Find("DetailsCanvas").GetComponent<Details>();
        _textMesh = GetComponent<TextMesh>();
        _textMesh.text = "+" + GameUtils.formatLargeNumber(detailsObj.popClick);
		_startColor = _textMesh.color;
		// end color is transparent start color
		_endColor = new Color(_startColor.r, _startColor.g, _startColor.b, 0);
        /**
        float random_posn_x = Random.Range(0.35f, 0.5f);
        transform.localPosition = new Vector3(random_posn_x,0,0);
        **/
        posnStart();
    }
	
	// Update is called once per frame
	void Update ()
	{
		_timeElapsed += Time.deltaTime;
		
		// make y pos movement accelerate 
		var animationMovementScale = _timeElapsed / (_animationColorTime * 20);
		transform.localPosition += new Vector3(0, animationMovementScale, 0);
		
		// lerp color from opaque to transparent
		_textMesh.color = Color.Lerp(_startColor, _endColor, _timeElapsed / _animationColorTime);
		
		// destroy when animation finished
		if (_timeElapsed > _animationColorTime)
		{
			Destroy(gameObject);
		}
	}

    //Update the click animation here
    private void posnStart()
    {
        //Seed random posn_x
        //float posn_x = Random.Range(0.1f, .4f);
        //float posn_y = Random.Range(.3f, .45f);
	    float posn_x = .25f;
	    float posn_y = .2f;
	    
        //What's daddy's scale?
        float parenScale = parentPlanet.transform.localScale.y;

        transform.localPosition = new Vector3(posn_x, posn_y* parenScale, 0);



    }

}
