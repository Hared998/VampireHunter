using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class DiscoverMap : MonoBehaviour
{
    public bool active;
    // Start is called before the first frame update
    void Start()
    {
        active = false;
        StartCoroutine(StartActive());
    }

    // Update is called once per frame
    void Update()
    {

    }
    public IEnumerator StartActive()
    {
        yield return new WaitForSeconds(0.5f);
        active = true;
        StopCoroutine(StartActive());
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (active)
        {
            if (collision.CompareTag("MAP"))
            {
                Destroy(collision.gameObject);
            }
        }
    }
}

