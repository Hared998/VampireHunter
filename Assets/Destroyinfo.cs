using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Destroyinfo : MonoBehaviour
{
    public Tilemap MapInfo;
    public Tile ChangeTexture;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetMap(Tilemap map)
    {
        MapInfo = map;
    }
    public void DestroyObject()
    {
        Vector3Int tmp = new Vector3Int((int)this.transform.position.x, (int)this.transform.position.y, 0);
        if (ChangeTexture != null)
            MapInfo.SetTile(tmp, ChangeTexture);
        else
            MapInfo.SetTile(tmp, null);
    }
}
