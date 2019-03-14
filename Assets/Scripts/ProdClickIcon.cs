using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProdClickIcon : MonoBehaviour
{
    int Source;
    int Destination;
    float progress;
    float progressPerSecond;
    float timer;

    Vector2 sizeMax;
    Vector2 sizeCurrent;

    private Vector2 source;
    private Vector2 destination;

    void Start()
    {
        timer = 1.8f;
        progress = 0f;
        progressPerSecond = 1 / (timer);

        sizeCurrent = new Vector2(0f, 0f);
        sizeMax = new Vector3(.4f,.4f,1f);

        source = transform.parent.position;
        Vector3 prod = GameObject.Find("ProductivityValue").GetComponent<RectTransform>().position;
        //Vector3 prod = transform.TransformPoint(GameObject.Find("ProductivityValue").GetComponent<RectTransform>().position);
        destination = new Vector2(prod.x-.9f, prod.y+.18f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.Lerp(source, destination, progress);
        transform.Rotate(0, 0, 10);
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