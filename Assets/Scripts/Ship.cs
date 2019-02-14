using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

    int Source;
    int Destination;
    float progress;
    float srcR;
    float dstR;
    float srcTheta;
    float prevSrcTheta;
    float prevDstTheta;
    float dstTheta;
    float progressPerSecond;
    float R;
    float Theta;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update ()
    {
        srcR = GameObject.Find("planet" + Source.ToString()).GetComponent<Planet>().GetR();
        dstR = GameObject.Find("planet" + Destination.ToString()).GetComponent<Planet>().GetR();
        srcTheta = GameObject.Find("planet" + Source.ToString()).GetComponent<Planet>().GetTheta();
        //Lerp breaks when theta rolls over from 2pi to 0
        //add 2pi once per revolution of planet since ship began
        while (srcTheta < prevSrcTheta)
        {
            srcTheta += 2 * Mathf.PI;
        }
        prevSrcTheta = srcTheta;
        dstTheta = GameObject.Find("planet" + Destination.ToString()).GetComponent<Planet>().GetTheta();
        while (dstTheta < prevDstTheta)
        {
            dstTheta += 2 * Mathf.PI;
        }
        prevDstTheta = dstTheta;
        R = Mathf.Lerp(srcR, dstR, progress);
        //adding 2pi to dsttheta helps force path to be counter-clockwise
        Theta = Mathf.Lerp(srcTheta, dstTheta+2*Mathf.PI, progress);
        Vector2 position = new Vector2(R * Mathf.Cos(Theta), R * Mathf.Sin(Theta));
        transform.position = position;
    }

    private void FixedUpdate()
    {
        progress = progress + progressPerSecond * Time.fixedDeltaTime;
        if (progress > 1)
        {
            Destroy(gameObject);
        }
    }

    public void InitializeShip(int SrcPlanet, int DstPlanet, float timer)
    {
        Source = SrcPlanet;
        Destination = DstPlanet;
        progress = 0f;
        srcR = GameObject.Find("planet" + Source.ToString()).GetComponent<Planet>().GetR();
        dstR = GameObject.Find("planet" + Destination.ToString()).GetComponent<Planet>().GetR();
        srcTheta = GameObject.Find("planet" + Source.ToString()).GetComponent<Planet>().GetTheta();
        dstTheta = GameObject.Find("planet" + Destination.ToString()).GetComponent<Planet>().GetTheta();
        progressPerSecond = 1 / (timer);
    }
}
