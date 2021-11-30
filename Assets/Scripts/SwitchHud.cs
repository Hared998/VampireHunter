using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchHud : MonoBehaviour
{
    public Canvas hud1;
    public Canvas hud2;
    // Start is called before the first frame update
    public void sw()
    {
        hud1.enabled = false;
        hud2.enabled = true;
    }
    public void Start()
    {
   
        hud2.enabled = false;
    }
}

    // Update is called once per frame

