using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    private float bgBounds, startPos;
    public GameObject cam;
    [SerializeField] private float parallaxEffect;
    [SerializeField] private List<SpriteRenderer> bgSpritesLevel1 = new();
    [SerializeField] private List<SpriteRenderer> bgSpritesLevel2 = new();
    [SerializeField] private List<SpriteRenderer> bgSpritesLevel3 = new();
    [SerializeField] private List<SpriteRenderer> bgSprites;

    // Start is called before the first frame update
    void Start() {
        bgSprites.AddRange(GetComponentsInChildren<SpriteRenderer>());
        int listLength = bgSprites.Count;
        startPos = transform.position.x;

        // for (int i = 0; i < listLength; i++) {
        //     if (i == 0) bgSpritesLevel1.Add(bgSprites[i]);
        //     if (i == 1) bgSpritesLevel2.Add(bgSprites[i]);
        //     if (i == 2) bgSpritesLevel3.Add(bgSprites[i]);
        // } 
        bgBounds = bgSpritesLevel1[0].bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);
        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

        foreach (SpriteRenderer sprite in bgSpritesLevel1)
        {
           if (temp > startPos + bgBounds) startPos += bgBounds;
            else if (temp < startPos - bgBounds) startPos -= bgBounds;
        }
    }
}
