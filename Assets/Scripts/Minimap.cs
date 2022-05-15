using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    public GameObject Minmap;
    void Start()
    {
      
        Minmap.active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Minmap.active = true;
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
            Minmap.active = false;
    }
}
