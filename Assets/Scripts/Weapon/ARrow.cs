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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))

        { 
            

            Vector3 direction = collision.transform.position - Arrow.transform.position;
            direction.y = 0;
            collision.GetComponent<EnemyController>().TakeDamage(damage);
            Destroy(gameObject);
            collision.GetComponent<EnemyController>().EF.GetHit = true;
            collision.GetComponent<Rigidbody2D>().AddForce(direction.normalized * 50f);
            




        }
        if (collision.tag != "Untagged") 
        {
            Arrow.velocity = transform.up * 0;
            StartCoroutine(Cleaner());
        }
    }
}
