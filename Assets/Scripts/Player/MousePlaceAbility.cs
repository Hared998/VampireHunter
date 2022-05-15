using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Smth To Place", menuName = "Abilitys/PlaceOnMouse")]
public class MousePlaceAbility : Ability
{
    public GameObject ObjectToPlace;

    public override void Activate(GameObject parent)
    {

    }
    public override bool BeginActivate(GameObject parent)
    {
        Debug.Log(this.Name + "Placed");
        Vector3 mousePos = Input.mousePosition;
        Vector3 objectPos = parent.GetComponent<Walk>().cam.ScreenToWorldPoint(mousePos);
        objectPos.z = 0;
        Instantiate(ObjectToPlace, objectPos, Quaternion.identity);
        return true;
    }
    public override void BeginCooldown(GameObject parent)
    {
        
    }
    void Start()
    {
    }
}
