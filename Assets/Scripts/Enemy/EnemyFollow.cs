using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{

    public EnemyAI AI;  
    public GameObject Enemy;
    public Rigidbody2D rb;
    Vector2 movement;
    private EnemyController ec;
    public float speed;
    private bool PlayerExit = false;
    private bool PlayerTrigger = false;
    private Vector3 LastPosition;

    public float Cooldown = 0.2f;
    public float Wakeup = 0.2f;
    public float Wait = 0.2f;
    public bool GetHit = false;
    public bool IsWall = false;

    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
   
        ec = Enemy.GetComponent<EnemyController>();
        speed = ec.speed;
        rb = Enemy.GetComponent<Rigidbody2D>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AI.EnemyActive = true;
            PlayerExit = false;
            PlayerTrigger = true;
            Vector3 direction = collision.transform.position - Enemy.transform.position + offset;
            direction.Normalize();
            movement = direction;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        { 
            PlayerTrigger = false;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!ec.isAttack && PlayerTrigger && !GetHit)
        {
            rb.MovePosition((Vector2)Enemy.transform.position + movement * speed * Time.deltaTime);
        }
        else if(!ec.isAttack || !GetHit || !IsWall)
        {
            rb.MovePosition((Vector2)Enemy.transform.position);
        }
        
        if (Wakeup < Time.time && GetHit)
        {
            Wakeup = Time.time + Cooldown;
            GetHit = false;
        }
        if (Wait < Time.time && IsWall)
        {
            Wait = Time.time + Cooldown;
            IsWall = false;
        }
        if (!GetHit)
        {
            Wakeup = Time.time + Cooldown;
        }
        if (!IsWall)
        {
            Wait = Time.time + Cooldown;
        }
    }
   
}
