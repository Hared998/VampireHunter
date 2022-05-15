using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{

    [SerializeField] List<ItemV2> itemList;
    // Start is called before the first frame update

    public List<ItemV2> GetItemList()
    {
        return itemList;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
