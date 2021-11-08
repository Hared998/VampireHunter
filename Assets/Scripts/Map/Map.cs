using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
[System.Serializable]
public class Map
{

    public Coords MapSize;
    public Coords ChunkSize;
    public List<Chunk> Chunks;
    public List<Road> Connections;

 
    public Map (Coords MapSize, Coords ChunkSize)
    {
        this.MapSize = MapSize;
        this.ChunkSize = ChunkSize;
        this.Chunks = new List<Chunk>();
    }

    public Map()
    {
        this.Chunks = new List<Chunk>();
    }
    public Map SetupMap(Coords MapSize, Coords ChunkSize)
    {
        return new Map(MapSize, ChunkSize);
    }
    public Road LowestRoad(List<Road> roads)
    {
        float min = 1;
        Road tmproad = new Road();
        foreach (var i in roads)
        {
            
            if (i.value < min)
                min = i.value;
        }
        foreach (var i in roads)
        {
            if (i.value == min )
                tmproad = i;
        }
        return tmproad;

    }
    public List<Road> Prim(Chunk Start, List<Chunk>FromChunk)
    {
        List<Chunk> LeftChunnks = FromChunk;
        List<Road> KnownRoads = new List<Road>();
        List<Chunk> Visited = new List<Chunk>();
        List<Road> FastestRoad = new List<Road>();
        Chunk Check = Start;
        Visited.Add(Check);
        LeftChunnks.Remove(Check);
        foreach (var i in Check.ListRoad)
        {
            if (!KnownRoads.Contains(i) && !KnownRoads.Contains(i.Oposite()))
            {
                KnownRoads.Add(i);
            }
        }
        
        Chunk previous = Check;
        while (LeftChunnks.Count > 0)
        {
            

            Road roadtmp = new Road();

            roadtmp = LowestRoad(KnownRoads);
            KnownRoads.Remove(roadtmp);
            if (!Visited.Contains(roadtmp.Parentb))
            { 
                Check = roadtmp.Parentb;
                
                
                FastestRoad.Add(roadtmp);
                KnownRoads.Remove(roadtmp);
                Visited.Add(Check);
                LeftChunnks.Remove(Check);
                foreach (var i in Check.ListRoad)
                {
                    if (!KnownRoads.Contains(i))
                    {
                        KnownRoads.Add(i);
                    }
                }

                foreach (var i in KnownRoads)
                    if (i.Parentb != null)
                        previous = Check;
            }
            else
            {
                KnownRoads.Remove(roadtmp);
            }


        }
        foreach(var i in FastestRoad)
        {
            
            if(i.Parentb.ID + MapSize.y == i.Parenta.ID)
            {
                i.nextdirection = 1;
                
            }
            else if(i.Parentb.ID - MapSize.y == i.Parenta.ID)
            {
                i.nextdirection = 2;
                
            }
            else if (i.Parentb.ID - 1 == i.Parenta.ID)
            {
                i.nextdirection = 3;
                
            }
            else if (i.Parentb.ID + 1 == i.Parenta.ID)
            {
                i.nextdirection = 4;
            }
            
            
        }    
        return FastestRoad;

    }

        public void FillListOfRoads(List<Chunk> FromChunk)
    {
        foreach (var i in FromChunk)
        {
            foreach (var j in i.ListRoad)
            {
                if (j.Parentb != null && !FromChunk[j.Parentb.ID].ListRoad.Contains(j))
                {
                    FromChunk[j.Parentb.ID].ListRoad.Add(j.Oposite());
                }
            }
        }
    }
    public void GenerateRoads()
    {
        foreach (var i in this.Chunks)
        {
            
            Chunk right;
            Chunk up;
            if (i.ID + 1 < this.Chunks.Count)
            {
             
                up = this.Chunks[i.ID + 1];
            }
            else
                up = null;
           
            if (this.Chunks.Count > (i.ID + MapSize.y))
            {
             
                right = this.Chunks[i.ID + MapSize.y];
            }
            else
                right = null;

            i.ConnectNeigbhours(MapSize, up, right);

        }
    }

    public void CreateMap()
    {
        Coords coordsEND = new Coords();
        Coords coordsSTART = new Coords();
        Coords TMPcoords = new Coords(ChunkSize.x, ChunkSize.y);


        int id = 0;
        for (int i = 0; i < MapSize.x; i++)
        {
            TMPcoords.y = ChunkSize.y;
            for (int j = 0; j < MapSize.y; j++)
            {

                Chunk TMPChunk = new Chunk();
                coordsSTART = coordsSTART.SetCoords(TMPcoords.x - ChunkSize.x, TMPcoords.y - ChunkSize.y);
                coordsEND = coordsEND.SetCoords(TMPcoords.x, TMPcoords.y);
                TMPChunk.CreateChunk(coordsSTART, coordsEND, ChunkSize);
                TMPChunk.SetID((id));
                id++;
                this.Chunks.Add(TMPChunk);
                TMPcoords.y += ChunkSize.y;
            }

            TMPcoords.x += ChunkSize.x;
        }
    }
    public Coords GetRandomCoords(Chunk chunk, Biomes biome)
    {
        if (chunk.random == null)
        {
            int random = Random.Range(0, ChunkSize.x * ChunkSize.y);
            Coords tmp = chunk.ListCoords[random];
            if ((tmp.x % ChunkSize.x > biome.roadSize + biome.Skip && tmp.y % ChunkSize.y > biome.roadSize + biome.Skip) && (tmp.x % ChunkSize.x < (ChunkSize.x - biome.roadSize - biome.Skip) && tmp.y % ChunkSize.y < (ChunkSize.y - biome.roadSize - biome.Skip)))
            {
                foreach (var i in chunk.ListBorder)
                {
                    if (i == tmp)
                    {
                        tmp = GetRandomCoords(chunk, biome);
                    }
                }
                chunk.random = tmp;
                return tmp;
            }
            else
            {
                tmp = GetRandomCoords(chunk, biome);
                return tmp;
            }
        }
        else
        { 
            return chunk.random;
        }
    }
    public Coords GetRandomCoordsPlace(Chunk chunk, Biomes biome)
    {

        int random = Random.Range(0, ChunkSize.x * ChunkSize.y);
        Coords tmp = chunk.ListCoords[random];
        if ((tmp.x % ChunkSize.x > biome.roadSize + biome.Skip && tmp.y % ChunkSize.y > biome.roadSize + biome.Skip) && (tmp.x % ChunkSize.x < (ChunkSize.x - biome.roadSize - biome.Skip) && tmp.y % ChunkSize.y < (ChunkSize.y - biome.roadSize - biome.Skip)))
        {
            foreach (var i in chunk.ListBorder)
            {
                if (i == tmp)
                {
                    tmp = GetRandomCoordsPlace(chunk, biome);
                }
            }
            return tmp;
        }
        else
        {
            tmp = GetRandomCoordsPlace(chunk, biome);
            return tmp;
        }

    }
    public Coords GetCenterOfChunk(Chunk chunk, RuleTile tile, Tilemap map, Biomes biome)
    {
        if (chunk.IsUesed)
        {
            Coords tmpcoords = new Coords();
            int x = chunk.ListCoords[chunk.ListCoords.Count / 2].x;
            int y = chunk.ListCoords[chunk.ListCoords.Count / 2].y + (ChunkSize.y / 2);
            
            if (chunk.random == null)
            {
                tmpcoords = new Coords(x, y);
                if (!map.GetTile(new Vector3Int(x, y, 0)) == tile)
                {
                    tmpcoords = FindRoom(tmpcoords, tile, map);
                }
                chunk.random = tmpcoords;
                return tmpcoords;
            }
            else
                return chunk.random;
        }
        else
        {
            return GetRandomCoords(chunk, biome);
        }
    }
    public Coords GetCenterOfChunk(Chunk chunk, RuleTile tile, RuleTile tile2, Tilemap map, Biomes biome)
    {
        if (chunk.IsUesed)
        {
            Coords tmpcoords = new Coords();
            int x = chunk.ListCoords[chunk.ListCoords.Count / 2].x;
            int y = chunk.ListCoords[chunk.ListCoords.Count / 2].y + (ChunkSize.y / 2);

            if (chunk.random == null)
            {
                tmpcoords = new Coords(x, y);
                if (!map.GetTile(new Vector3Int(x, y, 0)) == tile || !map.GetTile(new Vector3Int(x, y, 0)) == tile2)
                {
                    tmpcoords = FindRoom(tmpcoords, tile,tile2, map);
                }
                chunk.random = tmpcoords;
                return tmpcoords;
            }
            else
                return chunk.random;
        }
        else
        {
            return GetRandomCoords(chunk, biome);
        }
    }
    public Coords FindRoom(Coords cords , RuleTile tile, Tilemap map)
    {
        List<Vector3Int> Visited = new List<Vector3Int>();
        List<Vector3Int> Tovisit = new List<Vector3Int>();
        Tovisit.AddRange(cords.GetNeighbour());

        while (Tovisit.Count > 0)
        {
            if (!Visited.Contains(Tovisit[0]))
            {
                Vector3Int tmp = Tovisit[0];
                Tovisit.Remove(tmp);
                Visited.Add(tmp);
                Tovisit.AddRange(new Coords(tmp.x, tmp.y).GetNeighbour());
                if (map.GetTile(tmp) == tile)
                {
                    return new Coords(tmp.x, tmp.y);
                    break;
                }
                
            }
            else
            {
                Tovisit.Remove(Tovisit[0]);
            }
        }
        return cords;
    }
    public Coords FindRoom(Coords cords, RuleTile tile, RuleTile tile2, Tilemap map)
    {
        List<Vector3Int> Visited = new List<Vector3Int>();
        List<Vector3Int> Tovisit = new List<Vector3Int>();
        Tovisit.AddRange(cords.GetNeighbour());

        while (Tovisit.Count > 0)
        {
            if (!Visited.Contains(Tovisit[0]))
            {
                Vector3Int tmp = Tovisit[0];
                Tovisit.Remove(tmp);
                Visited.Add(tmp);
                Tovisit.AddRange(new Coords(tmp.x, tmp.y).GetNeighbour());
                if (map.GetTile(tmp) == tile || map.GetTile(tmp) == tile2)
                {
                    return new Coords(tmp.x, tmp.y);
                    break;
                }

            }
            else
            {
                Tovisit.Remove(Tovisit[0]);
            }
        }
        return cords;
    }
}
    

[System.Serializable]
public class Chunk
{
    public Room Type;
    public float cost;
    public Coords random;
    public bool IsUesed = false;
    public List<Coords> ListCoords;
    public List<Coords> ListBorder;
    public List<Road> ListRoad =new List<Road>();
    public int ID;
    public bool ChestSpawned;
    public bool IsSpawnPlayer = false;

    public Chunk()
        {
            this.ListCoords = new List<Coords>();
            this.ListBorder = new List<Coords>();
        }

    public void SetID(int ID)
    {
        this.ID = ID;
    }
    public bool ChunkHasCoord(Coords cordy)
    {
        for(int i = 0; i < ListCoords.Count; i++)
        {
            if (ListCoords[i] == cordy)
                return true;
        }
        return false;
    }
     
    public void ConnectNeigbhours(Coords MapSize, Chunk up, Chunk right)
    {
        Road tmproad = new Road();
        tmproad.Parenta = this;
        
        if (this.ID % MapSize.y != (MapSize.y -1))
        {
            tmproad.Parentb = up;
        }
        if (tmproad.Parentb != null)
            tmproad.value = Random.Range(0.0f, 1.0f);
        else
            tmproad.value = 2f;
        this.ListRoad.Add(tmproad);
        tmproad = new Road();
        tmproad.Parenta = this;
        tmproad.Parentb = right;
        if (tmproad.Parentb != null)
            tmproad.value = Random.Range(0.0f, 1.0f);
        else
            tmproad.value = 2f;
        this.ListRoad.Add(tmproad);
    }
    
    public void UseChunk()
    {
        IsUesed = true;
    }
    public void CreateChunk(Coords Start, Coords End, Coords Mapsize)
    {

        Coords TMPcoords = new Coords();
        for (int i = Start.x; i < End.x; i++)
            
            for (int j = Start.y; j < End.y; j++)
            {

                TMPcoords.id_chunk = this.ID;
                this.ListCoords.Add(TMPcoords.SetCoords(i, j));
                if ((i == 0 || j == 0) || i == End.x - 1 || j == End.y - 1)
                    this.ListBorder.Add(TMPcoords.SetCoords(i, j));
               
                
            }
    }
}
[System.Serializable]
public class Road
{
    public int nextdirection;
    [System.NonSerialized]
    public Chunk Parenta;
    [System.NonSerialized]
    public Chunk Parentb;

   
    public float value;

    public Road Oposite()
    {
        Road tmproad = new Road();
        tmproad.Parenta = this.Parentb;
        tmproad.Parentb = this.Parenta;
        tmproad.value = this.value;
        return tmproad;
    }
    
}
[System.Serializable]
public class Coords
{
    public string type = "void";
    public int id_chunk;
    public int direction =0;
    public bool IsSpawned = false;
    public int x;
    public int y;

    public Coords(){}
    public List<Vector3Int> GetNeighbour()
    {
        List<Vector3Int> tmp = new List<Vector3Int>();
        if(new Coords(this.x + 1, this.y).id_chunk == this.id_chunk)
            tmp.Add(new Vector3Int(this.x + 1, this.y,0));
        if (new Coords(this.x - 1, this.y).id_chunk == this.id_chunk)
            tmp.Add(new Vector3Int(this.x - 1, this.y,0));
        if (new Coords(this.x, this.y-1).id_chunk == this.id_chunk)
            tmp.Add(new Vector3Int(this.x, this.y-1,0));
        if (new Coords(this.x, this.y+1).id_chunk == this.id_chunk)
            tmp.Add(new Vector3Int(this.x, this.y+1,0));
        return tmp;
    }
    public int IsNearTextureWith(Tilemap Map, RuleTile texture)
    {
        int Btmp = 0;
        Vector3Int tmp = new Vector3Int(this.x + 1, this.y, 0);
        Coords Ctmp = new Coords(this.x + 1, this.y);
   
        if (Map.GetTile(tmp) == texture)
            Btmp += 1;
        Ctmp = new Coords(this.x - 1, this.y);
        tmp = new Vector3Int(this.x - 1, this.y, 0);
        if (Map.GetTile(tmp) == texture)
            Btmp += 1;
        Ctmp = new Coords(this.x, this.y+1);
        tmp = new Vector3Int(this.x, this.y + 1, 0);
        if (Map.GetTile(tmp) == texture)
            Btmp += 1;
        tmp = new Vector3Int(this.x, this.y - 1, 0);
        Ctmp = new Coords(this.x, this.y - 1);
        if (Map.GetTile(tmp) == texture)
            Btmp += 1;
        tmp = new Vector3Int(this.x - 1, this.y + 1, 0);
        Ctmp = new Coords(this.x - 1, this.y + 1);
        if (Map.GetTile(tmp) == texture)
            Btmp += 1;
        Ctmp = new Coords(this.x - 1, this.y + 1);
        tmp = new Vector3Int(this.x - 1, this.y + 1, 0);
        if (Map.GetTile(tmp) == texture)
            Btmp += 1;
        Ctmp = new Coords(this.x + 1, this.y + 1);
        tmp = new Vector3Int(this.x + 1, this.y + 1, 0);
        if (Map.GetTile(tmp) == texture)
            Btmp += 1;
        tmp = new Vector3Int(this.x-1, this.y - 1, 0);
        Ctmp = new Coords(this.x - 1, this.y - 1);
        if (Map.GetTile(tmp) == texture)
            Btmp += 1;

        return Btmp;



    }
    public Coords(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public Coords SetCoords(int x, int y)
    {
        return new Coords(x, y);
    }

    void Start()
    {

    }
}
