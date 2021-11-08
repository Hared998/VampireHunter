using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnyEnemy : MonoBehaviour
{
    public GameObject Enemy;

    public Tilemap map;
    public ParticleSystem blood;
    public GameObject EnemyObject;
    public Vector3 offset;
    // Start is called before the first frame update

    public Vector2 SetEnemy(Chunk chunk, Biomes biome)
    {
        if (chunk.Type != null)
        { 
            
            int k = Random.Range(0, chunk.ListCoords.Count);

            Vector3Int tmp = new Vector3Int(chunk.ListCoords[k].x, chunk.ListCoords[k].y, 0);
     
            if ((map.GetTile(tmp) == biome.Ground || map.GetTile(tmp) == biome.Road) && !chunk.ListCoords[k].IsSpawned)
            {
                
                chunk.ListCoords[k].IsSpawned = true;
                GameObject CopyEnemy = Instantiate(Enemy, tmp + offset, Quaternion.Euler(0, 0, Random.Range(1, 360)));
                CopyEnemy.GetComponent<EnemyController>().blood = blood;

            }
            else
                SetEnemy(chunk, biome);


            }
        return new Vector2(0,0);

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
