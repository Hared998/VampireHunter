using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    public SpriteRenderer spriteRender;
    public Sprite Animation1;
    public Sprite Animation2;
    public Shooting sht;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (sht.reload == true)
        {
            
            spriteRender.sprite = Animation1;
        }
        else
        {
            spriteRender.sprite = Animation2;
        }
    }
}
