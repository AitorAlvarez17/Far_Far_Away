using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteSwitch : MonoBehaviour
{

    SpriteRenderer spriteRenderer;
    Sprite sprite;
    public List<Sprite> spriteList;
    public int spriteNum;
    public int position = 0;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent <SpriteRenderer>();
        sprite = spriteList[position];
        spriteNum = spriteList.Count - 1;
    }

    void SwitchSpriteInOrder()
    {
        if(position < spriteNum)
        {
            position++;
        }
    }
    // Update is called once per frame
    void Update()
    {
        sprite = spriteList[position];
        spriteRenderer.sprite = sprite;
    }
}
