using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ARrow : MonoBehaviour
{
    public Rigidbody2D Arrow;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Cleaner()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);

    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.CompareTag("Enemy"))
        {

            Vector2 direction = col.contacts[0].point - (Vector2)Arrow.transform.position;
            direction.y = 0;
            col.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
            Destroy(gameObject);
            col.gameObject.GetComponent<EnemyController>().EF.GetHit = true;
            col.gameObject.GetComponent<Rigidbody2D>().AddForce(direction.normalized * 50f);

            Arrow.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            Arrow.velocity = transform.up * 0;
            StartCoroutine(Cleaner());

        }
    }
}
 


