using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public LayerMask ToCollide;
    public Transform Enemy;
    public int MaxDistance;
    public Vector3 LastPosition;

    private EnemyFollow EF;

    public bool EnemyActive;

    private Quaternion Ray45degree;
    private Quaternion Ray30degree;
    private Quaternion Ray15degree;

    void Start()
    {
        EF = gameObject.GetComponentInChildren<EnemyFollow>();
        Ray45degree = Quaternion.Euler(0, 0, 45);
        Ray30degree = Quaternion.Euler(0, 0, 30);
        Ray15degree = Quaternion.Euler(0, 0, 15);
        EnemyActive = false;
        MaxDistance = 3;
    }

    // Update is called once per frame  
    void Update()
    {
        if (EnemyActive)
        {
            CheckForCollision();
        }
    }
    private void CheckForCollision()
    {
        Debug.DrawRay(Enemy.position, Enemy.up * MaxDistance, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(Enemy.position, Enemy.transform.parent.up, MaxDistance, ToCollide);
        if (hit != null && hit)
        {
            EF.Enemy.transform.position += new Vector3(2, 2, 0); ;
            Debug.Log(hit);
        }

        //Debug.DrawRay(Enemy.position, Ray30degree * Enemy.up * MaxDistance, Color.green);
        //Debug.DrawRay(Enemy.position, Ray45degree * Enemy.up * MaxDistance, Color.green);
        //Debug.DrawRay(Enemy.position, Ray30degree * -Enemy.right * MaxDistance, Color.green);
        //Debug.DrawRay(Enemy.position, Ray45degree * -Enemy.right * MaxDistance, Color.green);
        //Debug.DrawRay(Enemy.position, Ray15degree * -Enemy.right  * MaxDistance, Color.green);
        //Debug.DrawRay(Enemy.position, Ray15degree * Enemy.up * MaxDistance, Color.green);



    }
}
