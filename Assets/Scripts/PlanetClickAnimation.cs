using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls color and y pos movement animation, destroys game object when animation finished.
public class PlanetClickAnimation : MonoBehaviour {

	//global
	private TextMesh _textMesh;
	private Color _startColor;
	private Color _endColor;
	private float _timeElapsed;
	
	private static float _animationColorTime = 1.4f;
	
	
	// Use this for initialization
	void Start ()
	{
		_textMesh = GetComponent<TextMesh>();
		_textMesh.text = "+" + planetData.popClick;
		_startColor = _textMesh.color;
		// end color is transparent start color
		_endColor = new Color(_startColor.r, _startColor.g, _startColor.b, 0);
		transform.localPosition = new Vector3(0.5f,0,0);
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
}
