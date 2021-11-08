using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Biomes : MonoBehaviour
{
    public string name;
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
    public int minRoomy;

    //Przeciwnicy
    public int maxEnemyChunk;

    //Zape³nienie pomieszczeñ
    public float minFill;
    public float maxFill;

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

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
