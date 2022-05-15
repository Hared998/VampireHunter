using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnyEnemy : MonoBehaviour
{
   
    public GameObject Parent;
    public Tilemap map;
    public ParticleSystem blood;
    public GameObject EnemyObject;
    public Vector3 offset;

    // Start is called before the first frame update

    public Vector2 SetEnemy(Chunk chunk, Biomes biome, ProgressMission mission)
    {
        if (chunk.Type != null)
        { 
            
            int k = Random.Range(0, chunk.ListCoords.Count);

            Vector3Int tmp = new Vector3Int(chunk.ListCoords[k].x, chunk.ListCoords[k].y, 0);
     
            if ((map.GetTile(tmp) == biome.Ground || map.GetTile(tmp) == biome.Road) && !chunk.ListCoords[k].IsSpawned)
            {
                
                chunk.ListCoords[k].IsSpawned = true;
                GameObject CopyEnemy = Instantiate(biome.GetEnemy(), tmp + offset, Quaternion.identity);
                CopyEnemy.GetComponent<EnemyController>().updatemission = mission;
                mission.addEnemy();
                CopyEnemy.GetComponent<EnemyController>().blood = blood;
                CopyEnemy.transform.parent = Parent.transform;

            }
            else
                SetEnemy(chunk, biome,mission);


            }
        return new Vector2(0,0);

    }
    public Vector2 SetBoss(Chunk chunk, Biomes biome, ProgressMission mission)
    {
        if (chunk.Type != null && biome.Boss.Count > 0)
        {

            int k = Random.Range(0, chunk.ListCoords.Count);

            Vector3Int tmp = new Vector3Int(chunk.ListCoords[k].x, chunk.ListCoords[k].y, 0);

            if ((map.GetTile(tmp) == biome.Ground || map.GetTile(tmp) == biome.Road) && !chunk.ListCoords[k].IsSpawned)
            {

                chunk.ListCoords[k].IsSpawned = true;
                GameObject CopyEnemy = Instantiate(biome.GetBoss(), tmp + offset, Quaternion.identity);
                CopyEnemy.GetComponent<EnemyController>().updatemission = mission;
                mission.addEnemy();
                CopyEnemy.GetComponent<EnemyController>().blood = blood;
                CopyEnemy.transform.parent = Parent.transform;

            }
            else
                SetBoss(chunk, biome, mission);


        }
        return new Vector2(0, 0);

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
