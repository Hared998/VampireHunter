using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dontRotate : MonoBehaviour
{
    public Transform childObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Quaternion savedRotation = childObject.rotation;
        // Put parent rotation code here...
        childObject.rotation = savedRotation;
    }
}
