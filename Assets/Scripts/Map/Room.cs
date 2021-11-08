using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class Chest
{
    public float spawnRateMin;
    public float spawnRateMax;
    public Tile Texture;
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

    public Tile SpawnChest()
    {
        Tile tmp = null;
        float ratio = Random.Range(0.0f, 1.0f);
        foreach (var i in this.Chest)
        {
         
            if (i.spawnRateMax > ratio && i.spawnRateMin <= ratio)
            {
                tmp = i.Texture;
                return tmp;
            }
        }
        return tmp;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
