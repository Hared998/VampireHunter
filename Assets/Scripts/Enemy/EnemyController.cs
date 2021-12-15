using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float damage;
    public Vector3 offset;

    PlayerStats dealdamage;

    public float NextAttack;
    public float TimeAttack;
    public float NextTimeAttack;
    public float Cooldown;

    public float speed;
    public float Health;
    public float Armor = 10;

    public int exp = 50;

    private Collider2D PlayerCollider;
    public EnemyFollow EF;
    public Transform LasetStart;
    public bool isAttack = false;
    public bool Zaatakowano = false;

    public ParticleSystem blood;
    void Start()
    {

        PlayerCollider = null;
        NextAttack = Cooldown +Time.deltaTime;
        NextTimeAttack = TimeAttack + Time.deltaTime;
    }

    public void AttacPlayer()
    {
        
        RaycastHit2D hit = Physics2D.Raycast(LasetStart.position, transform.up, 0.75f);
        Debug.DrawRay(LasetStart.position, transform.up * 0.75f, Color.red);
        
        if (hit != null && hit.collider != null && hit.collider.tag == "Player")
        {
            PlayerCollider = hit.collider;
            isAttack = true;
        }
        else
            PlayerCollider = null;
       
        
    }
    public void TakeDamage(float Damage)
    {
        float ReduceDamage = (100 - Armor) / 100;
        float RealDamage = Damage - ReduceDamage;
        RealDamage = Mathf.Round(RealDamage);
        Health -= RealDamage;


    }
    public void GiveDamage(Collider2D player)
    {
        
        if (player.CompareTag("Player") )
        {
            if (isAttack == true && !Zaatakowano)
            {
                Zaatakowano = true;
                dealdamage = player.GetComponent<PlayerStats>();
                dealdamage.TakeDamage(damage);

            }
        }
        
        


    }
    public void Update()
    {
        AttacPlayer();
        if (PlayerCollider != null)
        {
            GiveDamage(PlayerCollider);
        }
        if (NextAttack < Time.time && Zaatakowano)
        {
            NextAttack = Time.time + Cooldown;
            Zaatakowano = false;
            
        }
        if (!Zaatakowano)
        {
            NextAttack = Time.time + Cooldown;
            isAttack = false;;
        }
        
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        

        if (Health <= 0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().UpdateLevel(exp);
            Destroy(gameObject);
            ParticleSystem CreateBlood = Instantiate(blood, transform.position, transform.rotation);
            CreateBlood.Play();
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
            EF.IsWall = true;
    }
        
    
}
