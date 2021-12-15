using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AoE Utility", menuName = "Abilitys/AoE")]
public class AoEAbility : Ability
{
    public GameObject dashParticle;
    public float radiusDamage;
    public float basicDamage;
    public float usingSpeedPlayer = 0;

    private float actualRadius;
    private List<Collider2D> HitColliders;

    public override void Activate(GameObject parent)
    {

        List<Collider2D> colliders = new List<Collider2D>();
        Walk walking = parent.GetComponent<Walk>();

        actualRadius += radiusDamage * Time.deltaTime;
  
        colliders.AddRange(Physics2D.OverlapCircleAll(parent.transform.position, actualRadius));

        if(actualRadius > radiusDamage/2)
            walking.SpeedPlayer = walking.DefaultSpeed;

    
        foreach (var i in colliders)
        {
            if (i.CompareTag("Enemy") && !HitColliders.Contains(i))
            {
                HitColliders.Add(i);
                Debug.Log("Mamy wroga");
                i.GetComponent<EnemyController>().Health -= basicDamage;
                Debug.Log("Zadano " + radiusDamage + "obra¿eñ i zosta³o: " + i.GetComponent<EnemyController>().Health);
            }
        }

    }
    public override bool BeginActivate(GameObject parent)
    {

        Instantiate(dashParticle, parent.transform.position, parent.transform.rotation);
        parent.GetComponent<Walk>().SpeedPlayer = usingSpeedPlayer;

        return true;
    }
    public override void BeginCooldown(GameObject parent)
    {
        actualRadius = 0;
        Walk walking = parent.GetComponent<Walk>();
        
    }
    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(dashParticle.transform.position, actualRadius);
    }
}

