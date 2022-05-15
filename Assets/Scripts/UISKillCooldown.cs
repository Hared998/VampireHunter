using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UISKillCooldown : MonoBehaviour
{
    // Start is called before the first frame update

    public AbilityHolder abHolder;
    Image cooldownImage;
    void Start()
    {
        cooldownImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (abHolder != null)
        {
            if (abHolder.GetState() == AbilityHolder.State.ready)
            {
                if (!cooldownImage.color.Equals(Color.white))
                    cooldownImage.color = Color.white;
            }
            else
            {
                if (!cooldownImage.color.Equals(Color.gray))
                    cooldownImage.color = Color.gray;
             
            }
        }
        else
        {
            if (!cooldownImage.color.Equals(Color.gray))
                cooldownImage.color = Color.gray;
         
        }
    }
}
