using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHear : MonoBehaviour
{
    public GameObject Enemy;
    private Rigidbody2D rb;
    private Vector3 velocity = Vector3.one;
    public float smoothspeed;
    // Start is called before the first frame update
    void Start()
    {
        rb = Enemy .GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)

    {
        if (collision.CompareTag("Player"))
        {

            if (collision.GetComponent<Walk>().IsWalking && !Enemy.GetComponent<EnemyController>().isAttack)
            {
                Vector3 direction = collision.transform.position - Enemy.transform.position;

                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

                rb.rotation = angle;
            }

        }
    }
}
