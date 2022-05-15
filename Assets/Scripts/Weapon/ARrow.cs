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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Arrow.velocity = transform.up * 0;
        StartCoroutine(Cleaner());
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("ItemDestroy"))
        {
            Destroy(col.gameObject);
            col.gameObject.GetComponent<Destroyinfo>().DestroyObject();
            Destroy(gameObject);
        }
        else if (col.gameObject.CompareTag("Enemy"))
        {

            col.gameObject.GetComponentInParent<EnemyController>().TakeDamage(damage);
            col.gameObject.GetComponentInParent<EnemyController>().EF.GetHit = true;
            Destroy(gameObject);
            

            Arrow.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            Arrow.velocity = transform.up * 0;
            StartCoroutine(Cleaner());

        }

    }
}
 


