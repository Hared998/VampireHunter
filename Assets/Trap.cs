using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public float trapTime;
    public bool traped = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
     
        if (other.CompareTag("Enemy") && !traped)
        {
            Debug.Log("Enemy");
            StartCoroutine(TrapEnemy(other));
        }
    }
    IEnumerator TrapEnemy(Collider2D other)
    {
        other.GetComponentInChildren<EnemyFollow>().speed = 0;
        traped = true;
        yield return new WaitForSeconds(trapTime);
        other.GetComponentInChildren<EnemyFollow>().speed = other.GetComponent<EnemyController>().speed;
        gameObject.active = false;
        
    }
}
