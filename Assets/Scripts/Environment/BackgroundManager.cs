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

    // Start is called before the first frame update
    void Start() {
        bgSpritesLevel1.AddRange(GetComponentsInChildren<SpriteRenderer>());
        bgSpritesLevel2.AddRange(GetComponentsInChildren<SpriteRenderer>());
        bgSpritesLevel3.AddRange(GetComponentsInChildren<SpriteRenderer>());

       // int listLength = bgSprites.Count;
        startPos = transform.position.x;

        bgBounds = bgSpritesLevel1[0].bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);
        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

        foreach (SpriteRenderer bgSprite in bgSpritesLevel1)
        {
           if (temp > startPos + bgBounds) startPos += bgBounds;
            else if (temp < startPos - bgBounds) startPos -= bgBounds;
        }
    }
}
