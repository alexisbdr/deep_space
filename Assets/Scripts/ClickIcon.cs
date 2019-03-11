using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickIcon : MonoBehaviour
{
    int Source;
    int Destination;
    float progress;
    float progressPerSecond;
    float timer;

    /**
    float srcR;
    float dstR;
    float srcTheta;
    float prevSrcTheta;
    float prevDstTheta;
    float dstTheta;
    float R;
    float Theta;
    **/

    Vector2 sizeMax;
    Vector2 sizeCurrent;

    private Vector2 source;
    private Vector2 destination;

    private RectTransform prod;
    // Use this for initialization
    void Start()
    {
        timer = 15f;
        progress = 0f;
        progressPerSecond = 1 / (timer);

        sizeMax = transform.localScale;
        sizeCurrent = new Vector2(0f, 0f);
        transform.localScale = sizeCurrent;

        source = transform.parent.position;
        prod = GameObject.Find("ProductivityValue").GetComponent<RectTransform>();
        destination = new Vector2(prod.localPosition.x, prod.localPosition.y);
        Debug.Log(source);
    }

    // Update is called once per frame
    void Update()
    {

        /**
        R = Mathf.Lerp(srcR, dstR, progress);
        Theta = Mathf.Lerp(srcTheta, dstTheta, progress);
        **/
        transform.localPosition = Vector2.Lerp(source, destination, progress);
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
        }
        else
        {
            sizeCurrent.x = Mathf.Lerp(0, sizeMax.x, timer * 2);
            sizeCurrent.y = Mathf.Lerp(0, sizeMax.y, timer * 2);
            transform.localScale = sizeCurrent;
            timer = timer + Time.fixedDeltaTime;
        }
    }
}
