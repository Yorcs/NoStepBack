using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float bgBounds;
    public float parallaxEffect;
    public GameObject cam;

    // Start is called before the first frame update
    void Start()
    {
        bgBounds = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    public void SetParallax()
    {

    }
}
