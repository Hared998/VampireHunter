using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class DungeonRoom : MonoBehaviour
{
    public Tilemap botMap;

    public Coords ChunkSize;
    public Coords MapSize;
    public Chunk chunk;

    public BoxCollider2D WallCollider;

    public float Scale;
    public Biomes biome;
    public Vector2 RoomSize;
    public List<Vector2> ListRoomChunk;
    public List<Chunk> ListChunk;
    public List<Coords> ListWall;


    public Tile[] Tiles;

    public Map DungeonMap;

    public string type;

    public void PaintRoomSquare(Chunk PaintChank, int start, Biomes biome)
    {
     
        PaintChank.Type = biome.GetRoom(PaintChank.ID, start);

        PaintChank.ChestSpawned = PaintChank.Type.ChestSpawned;
        float RandomCoords = Random.Range(0, 100000);
        int filled = 0;
        foreach (var i in PaintChank.ListCoords)
        {
            if (i.x % ChunkSize.x < (ChunkSize.x - 3) && i.y % ChunkSize.y < (ChunkSize.y - 3))
            {
                float yCoord = (((float)i.x / ((MapSize.x * ChunkSize.x))) + RandomCoords) * Scale;
                float xCoord = (((float)i.y / ((MapSize.y * ChunkSize.y))) + RandomCoords) * Scale;
                RuleTile tile = GetTexture(yCoord, xCoord,biome);
                if(tile != null)
                {
                    i.type = "ground";
                    botMap.SetTile(new Vector3Int(i.x, i.y, 0), tile);
                    filled++;
                }
            }
        }
        IsHalfPainted(PaintChank,filled, start,biome);

    }
    public void GenerateSpecialRoom(Chunk chunk)
    {
        chunk.IsUesed = true;
        float RandomCoords = Random.Range(0, 100000);
        int filled = 0;
        foreach (var i in chunk.ListCoords)
        {
            if (i.x % ChunkSize.x < (ChunkSize.x - 3) && i.y % ChunkSize.y < (ChunkSize.y - 3))
            {
                float yCoord = (((float)i.x / ((MapSize.x * ChunkSize.x))) + RandomCoords) * Scale;
                float xCoord = (((float)i.y / ((MapSize.y * ChunkSize.y))) + RandomCoords) * Scale;
                RuleTile tile = GetTexture(yCoord, xCoord,biome);
                if (tile != null)
                {
                    i.type = "ground";
                    botMap.SetTile(new Vector3Int(i.x, i.y, 0), tile);
                    filled++;
                }
            }
        }
        IsHalfPainted(chunk, filled);
    }
    public Chunk GenerateRoom(Chunk WhichChunk, int start, Biomes biome)
    {
        PaintRoomSquare(WhichChunk, start, biome);
        WhichChunk.IsUesed = true;
        return WhichChunk;
    }

    public void IsHalfPainted(Chunk PaintChank, int filled, int start,Biomes biome)
    {
        

        float sum = filled / (float)PaintChank.ListCoords.Count;
        
        if (sum < biome.minFill || sum > biome.maxFill)
        {
            PaintRoomSquare(PaintChank, start,biome);
        }
        
    }
    public void IsHalfPainted(Chunk PaintChank, int filled)
    {


        float sum = filled / (float)PaintChank.ListCoords.Count;

        if (sum < biome.minFill || sum > biome.maxFill)
        {
            GenerateSpecialRoom(PaintChank);
        }

    }
    public RuleTile GetTexture(float x, float y,Biomes biome)
    {
        float noises = Mathf.PerlinNoise(x, y * 1.5f);
      
        if (biome.liquid)
        {

            if (noises > 0.80)
                return biome.liquidtile;

            if (noises > 0.40)
                return biome.Ground;

           
        }
        else
        {

            if (noises > 0.40)
                return biome.Ground;
        }
       
        return null;
    }
    
    public int SetDirection(int x, int y)
    {
        if(x % ChunkSize.x < ChunkSize.x / 2 && y % ChunkSize.y < ChunkSize.x/2)
        {
            return 4;
        }
        else if (x % ChunkSize.x < ChunkSize.x / 2 && y % ChunkSize.y >= ChunkSize.x / 2)
        {
            return 1;
        }
        else if (x % ChunkSize.x >= ChunkSize.x / 2 && y % ChunkSize.y < ChunkSize.x / 2)
        {
            return 2;
        }
        else if (x % ChunkSize.x >= ChunkSize.x / 2 && y % ChunkSize.y >= ChunkSize.x / 2)
        {
            return 3;
        }
        return 0;
    }

    void Start()
    {

       
        
        //PG.maxEnemy = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

}