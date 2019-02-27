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
    float timer;
    Vector2 sizeMax;
    Vector2 sizeCurrent;


	// Use this for initialization
	void Start () {
        timer = 0;
        sizeMax = transform.localScale ;
        sizeCurrent = new Vector2(0f,0f);
        transform.localScale = sizeCurrent;
    }
	
	// Update is called once per frame
	void Update ()
    {
        srcR = GameObject.Find("planet" + Source.ToString()).GetComponent<Planet>().R;
        dstR = GameObject.Find("planet" + Destination.ToString()).GetComponent<Planet>().R;
        srcTheta = GameObject.Find("planet" + Source.ToString()).GetComponent<Planet>().Theta;
        //Lerp breaks when theta rolls over from 2pi to 0
        //add 2pi once per revolution of planet since ship began
        while (srcTheta < prevSrcTheta)
        {
            srcTheta += 2 * Mathf.PI;
        }
        prevSrcTheta = srcTheta;
        dstTheta = GameObject.Find("planet" + Destination.ToString()).GetComponent<Planet>().Theta;
        while (dstTheta < prevDstTheta)
        {
            dstTheta += 2 * Mathf.PI;
        }
        prevDstTheta = dstTheta;
        R = Mathf.Lerp(srcR, dstR, progress);
        //adding 2pi to dsttheta helps force path to be counter-clockwise
        Theta = Mathf.Lerp(srcTheta, dstTheta, progress);
        Vector2 position = new Vector2(R * Mathf.Cos(Theta), R * Mathf.Sin(Theta));
        transform.position = position;
    }

    private void FixedUpdate()
    {
        if (timer > 0.5f)
        {
            progress = progress + progressPerSecond * Time.fixedDeltaTime;
            if (progress > 1)
            {
                Destroy(gameObject);
            }
        } else {
            sizeCurrent.x = Mathf.Lerp(0, sizeMax.x, timer * 2);
            sizeCurrent.y = Mathf.Lerp(0, sizeMax.y, timer * 2);
            transform.localScale = sizeCurrent;
            timer = timer + Time.fixedDeltaTime;
        }
    }

    public void InitializeShip(int SrcPlanet, int DstPlanet, float timer)
    {
        Source = SrcPlanet;
        Destination = DstPlanet;
        progress = 0f;
        srcR = GameObject.Find("planet" + Source.ToString()).GetComponent<Planet>().R;
        R = srcR;
        dstR = GameObject.Find("planet" + Destination.ToString()).GetComponent<Planet>().R;
        srcTheta = GameObject.Find("planet" + Source.ToString()).GetComponent<Planet>().Theta;
        Theta = srcTheta;
        prevSrcTheta = srcTheta;
        dstTheta = GameObject.Find("planet" + Destination.ToString()).GetComponent<Planet>().Theta;
        prevDstTheta = dstTheta;
        progressPerSecond = 1 / (timer);
    }
}
