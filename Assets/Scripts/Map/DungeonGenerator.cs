using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class DungeonGenerator : MonoBehaviour
{

    public GameObject ParentCol;
    // Start is called before the first frame update
    public DungeonRoom RG;
    public Chunk chunk;
    public Map DungeonMap;
    public Tilemap botMap;
    public Tilemap backgroundMap;
    public Tilemap decorateMap;

    public Text Biomename;

    public int offset;
    public int roadoffset;

    public List<DungeonRoom> Rooms;

    public Coords ChunkSize;
    private Coords MapSize;
    public List<Coords> ListWall;
    public List<Chunk> ListChunk;
    public List<Road> ListConnection;
    public int start;
    public bool chestSpawned;
    public Camera cam;
    public Collider2D collider;
    // Start is called before the first frame update

    void Start()
    {
        cam.GetComponent<CameManager>().enabled = false;

    }
    public void SetPosition(float x, float y, float size)
    {
        cam.orthographicSize = (size / 2) + 6;

        cam.transform.position = new Vector3(x / 2, (y / 2) - 3, -2);
    }
    public int ExtrudeChance(float chance, int k)
    {

        float addpath = Random.Range(0.0f, 1.0f);
        if (addpath < chance)
        {
            k++;
            k = ExtrudeChance(chance/2, k);
        }
        return k;
    }
    public void PrintRoad(Biomes biome, int direction, Coords basepoint)

    {
        Vector3Int tmp = new Vector3Int();
        Vector3Int tmp2 = new Vector3Int();
        Vector3Int center = new Vector3Int();
        int k = 0;
        
        k = ExtrudeChance(biome.roadChanceExtrude, k);
        
        for (int i = 0; i <= biome.roadSize + k; i++)
        {
            float random = Random.Range(0.0f, 1.0f);
            float random2 = Random.Range(0.0f, 1.0f);
            if (i > 0)
            {
                if (direction == 2 || direction == 1)
                {
                    
                    if (i == biome.roadSize + k && k > 0)
                    {
                        if (random < biome.roadChanceSpawn)
                            tmp = new Vector3Int(basepoint.x, basepoint.y + i, 0);
                        if (random2 < biome.roadChanceSpawn)
                            tmp2 = new Vector3Int(basepoint.x, basepoint.y - i, 0);
                    }
                    else
                    {
                        tmp = new Vector3Int(basepoint.x, basepoint.y + i, 0);
                        tmp2 = new Vector3Int(basepoint.x, basepoint.y - i, 0);
                    }
                }
                if (direction == 3 || direction == 4)
                {
                    if (i == biome.roadSize + k && k > 0)
                    {
                        if (random2 < biome.roadChanceSpawn)
                            tmp = new Vector3Int(basepoint.x - 1, basepoint.y, 0);
                        if (random < biome.roadChanceSpawn)
                            tmp2 = new Vector3Int(basepoint.x + 1, basepoint.y, 0);
                    }
                    else
                    {
                        tmp = new Vector3Int(basepoint.x - i, basepoint.y, 0);
                        tmp2 = new Vector3Int(basepoint.x + i, basepoint.y, 0);
                    }

                }
            }
           

            center = new Vector3Int(basepoint.x, basepoint.y, 0);
            
                botMap.SetTile(center, null);
                botMap.SetTile(center, biome.Road);



                if (tmp != null)
                {
                    botMap.SetTile(tmp, null);

                    botMap.SetTile(tmp, biome.Road);
                }
                if (tmp2 != null)
                {
                    botMap.SetTile(tmp2, null);

                    botMap.SetTile(tmp2, biome.Road);
                }

            
        }

    }
    public void PrintRoad(Biomes biome, int direction, Coords basepoint, RuleTile ground)

    {
        Vector3Int tmp = new Vector3Int();
        Vector3Int tmp2 = new Vector3Int();
        Vector3Int center = new Vector3Int();
        int k = 0;

        k = ExtrudeChance(biome.roadChanceExtrude, k);

        for (int i = 0; i <= biome.roadSize + k; i++)
        {
            float random = Random.Range(0.0f, 1.0f);
            float random2 = Random.Range(0.0f, 1.0f);
            if (i > 0)
            {
                if (direction == 2 || direction == 1)
                {

                    if (i == biome.roadSize + k && k > 0)
                    {
                        if (random < biome.roadChanceSpawn)
                            tmp = new Vector3Int(basepoint.x, basepoint.y + i, 0);
                        if (random2 < biome.roadChanceSpawn)
                            tmp2 = new Vector3Int(basepoint.x, basepoint.y - i, 0);
                    }
                    else
                    {
                        tmp = new Vector3Int(basepoint.x, basepoint.y + i, 0);
                        tmp2 = new Vector3Int(basepoint.x, basepoint.y - i, 0);
                    }
                }
                if (direction == 3 || direction == 4)
                {
                    if (i == biome.roadSize + k && k > 0)
                    {
                        if (random2 < biome.roadChanceSpawn)
                            tmp = new Vector3Int(basepoint.x - 1, basepoint.y, 0);
                        if (random < biome.roadChanceSpawn)
                            tmp2 = new Vector3Int(basepoint.x + 1, basepoint.y, 0);
                    }
                    else
                    {
                        tmp = new Vector3Int(basepoint.x - i, basepoint.y, 0);
                        tmp2 = new Vector3Int(basepoint.x + i, basepoint.y, 0);
                    }

                }
            }


            center = new Vector3Int(basepoint.x, basepoint.y, 0);
            if (botMap.GetTile(center) != biome.Road)
            {
                botMap.SetTile(center, null);
                botMap.SetTile(center, ground);


                if (tmp != null)
                {
                    botMap.SetTile(tmp, null);

                    botMap.SetTile(tmp, ground);
                }
                if (tmp2 != null)
                {
                    botMap.SetTile(tmp2, null);

                    botMap.SetTile(tmp2, ground);
                }
            }

        }

    }
    //public void PrintRoadOutside(Biomes biome, int direction, Coords basepoint)

    //{
    //    Vector3Int tmp = new Vector3Int();
    //    Vector3Int tmp2 = new Vector3Int();
    //    Vector3Int center = new Vector3Int();
    //    int k = 0;

    //    k = ExtrudeChance(biome.roadChanceExtrude, k);

    //    for (int i = 0; i <= biome.roadSizeMax + k; i++)
    //    {
    //        float random = Random.Range(0.0f, 1.0f);
    //        float random2 = Random.Range(0.0f, 1.0f);
    //        if (i > 0)
    //        {
    //            if (direction == 2 || direction == 1)
    //            {

    //                if (i == biome.roadSizeMax + k && k > 0)
    //                {
    //                    if (random < biome.roadChanceSpawn)
    //                        tmp = new Vector3Int(basepoint.x, basepoint.y + i, 0);
    //                    if (random2 < biome.roadChanceSpawn)
    //                        tmp2 = new Vector3Int(basepoint.x, basepoint.y - i, 0);
    //                }
    //                else
    //                {
    //                    tmp = new Vector3Int(basepoint.x, basepoint.y + i, 0);
    //                    tmp2 = new Vector3Int(basepoint.x, basepoint.y - i, 0);
    //                }
    //            }
    //            if (direction == 3 || direction == 4)
    //            {
    //                if (i == biome.roadSizeMax + k && k > 0)
    //                {
    //                    if (random2 < biome.roadChanceSpawn)
    //                        tmp = new Vector3Int(basepoint.x - 1, basepoint.y, 0);
    //                    if (random < biome.roadChanceSpawn)
    //                        tmp2 = new Vector3Int(basepoint.x + 1, basepoint.y, 0);
    //                }
    //                else
    //                {
    //                    tmp = new Vector3Int(basepoint.x - i, basepoint.y, 0);
    //                    tmp2 = new Vector3Int(basepoint.x + i, basepoint.y, 0);
    //                }

    //            }
    //        }
    //        center = new Vector3Int(basepoint.x, basepoint.y, 0);

    //        botMap.SetTile(center, null);
    //        botMap.SetTile(center, biome.Road);


    //        if (tmp != null)
    //        {
    //            botMap.SetTile(tmp, null);

    //            botMap.SetTile(tmp, biome.Road);
    //        }
    //        if (tmp2 != null)
    //        {
    //            botMap.SetTile(tmp2, null);

    //            botMap.SetTile(tmp2, biome.Road);
    //        }


    //    }

    //}
    public Map RoadGenerate(Biomes biome, int start)
    {
        DungeonMap.CreateMap();
        ListChunk = new List<Chunk>();
        DungeonMap.GenerateRoads();
        DungeonMap.FillListOfRoads(DungeonMap.Chunks);
      
        for (int i = 0; i < DungeonMap.Chunks.Count; i++)
        {

            DungeonMap.Chunks[i].Type = biome.GetRoom(i, start);
            DungeonMap.Chunks[i].IsUesed = true;
            ListChunk.Add(DungeonMap.Chunks[i]);
        }
        
        DungeonMap.Connections = DungeonMap.Prim(DungeonMap.Chunks[start], DungeonMap.Chunks);
        int max = 0;
        int id = 0;
        foreach (var i in DungeonMap.Connections)
        {
            i.Parentb.cost = i.Parenta.cost + 1;
            if (i.Parentb.cost > max)
            {
                max = (int)i.Parentb.cost;
                id = i.Parentb.ID;
            }
        }
  
        foreach (var i in DungeonMap.Connections)
        {
            if (i.Parentb.ID == id)
            {
                i.Parentb.Type = biome.GetRoomLast();
            }
            Coords tmp1 = DungeonMap.GetRandomCoords(i.Parenta, biome);
            Vector3Int tmp_l1 = new Vector3Int(tmp1.x, tmp1.y, 0);
            Coords tmp2 = DungeonMap.GetRandomCoords(i.Parentb, biome);
            Vector3Int tmp_l2 = new Vector3Int(tmp2.x, tmp2.y, 0);

            if (i.nextdirection == 1)
            {

                for (int k = tmp_l1.x; k >= tmp_l2.x; k--)
                {
                    Coords newCoords = new Coords();
                    if (tmp_l2.y > tmp_l1.y)
                    {
                        tmp_l1.y++;
                    }
                    else if (tmp_l2.y < tmp_l1.y)
                    {
                        tmp_l1.y--;
                    }
                    newCoords = new Coords(k, tmp_l1.y);
                    PrintRoad(biome, i.nextdirection, newCoords);

                }
            }
            if (i.nextdirection == 2)
            {
                for (int k = tmp_l1.x; k <= tmp_l2.x; k++)
                {
                    Coords newCoords = new Coords();
                    if (tmp_l2.y > tmp_l1.y)
                    {
                        tmp_l1.y++;
                    }
                    else if (tmp_l2.y < tmp_l1.y)
                    {
                        tmp_l1.y--;
                    }
                    newCoords = new Coords(k, tmp_l1.y);
                    PrintRoad(biome, i.nextdirection, newCoords);
                }
            }
            if (i.nextdirection == 3)
            {
                for (int k = tmp_l1.y; k <= tmp_l2.y; k++)
                {
                    Coords newCoords = new Coords();

                    if (tmp_l2.x > tmp_l1.x)
                    {
                        tmp_l1.x++;
                    }
                    else if (tmp_l2.x < tmp_l1.x)
                    {
                        tmp_l1.x--;
                    }
                    newCoords = new Coords(tmp_l1.x, k);
                    PrintRoad(biome, i.nextdirection, newCoords);

                }
            }
            if (i.nextdirection == 4)
            {
                for (int k = tmp_l1.y; k >= tmp_l2.y; k--)
                {
                    Coords newCoords = new Coords();
                    if (tmp_l2.x > tmp_l1.x)
                    {
                        tmp_l1.x++;
                    }
                    else if (tmp_l2.x < tmp_l1.x)
                    {
                        tmp_l1.x--;
                    }
                    newCoords = new Coords(tmp_l1.x, k);
                    PrintRoad(biome, i.nextdirection, newCoords);
                }
            }
            

        }
        return DungeonMap;
    }


    public Map RoomGenerate(Biomes biome, int start)
    {
        DungeonMap.CreateMap();
        ListChunk = new List<Chunk>();
        DungeonMap.GenerateRoads();
        DungeonMap.FillListOfRoads(DungeonMap.Chunks);

        for (int i = 0; i < DungeonMap.Chunks.Count; i++)
        {
            DungeonMap.Chunks[i] = RG.GenerateRoom(DungeonMap.Chunks[i], start,biome);

            ListChunk.Add(DungeonMap.Chunks[i]);

        }
        
        
        
        DungeonMap.Connections = DungeonMap.Prim(DungeonMap.Chunks[start], DungeonMap.Chunks);
        int max = 0;
        int id = 0;

        foreach (var i in DungeonMap.Connections)
        {
            i.Parentb.cost = i.Parenta.cost + 1;
            if (i.Parentb.cost > max)
            {
                max = (int)i.Parentb.cost;
                id = i.Parentb.ID;
            }

        }
        foreach (var i in DungeonMap.Connections)
        {
            if (i.Parentb.ID == id)
            {
                i.Parentb.Type = biome.GetRoomLast();
            }

            Coords tmp1 = DungeonMap.GetCenterOfChunk(i.Parenta, biome.Ground, botMap, biome);
            Vector3Int tmp_l1 = new Vector3Int(tmp1.x, tmp1.y, 0);
            Coords tmp2 = DungeonMap.GetCenterOfChunk(i.Parentb, biome.Ground, botMap, biome);
            Vector3Int tmp_l2 = new Vector3Int(tmp2.x, tmp2.y, 0);

            if (i.nextdirection == 1)
            {

                for (int k = tmp_l1.x; k >= tmp_l2.x; k--)
                {
                    Coords newCoords = new Coords();
                    if (tmp_l2.y > tmp_l1.y)
                    {
                        tmp_l1.y++;
                    }
                    else if (tmp_l2.y < tmp_l1.y)
                    {
                        tmp_l1.y--;
                    }

                    newCoords = new Coords(k, tmp_l1.y);
                    PrintRoad(biome, i.nextdirection, newCoords);
                    
                }
            }
            if (i.nextdirection == 2)
            {
                for (int k = tmp_l1.x; k <= tmp_l2.x; k++)
                {
                    Coords newCoords = new Coords();
                    if (tmp_l2.y > tmp_l1.y)
                    {
                        tmp_l1.y++;
                    }
                    else if (tmp_l2.y < tmp_l1.y)
                    {
                        tmp_l1.y--;
                    }
             
                    newCoords = new Coords(k, tmp_l1.y);
                    PrintRoad(biome, i.nextdirection, newCoords);
                    
                }
            }
            if (i.nextdirection == 3)
            {
                for (int k = tmp_l1.y; k <= tmp_l2.y; k++)
                {
                    Coords newCoords = new Coords();

                    if (tmp_l2.x > tmp_l1.x)
                    {
                        tmp_l1.x++;
                    }
                    else if (tmp_l2.x < tmp_l1.x)
                    {
                        tmp_l1.x--;
                    }
                 
                    newCoords = new Coords(tmp_l1.x, k);
                    PrintRoad(biome, i.nextdirection, newCoords);
                   
                }
            }
            if (i.nextdirection == 4)
            {
                for (int k = tmp_l1.y; k >= tmp_l2.y; k--)
                {
                    Coords newCoords = new Coords();
                    if (tmp_l2.x > tmp_l1.x)
                    {
                        tmp_l1.x++;
                    }
                    else if (tmp_l2.x < tmp_l1.x)
                    {
                        tmp_l1.x--;
                    }

                    //if (i.Parenta.Type.TypeGenerate == Room.Generate.Normal)
                    //{

                    //    if (k % 8 == 1)
                    //    {
                    //        Vector3Int t = new Vector3Int(tmp2.x + (int)biome.roadSize + 1, tmp2.y, 0);
                    //        Vector3Int t2 = new Vector3Int(tmp2.x - (int)biome.roadSize - 1, tmp2.y, 0);
                    //        botMap.SetTile(t, biome.Ground);
                    //        decorateMap.SetTile(t, i.Parenta.Type.MainDecorate);
                    //        botMap.SetTile(t2, biome.Ground);
                    //        decorateMap.SetTile(t2, i.Parenta.Type.MainDecorate);
                    //    }
                    //}
                    newCoords = new Coords(tmp_l1.x, k);
                    PrintRoad(biome, i.nextdirection, newCoords);
                   
                }
                
            }


        }

        return DungeonMap;
    }
    
    public void GenerateRandomRoom(Biomes biome, int start)
    {
        DungeonMap.CreateMap();
        ListChunk = new List<Chunk>();
        DungeonMap.GenerateRoads();
        DungeonMap.FillListOfRoads(DungeonMap.Chunks);
     
        for (int i = 0; i < DungeonMap.Chunks.Count; i++)
        {
            float random = Random.Range(0.0f, 1.0f);
            if (random < biome.RoomSpawnChance)
            {
                DungeonMap.Chunks[i] = RG.GenerateRoom(DungeonMap.Chunks[i], start,biome);
                ListChunk.Add(DungeonMap.Chunks[i]);
            
            }
            else
            {
                DungeonMap.Chunks[i].Type = biome.GetRoom(i, start);
                DungeonMap.Chunks[i].ChestSpawned = DungeonMap.Chunks[i].Type.ChestSpawned;
                ListChunk.Add(DungeonMap.Chunks[i]);

            }
           
        }
        
        DungeonMap.Connections = DungeonMap.Prim(DungeonMap.Chunks[start], DungeonMap.Chunks);
        int max = 0;
        int id = 0;
        foreach (var i in DungeonMap.Connections)
        {
            i.Parentb.cost = i.Parenta.cost + 1;
            if (i.Parentb.cost > max)
            {
                max = (int)i.Parentb.cost;
                id = i.Parentb.ID;
            }
        }
        int counter = 0;
        foreach (var i in DungeonMap.Connections)
        {
            if(i.Parentb.ID == id)
            {
                i.Parentb.Type = biome.GetRoomLast();
            }
            counter++;
             Coords tmp1 = DungeonMap.GetCenterOfChunk(i.Parenta, biome.Ground, botMap, biome);
            Vector3Int tmp_l1 = new Vector3Int(tmp1.x, tmp1.y, 0);
            Coords tmp2 = DungeonMap.GetCenterOfChunk(i.Parentb, biome.Ground, botMap, biome);
            Vector3Int tmp_l2 = new Vector3Int(tmp2.x, tmp2.y, 0);


            if (i.nextdirection == 1)
            {

                for (int k = tmp_l1.x; k >= tmp_l2.x; k--)
                {
                    Coords newCoords = new Coords();
                    if (tmp_l2.y > tmp_l1.y)
                    {
                        tmp_l1.y++;
                    }
                    else if (tmp_l2.y < tmp_l1.y)
                    {
                        tmp_l1.y--;
                    }
                    newCoords = new Coords(k, tmp_l1.y);
                    if (i.Parenta.IsUesed)
                        PrintRoad(biome, i.nextdirection, newCoords);
                    else
                        PrintRoad(biome, i.nextdirection, newCoords);
                }
            }
            if (i.nextdirection == 2)
            {
                for (int k = tmp_l1.x; k <= tmp_l2.x; k++)
                {
                    Coords newCoords = new Coords();
                    if (tmp_l2.y > tmp_l1.y)
                    {
                        tmp_l1.y++;
                    }
                    else if (tmp_l2.y < tmp_l1.y)
                    {
                        tmp_l1.y--;
                    }
                    newCoords = new Coords(k, tmp_l1.y);
                    if (i.Parenta.IsUesed)
                        PrintRoad(biome, i.nextdirection, newCoords);
                    else
                        PrintRoad(biome, i.nextdirection, newCoords);
                }
            }
            if (i.nextdirection == 3)
            {
                for (int k = tmp_l1.y; k <= tmp_l2.y; k++)
                {
                    Coords newCoords = new Coords();

                    if (tmp_l2.x > tmp_l1.x)
                    {
                        tmp_l1.x++;
                    }
                    else if (tmp_l2.x < tmp_l1.x)
                    {
                        tmp_l1.x--;
                    }
                    newCoords = new Coords(tmp_l1.x, k);
                    if (i.Parenta.IsUesed)
                        PrintRoad(biome, i.nextdirection, newCoords);
                    else
                        PrintRoad(biome, i.nextdirection, newCoords);
                }
            }
            if (i.nextdirection == 4)
            {
                for (int k = tmp_l1.y; k >= tmp_l2.y; k--)
                {
                    Coords newCoords = new Coords();
                    if (tmp_l2.x > tmp_l1.x)
                    {
                        tmp_l1.x++;
                    }
                    else if (tmp_l2.x < tmp_l1.x)
                    {
                        tmp_l1.x--;
                    }
                    newCoords = new Coords(tmp_l1.x, k);
                    if (i.Parenta.IsUesed)
                        PrintRoad(biome, i.nextdirection, newCoords);
                    else
                        PrintRoad(biome, i.nextdirection, newCoords);
                }
            }

        }
        
    }
    public void Generate(int x, int y, int mapsizex, int mapsizey, string biome_name)
    {
        
        GameObject[] GetBiome = GameObject.FindGameObjectsWithTag("Biome");
        //Biomes biome = GetBiome[Random.Range(0,GetBiome.Length)].GetComponent<Biomes>();
        Debug.Log(biome_name);
        Biomes biome = null;
        foreach(var i in GetBiome)
        {
            Debug.Log(i.name + "-" + biome_name);
            if (i.name == biome_name)
            {
                Debug.Log("Mam");
                biome = i.GetComponent<Biomes>();
                break;
            }
        }
        Debug.Log(biome.name);
        decorateMap.ClearAllTiles();
        botMap.ClearAllTiles();
        ListWall = new List<Coords>();
        MapSize = new Coords(mapsizex, mapsizey);
        ChunkSize = new Coords(x,y);
        //if (x > y)
        //{
        //    SetPosition(x * (mapsizex), y * (mapsizey), x * (mapsizex));
        //}
        //else
        //    SetPosition(x * (mapsizex), y * (mapsizey), y * (mapsizey));
        chunk = new Chunk();
        chunk.CreateChunk(new Coords(0, 0), ChunkSize, MapSize);
        DungeonMap = new Map(MapSize,ChunkSize);
        RG.ChunkSize = ChunkSize;
        RG.MapSize = MapSize;
        RG.botMap = botMap;
        ListChunk.Clear();
        start = Random.Range(0, (MapSize.x*MapSize.y) - 1);
       
        if (biome.TypeGenerate == Biomes.Generate.RoadOnly)
        {
          
            RoadGenerate(biome, start);
        }
        else if (biome.TypeGenerate == Biomes.Generate.Rooms)
        {
            RoomGenerate(biome, start);
        }
        else if (biome.TypeGenerate == Biomes.Generate.RandomRooms)
        {
            GenerateRandomRoom(biome, start);

        }
        
        DungeonMap = GenerateMainDecorate(biome, ListChunk, botMap);

        CreateWall(biome);

        PaintVoid(biome);
        if(biome.Void != null)
         PaintBackground(biome);







        foreach (var i in ListChunk)
        {
            for (int j = 0; j < 5; j++)
            {
                if (i.ID != start)
                {

                    Vector2 tmp = gameObject.GetComponent<SpawnyEnemy>().SetEnemy(ListChunk[i.ID],biome);
                  
                }
            }

        }
        gameObject.GetComponent<ProgressMission>().SetEnemys();
      
        gameObject.GetComponent<SpawnPlayer>().SetPlayer(ListChunk[start], DungeonMap.GetCenterOfChunk(ListChunk[start], biome.Ground,biome.Road, botMap,biome));
        gameObject.GetComponent<SwitchHud>().sw();
    }
    public Map GenerateMainDecorate(Biomes biome, List<Chunk> ListChunk, Tilemap botMap)
    {

        foreach (var i in ListChunk)
        {

            if (i.Type.TypeGenerate == Room.Generate.Random && i.IsUesed)
            {

                for (int j = 0; j < i.ListCoords.Count; j = j + Random.Range(ChunkSize.x, (ChunkSize.y + ChunkSize.x / 2)))
                {
                    Vector3Int t = new Vector3Int(i.ListCoords[j].x, i.ListCoords[j].y, 0);

                    if (biome.Ground == botMap.GetTile(t) && i.ListCoords[j].IsNearTextureWith(botMap, biome.Ground) == 8 && i.ListCoords[j].IsNearTextureWith(decorateMap, null) == 8)
                    {
                        float random = Random.Range(0.0f, 1.0f);
                        if (random >= 0.5)
                        {
                            Collider2D tmpcol = Instantiate(collider, new Vector3(t.x + 0.5f, t.y + 0.5f, t.z), Quaternion.identity);
                            decorateMap.SetTile(t, i.Type.MainDecorate);
                            Coords tmp = DungeonMap.GetCenterOfChunk(i, biome.Ground, botMap, biome);
                            Coords tmp2 = PaintChest(biome, i, decorateMap, botMap);

                            if (tmp2 != null)
                            {
             
                                PaintConnect(biome, tmp2, tmp, biome.Ground);
                            }
                        }
                    }
                }
                int trashmax = 2;
                while (trashmax >= 0)
                {
                    Coords trash = DungeonMap.GetRandomCoordsPlace(i, biome);
                    Vector3Int normal = new Vector3Int(trash.x, trash.y, 0);
                    if (trash.IsNearTextureWith(botMap, biome.Ground) > 2 && biome.Ground == botMap.GetTile(normal) && decorateMap.GetTile(normal) == null)
                    {
                        float random = Random.Range(0.0f, 1.0f);
                        if (random >= i.Type.trashSpawnChance)
                            decorateMap.SetTile(normal, i.Type.Trash);
                        trashmax--;

                    }
                }


            }
            if (i.Type.TypeGenerate == Room.Generate.Normal && i.IsUesed)
            {
                Vector3Int normal = new Vector3Int();

                for (int j = 0; j < i.ListCoords.Count; j = j + 2)
                {
                    if ((i.ListCoords[j].x % ChunkSize.x == (ChunkSize.x - 6) && i.ListCoords[j].y % ChunkSize.y == (ChunkSize.y - 6)) || (i.ListCoords[j].x % ChunkSize.x == 6 && i.ListCoords[j].y % ChunkSize.y == 6) || (i.ListCoords[j].x % ChunkSize.x == (ChunkSize.x - 6) && i.ListCoords[j].y % ChunkSize.y == 6) || (i.ListCoords[j].x % ChunkSize.x == 6 && i.ListCoords[j].y % ChunkSize.y == (ChunkSize.y - 6)))
                    {
                        normal = new Vector3Int(i.ListCoords[j].x, i.ListCoords[j].y, 0);

                        if (biome.Ground == botMap.GetTile(normal))
                        {
                            Collider2D tmpcol = Instantiate(collider, new Vector3(normal.x + 0.5f, normal.y + 0.5f, normal.z), Quaternion.identity);
                            decorateMap.SetTile(normal, i.Type.MainDecorate);

                            Coords tmp = DungeonMap.GetCenterOfChunk(i, biome.Ground, botMap, biome);
                            Coords tmp2 =PaintChest(biome, i, decorateMap, botMap);
                            if (tmp2 != null)
                            {
                                
                                PaintConnect(biome, tmp2, tmp, biome.Ground);
                            }
                        }

                    }
                }
                int trashmax = 2;

                while (trashmax > 0)
                {
                    Coords trash = DungeonMap.GetRandomCoordsPlace(i, biome);
                    normal = new Vector3Int(trash.x, trash.y, 0);
                    if (trash.IsNearTextureWith(botMap, biome.Ground) > 2 && biome.Ground == botMap.GetTile(normal) && decorateMap.GetTile(normal) == null)
                    {
                        float random = Random.Range(0.0f, 1.0f);
                        if (random >= i.Type.trashSpawnChance)
                            decorateMap.SetTile(normal, i.Type.Trash);
                        trashmax--;

                    }

                }
            }
            if (i.Type.TypeGenerate == Room.Generate.Start || i.Type.TypeGenerate == Room.Generate.End)
            {
                Coords tmp = DungeonMap.GetCenterOfChunk(i, biome.Ground,biome.Road, botMap, biome);
                Vector3Int normal = new Vector3Int(tmp.x, tmp.y, 0);
                decorateMap.SetTile(normal, i.Type.MainDecorate);
                
                if (i.IsUesed)
                {
                    
                    Coords tmp2 = PaintChest(biome, i, decorateMap, botMap);
     
                    if (tmp2 != null)
                    {
     
                        PaintConnect(biome, tmp2, tmp, biome.Ground);
                    }
                }
                int trashmax = 2;

                while (trashmax > 0)
                {
                    Coords trash = DungeonMap.GetRandomCoordsPlace(i, biome);
                    normal = new Vector3Int(trash.x, trash.y, 0);
                    if (trash.IsNearTextureWith(botMap, biome.Ground) > 2 && biome.Ground == botMap.GetTile(normal) && decorateMap.GetTile(normal) == null && i.IsUesed)
                    {
                        float random = Random.Range(0.0f, 1.0f);
                        if (random >= i.Type.trashSpawnChance)
                            decorateMap.SetTile(normal, i.Type.Trash);
                        trashmax--;

                    }
                    else if (!i.IsUesed)
                    {
      
                        trashmax--;

                    }

                }


            }


        }
        return DungeonMap;
    }
    public Coords PaintChest(Biomes biome, Chunk i, Tilemap decorateMap, Tilemap botMap)
    {
        
        Coords tmp = DungeonMap.GetRandomCoordsPlace(i, biome);
        Vector3Int newcoords = new Vector3Int(tmp.x, tmp.y, 0);

        if (!i.ChestSpawned)
        {
            Tile chest = i.Type.SpawnChest();

            if (biome.Ground == botMap.GetTile(newcoords))
            {

                decorateMap.SetTile(newcoords, chest);
                i.ChestSpawned = true;
                if (chest != null)
                    return tmp;
                else
                    return null;
            }
            else
            {
                return PaintChest(biome, i, decorateMap, botMap);
                
            }

        }
        else
        {
            return null;

        }
    }
    public void PaintVoid(Biomes biomes)
    {
        foreach(var i in ListChunk)
        {
            foreach(var j in i.ListCoords)
            {
                Vector3Int PaintPixel = new Vector3Int(j.x, j.y, 0);
                if (botMap.GetTile(PaintPixel) == null)
                {
                    //Collider2D tmpcol = Instantiate(collider, new Vector3(PaintPixel.x + 0.5f, PaintPixel.y + 0.5f, PaintPixel.z), Quaternion.identity);
//                    tmpcol.transform.parent = ParentCol.transform;


                    botMap.SetTile(PaintPixel, biomes.Wall);
     
                }
            }
        }
    }
    public void PaintBackground(Biomes biomes)
    {
        foreach (var i in ListChunk)
        {
            foreach (var j in i.ListCoords)
            {
                Vector3Int PaintPixel = new Vector3Int(j.x, j.y, 0);
                if (backgroundMap.GetTile(PaintPixel) == null)
                {
                    //Collider2D tmpcol = Instantiate(collider, new Vector3(PaintPixel.x + 0.5f, PaintPixel.y + 0.5f, PaintPixel.z), Quaternion.identity);
                    //                    tmpcol.transform.parent = ParentCol.transform;


                    backgroundMap.SetTile(PaintPixel, biomes.Void);

                }
            }
        }
    }
    public void CreateWall(Biomes biome)
    {
        foreach (var i in ListChunk)
        {
            if (!biome.FullRoad && !i.IsUesed)
            {
                foreach (var j in i.ListCoords)
                {
                    // Tworzenie Drogi g³ównie na ³aczeniach

                    Vector3Int PaintPixel = new Vector3Int(j.x, j.y, 0);

                    if (j.IsNearTextureWith(botMap, biome.Ground) > 3 && j.IsNearTextureWith(botMap, biome.Road) > 0)
                    {
                        botMap.SetTile(PaintPixel, biome.Ground);

                        j.id_chunk = i.ID;
                    }
                }
            }
        }
        foreach (var i in ListChunk)
        {
            if (!biome.FullRoad && i.IsUesed)
            {
                foreach (var j in i.ListCoords)
                {
                    // Tworzenie Drogi g³ównie na ³aczeniach

                    Vector3Int PaintPixel = new Vector3Int(j.x, j.y, 0);

                    if (j.IsNearTextureWith(botMap, biome.Ground) > 3 && j.IsNearTextureWith(botMap, biome.Road) > 0)
                    {
                        botMap.SetTile(PaintPixel, biome.Ground);

                        j.id_chunk = i.ID;
                    }
                }
            }

            foreach (var j in i.ListCoords)
            {

                Vector3Int PaintPixel = new Vector3Int(j.x, j.y, 0);
                if (botMap.GetTile(PaintPixel) == null)
                {

                    if (j.IsNearTextureWith(botMap, biome.Ground) > 0 || (j.IsNearTextureWith(botMap, biome.Road) > 0))
                    {
                        Collider2D tmpcol = Instantiate(collider, new Vector3(PaintPixel.x + 0.5f, PaintPixel.y + 0.5f, PaintPixel.z), Quaternion.identity);
                        tmpcol.transform.parent = ParentCol.transform;
                        botMap.SetTile(PaintPixel, biome.Wall);
                        j.id_chunk = i.ID;
                        ListWall.Add(j);

                    }
                }

                if (biome.Ground == botMap.GetTile(PaintPixel) && (j.x == ((MapSize.x * ChunkSize.x) - 1) || j.y == ((MapSize.y * ChunkSize.y) - 1)) || biome.Road == botMap.GetTile(PaintPixel) && (j.x == ((MapSize.x * ChunkSize.x) - 1) || j.y == ((MapSize.y * ChunkSize.y) - 1)))
                {
                    botMap.SetTile(PaintPixel, null);
                    botMap.SetTile(PaintPixel, biome.Wall);
                    Collider2D tmpcol = Instantiate(collider, new Vector3(PaintPixel.x + 0.5f, PaintPixel.y + 0.5f, PaintPixel.z), Quaternion.identity);
                    tmpcol.transform.parent = ParentCol.transform;
                    j.id_chunk = i.ID;
                    ListWall.Add(j);
                }
                else if (biome.Ground == (botMap.GetTile(PaintPixel)) && (j.x == 0 || j.y == 0) || biome.Road == (botMap.GetTile(PaintPixel)) && (j.x == 0 || j.y == 0))
                {
                    botMap.SetTile(PaintPixel, null);
                    botMap.SetTile(PaintPixel, biome.Wall);
                    j.id_chunk = i.ID;
                    Collider2D tmpcol = Instantiate(collider, new Vector3(PaintPixel.x + 0.5f, PaintPixel.y + 0.5f, PaintPixel.z), Quaternion.identity);
                    tmpcol.transform.parent = ParentCol.transform;

                    ListWall.Add(j);
                }


            }
        }



    }
    public void PaintConnect(Biomes biome,Coords first, Coords seconds, RuleTile ground)
    {
       
        if (seconds.x < first.x )
        {

            for (int k = first.x; k >= seconds.x; k--)
            {
                Coords newCoords = new Coords();
                if (seconds.y > first.y)
                {
                    first.y++;
                }
                else if (seconds.y < first.y)
                {
                    first.y--;
                }
                newCoords = new Coords(k, first.y);
                PrintRoad(biome, 1, newCoords, ground);

            }
        }
        if (seconds.x > first.x)
        {
            for (int k = first.x; k <= seconds.x; k++)
            {
                Coords newCoords = new Coords();
                if (seconds.y > first.y)
                {
                    first.y++;
                }
                else if (seconds.y < first.y)
                {
                    first.y--;
                }
                newCoords = new Coords(k, first.y);
                PrintRoad(biome, 2, newCoords, ground);

            }
        }
        if (seconds.y > first.y)
        {
            for (int k = first.y; k <= seconds.y; k++)
            {
                Coords newCoords = new Coords();

                if (seconds.x > first.x)
                {
                    first.x++;
                }
                else if (seconds.x < first.x)
                {
                    first.x--;
                }
                newCoords = new Coords(first.x, k);
                PrintRoad(biome, 3, newCoords);
  
            }
        }
        if (seconds.y < first.y)
        {
            for (int k = first.y; k >= seconds.y; k--)
            {
                Coords newCoords = new Coords();
                if (seconds.x > first.x)
                {
                    first.x++;
                }
                else if (seconds.x < first.x)
                {
                    first.x--;
                }
                newCoords = new Coords(first.x, k);
                PrintRoad(biome, 4, newCoords);

            }
        }
    }
    public Chunk NextChunks(int before, Chunk chunk)
    {
        
        if (before >= MapSize.x)
        {
            int next = chunk.ID - MapSize.x - 1;
            return DungeonMap.Chunks[next];
            
        }
        else
        {
            if ((chunk.ID + MapSize.x) < DungeonMap.Chunks.Count)
            {
                int next = chunk.ID + MapSize.x;
                return DungeonMap.Chunks[next];
            }
            else
                return chunk;
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           
        }
    }

}
