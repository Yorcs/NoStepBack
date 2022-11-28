using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float bgBounds, startPos;
    public GameObject cam;
    public float parallaxEffect;
    private SpriteRenderer[] bgSprites;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        bgSprites = GetComponentsInChildren<SpriteRenderer>();
        bgBounds = bgSprites[0].bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);
        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

        foreach (SpriteRenderer sprite in bgSprites)
        {
            if (temp > startPos + bgBounds) startPos += bgBounds;
            else if (temp < startPos - bgBounds) startPos -= bgBounds;
        }
    }
}
