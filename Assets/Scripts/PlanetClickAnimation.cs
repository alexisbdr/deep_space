using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls color of animation, destroys game object when animation finished. (Look at AnimatorController component for movement animation.)
public class PlanetClickAnimation : MonoBehaviour {

	//global
	private TextMesh _textMesh;
	private Color _startColor;
	private Color _endColor = Color.black;
	private float _timeElapsed;
	
	private float _animationTime = 1.4f;
	
	// Use this for initialization
	void Start ()
	{
		_textMesh = GetComponent<TextMesh>();
		_startColor = _textMesh.color;
	}
	
	// Update is called once per frame
	void Update ()
	{
		_timeElapsed += Time.deltaTime;
		_textMesh.color = Color.Lerp(_startColor, _endColor, _timeElapsed / _animationTime);
		if (_timeElapsed > _animationTime)
		{
			Destroy(gameObject);
		}
	}
}
