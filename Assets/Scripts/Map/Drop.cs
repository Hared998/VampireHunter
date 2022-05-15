using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewDrop", menuName = "Equipment/Drop")]
public class Drop : ScriptableObject
{ 
    [SerializeField] List<ItemV2> common;
    [SerializeField] List<ItemV2> uncommon;
    [SerializeField] List<ItemV2> rare;
    // Start is called before the first frame update
    public ItemV2 DropItem()
    {
        List<ItemV2> list = RariryLevel();
        Debug.Log(this.name);
        return list[Random.Range(0, list.Count)];
    }
    private List<ItemV2> RariryLevel()
    {
        int rand = Random.RandomRange(0, 100);
        if (rand > 40)
        {
            Debug.Log("common");
            return common;
        }
        else if(rand > 10 && rand <= 40)
        {
            Debug.Log("uncommon");
            return uncommon;
        }
        else if(rand < 10)
        {
            Debug.Log("rare");
            return rare;
        }
        return common;
    }
}
