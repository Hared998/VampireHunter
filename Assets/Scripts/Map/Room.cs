using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class Chest
{
    public string namechest;
    public float spawnRateMin;
    public int idChunk;
    public float spawnRateMax;
    public Drop chestItems;
    public Tile Texture;
    public bool open;
}
public class Room : MonoBehaviour
{
    public string name;
    public string biome_Name;
    public enum Generate
    {
        Start,
        End,
        Single,
        Normal,
        Phillars,
        BossRoom,
        Random
    }
    public Generate TypeGenerate;

    public Tile MainDecorate;
    public int MaxDecorate;

    public List<Chest> Chest;
    public bool ChestSpawned;

    public Tile Trap;
    public int trapLevel;

    public Tile Trash;
    public int MaxTrash;
    public float trashSpawnChance;
    List<Chest> spawnedChest = new List<Chest>();



    public Chest GetChest(int idchunk)
    {
        foreach(var i in spawnedChest)
        {
            if (i.idChunk == idchunk)
            {
                return i;
            }
        }
        return null;
    }
    public Tile SpawnChest(int idchunk)
    {
        Tile tmp = null;
        float ratio = Random.Range(0.0f, 1.0f);
        foreach (var i in this.Chest)
        {
            if (i.spawnRateMax > ratio && i.spawnRateMin <= ratio)
            {
                tmp = i.Texture;
                Chest tmpchest = i;
                tmpchest.idChunk = idchunk;
                spawnedChest.Add(tmpchest);
                return tmp;
            }
        }
        return tmp;

    }
    // Start is called before the first frame update
    void Awake()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
