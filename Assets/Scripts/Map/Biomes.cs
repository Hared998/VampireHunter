using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Biomes : MonoBehaviour
{


    public string name;

    public int LevelRequirment = 1;
    public int LevelMax = 0;

    //Wszystkie tekstury biomu
    public RuleTile Ground;
    public RuleTile Wall;
    public RuleTile Road;
    public RuleTile Void;
    public RuleTile liquidtile;
   
    public List<GameObject> Enemy;
    public List<GameObject> Boss;

    //Typ geneorwania
    [System.Serializable]
    public enum Generate
    {
        Rooms,
        RoadOnly,
        RoomsWithTeleport,
        RandomRooms 
    }
    public Generate TypeGenerate;

    public int Skip;
    public bool FullRoad;
    //Dla Random Rooms szansa na stworzenie pomieszczenia
    public float RoomSpawnChance;

    public bool liquid;
    public float liquidchance;
    //Szerokoœæ drogi
    public float roadSizeMax;
    public float roadSize;
    public float roadChanceExtrude;
    public float roadChanceSpawn;

    //Rozmiary pomieszczeñ
    public int maxRoomx;
    public int maxRoomy;
    public int minRoomx;
    public int minRoomy;

    public int minMapSize;
    public int maxMapSize;

    //Przeciwnicy
    public int maxEnemyChunk;

    //Zape³nienie pomieszczeñ
    public float minFill;
    public float maxFill;

    private bool bossSet;


    public GameObject GetEnemy()
    {
        if (Enemy.Count > 1)
            return Enemy[Random.Range(0, Enemy.Count)];
        else
            return Enemy[0];
    }
    public GameObject GetBoss()
    {
        if (Boss.Count > 1 )
            return Boss[Random.Range(0, Boss.Count)];
        else
            return Boss[0];
    }
    public Room GetRoomLast()
    {
        List<GameObject> FindedRooms = new List<GameObject>();
        GameObject[] Rooms = GameObject.FindGameObjectsWithTag("Room");
        foreach (var i in Rooms)
        {
            Room tmp = i.GetComponent<Room>();
            if (tmp.biome_Name == this.name)
            {
                FindedRooms.Add(i);
            }
        }
        if (FindedRooms.Count > 0)
        {
            return RoomCheckerLast(FindedRooms);
        }
        else
            return null;
    }
    public Room RoomCheckerLast(List<GameObject> FindedRooms)
    {
        Room randr = FindedRooms[Random.Range(0, FindedRooms.Count)].GetComponent<Room>();

            if (randr.name == "End")
            {
                return randr;
            }
            return RoomCheckerLast(FindedRooms);
        
    }
    public Coords getRandomRoomSize()
    {
        Coords tmpCoords = new Coords(Random.Range(minRoomx, maxRoomx), Random.Range(minRoomy, maxRoomy));
        return tmpCoords;
    }
    public Coords getRandomMapSize()
    {
        
        Coords tmpCoords = new Coords(Random.Range(minMapSize, maxMapSize), Random.Range(minMapSize, maxMapSize));
        if (tmpCoords.x == 1 && tmpCoords.y == 1)
            tmpCoords.x++;
        return tmpCoords;
    }
    public Room GetRoom(int chunk, int start)
    {
        List<GameObject> FindedRooms  = new List<GameObject>();
        GameObject[] Rooms = GameObject.FindGameObjectsWithTag("Room");
     
        foreach (var i in Rooms)
        {

            Room tmp = i.GetComponent<Room>();
            if(tmp.biome_Name == this.name)
            {
                FindedRooms.Add(i);
            }
        }
        if (FindedRooms.Count > 0)
        {
            return RoomChecker(FindedRooms, chunk, start);
        }
        else
            return null;
    }
    public Room RoomChecker(List<GameObject> FindedRooms, int chunk, int start)
    {
        Room randr = FindedRooms[Random.Range(0, FindedRooms.Count)].GetComponent<Room>();

        if (randr.TypeGenerate == Room.Generate.BossRoom && bossSet)
        {
            return RoomChecker(FindedRooms, chunk, start);
        }
        else if (randr.TypeGenerate == Room.Generate.BossRoom && !bossSet)
        {
            bossSet = true;
        }
        if (start == chunk)
        {
            if (randr.name == "Start")
            {
                return randr;
            }
            return RoomChecker(FindedRooms, chunk, start);
        }
        if (randr.name == "Start" || randr.name == "End")
            return RoomChecker(FindedRooms, chunk, start);
  
        return randr;
    }

    void Awake()
    {
        bossSet = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
