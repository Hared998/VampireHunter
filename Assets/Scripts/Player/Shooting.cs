using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float Cooldown;
    public float NextAttack;
    public bool reload = false;

    public GameObject Arrow;
    public Transform SpawnObject;

    

    void Start()
    {
        reload = false;
        
    }
    public void AttackDisctane()
    {
        GameObject CloneArrow = Instantiate(Arrow, SpawnObject.position, SpawnObject.rotation);

        CloneArrow.GetComponent<Rigidbody2D>().velocity = transform.up * 10;
       

    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0) && !reload)
        {
            AttackDisctane();
            reload = true;
        }
       
        if (NextAttack < Time.time && reload)
        {
            NextAttack = Time.time + Cooldown;
            reload = false;
        }
        if (!reload)
        {
            NextAttack = Time.time + Cooldown;
        }


    }
}
