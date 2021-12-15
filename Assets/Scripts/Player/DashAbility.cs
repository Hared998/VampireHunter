using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dash", menuName = "Abilitys/Dash")]
public class DashAbility : Ability
{
    public float dashSpeed;
    public float dashSafety;
    public GameObject dashParticle;
    public GameObject dashBoxParticle;
    public override bool BeginActivate(GameObject parent)
    {
        Walk walking = parent.GetComponent<Walk>();
        if (walking.IsWalking)
        {
            Instantiate(dashParticle, walking.Player.position, walking.transform.rotation);
            Debug.Log("Odpalam!");
            return true;
        }
        else
        {
            return false;
        }
        
    }
    public override void Activate(GameObject parent)
    {
        Walk walking = parent.GetComponent<Walk>();

            Debug.DrawRay(walking.DashDetector.position, walking.movement * dashSafety);
            RaycastHit2D hit = Physics2D.Raycast(walking.DashDetector.position, walking.movement, dashSafety);
            if (hit != null && hit.collider != null && hit.collider.CompareTag("Wall"))
            {
                walking.SpeedPlayer = walking.DefaultSpeed;
            }
            else
            {
                walking.SpeedPlayer = dashSpeed;
            }
        
    }
    public override void BeginCooldown(GameObject parent)
    {
        Walk walking = parent.GetComponent<Walk>();
        Destroy(dashBoxParticle);
        walking.SpeedPlayer = walking.DefaultSpeed;
    }
        // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
