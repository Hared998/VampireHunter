using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    public Room RoomInfo;
    public GameObject PressFtoOpen;
    public int idchunk;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!RoomInfo.GetChest(idchunk).open)
            {
                PressFtoOpen.SetActive(true);
                if (Input.GetKey(KeyCode.F) && !RoomInfo.GetChest(idchunk).open)
                {
                    collision.gameObject.GetComponent<Character>().AddItemToInventory(RoomInfo.GetChest(idchunk).chestItems.DropItem());
                    RoomInfo.GetChest(idchunk).open = true;
                    PressFtoOpen.SetActive(false);
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PressFtoOpen.SetActive(false);
        }
    }
}
