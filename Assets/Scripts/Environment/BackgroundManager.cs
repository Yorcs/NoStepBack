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
    private int backgroundState;

    // Start is called before the first frame update
    void Start() {
        bgSpritesLevel1.AddRange(GetComponents<SpriteRenderer>());
        bgSpritesLevel2.AddRange(GetComponents<SpriteRenderer>());
        bgSpritesLevel3.AddRange(GetComponents<SpriteRenderer>());

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

        if (backgroundState == 0)
        {
            foreach (SpriteRenderer bgSprite in bgSpritesLevel1)
            {
                if (temp > startPos + bgBounds) startPos += bgBounds;
                else if (temp < startPos - bgBounds) startPos -= bgBounds;
            }
        }
        else if (backgroundState == 1)
        {
            foreach (SpriteRenderer bgSprite in bgSpritesLevel2)
            {
                if (temp > startPos + bgBounds) startPos += bgBounds;
                else if (temp < startPos - bgBounds) startPos -= bgBounds;
            }
        }

        else
        {
            foreach (SpriteRenderer bgSprite in bgSpritesLevel3)
            {
                if (temp > startPos + bgBounds) startPos += bgBounds;
                else if (temp < startPos - bgBounds) startPos -= bgBounds;
            }
        }
    }

    public void changeBackground(int backgroundState)
    {
        switch (backgroundState)
        {
            case 0:
                foreach (SpriteRenderer renderer in bgSpritesLevel1)
                {
                    renderer.gameObject.SetActive(true);
                }
                 foreach(SpriteRenderer renderer in bgSpritesLevel2)
                {
                    renderer.gameObject.SetActive(false);
                }
                foreach (SpriteRenderer renderer in bgSpritesLevel3)
                {
                    renderer.gameObject.SetActive(false);
                }
                break;

            case 1:
                foreach (SpriteRenderer renderer in bgSpritesLevel1)
                {
                    renderer.gameObject.SetActive(false);
                }
                foreach (SpriteRenderer renderer in bgSpritesLevel2)
                {
                    renderer.gameObject.SetActive(true);
                }
                foreach (SpriteRenderer renderer in bgSpritesLevel3)
                {
                    renderer.gameObject.SetActive(false);
                }
                break;

            case 2:
                foreach (SpriteRenderer renderer in bgSpritesLevel1)
                {
                    renderer.gameObject.SetActive(false);
                }
                foreach (SpriteRenderer renderer in bgSpritesLevel2)
                {
                    renderer.gameObject.SetActive(false);
                }
                foreach (SpriteRenderer renderer in bgSpritesLevel3)
                {
                    renderer.gameObject.SetActive(true);
                }
                break;

            
        }
    }
}
