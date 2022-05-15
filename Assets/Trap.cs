using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public float trapTime;
    enum stateTrap
    {
        ready,
        traped,
        caught,
    }
    stateTrap state;

    private Collider2D enemy;
    // Start is called before the first frame update
    void Start()
    {
        state = stateTrap.ready;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == stateTrap.traped)
        {
            StartCoroutine(TrapEnemy(enemy));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Enemy") && state == stateTrap.ready)
        { 
            state = stateTrap.traped;
            enemy = other;
           
        }
    }
    IEnumerator TrapEnemy(Collider2D other)
    {
        state = stateTrap.caught;
        Debug.Log(other.gameObject.tag);
    
        other.transform.parent.GetComponentInChildren<EnemyFollow>().speed = 0;
        yield return new WaitForSeconds(trapTime);
        if(other!=null)
        other.transform.parent.GetComponentInChildren<EnemyFollow>().speed = other.transform.parent.GetComponent<EnemyController>().speed;
        gameObject.active = false;
        
    }
}
