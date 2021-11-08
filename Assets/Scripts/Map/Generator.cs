using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class Generator : MonoBehaviour
{
    public Tilemap botMap;
    public Tilemap midgroundMap;

    public Tile Grass1;
    public Tile Grass2;
    public Tile Grass3;

    public Tile Tree1p1;
    public Tile Tree1p2;

    public Vector3Int coords;

    public int TownCount;

    public int height;
    public int width;
    public GameObject Collider2D;
    public GameObject TownCenter;
    public LayerMask Layer;

    public List<GameObject> ListTownCenter;
    RaycastHit rhit;
    // Start is called before the first frame update


    void GenerateRoad()
    {

    }
    void GenerateTown(int HowMany)
    {
        GameObject TMP;
        bool WasTryToCreate = false;
        int CreatedTown = 0;
        for (int x = 50; x < width-50; x++)
        {
            for (int y = 50; y < height-50; y++)
            {
                {
                    float SpawnR = Random.Range(0f, 1f);
                    if (!WasTryToCreate && CreatedTown < HowMany)
                    {
                        if (SpawnR < 0.001)
                        {
                            SpawnR = Random.Range(0f, 1f);
                            if (SpawnR < 0.1 && !WasTryToCreate)
                            {
                                TMP = Instantiate(TownCenter, new Vector3(x, y, 0), Quaternion.identity);
                                ListTownCenter.Add(TMP);
                                CreatedTown++;
                                WasTryToCreate = true;
                            }
                            else
                                WasTryToCreate = true;
                        }

                    }
                    else
                    {
                        float SpawnO = Random.Range(0f, 1f);
                        if (x + 75 < width - 50 && y + 75 < height)
                        {
                            if (SpawnO <= 0.25)
                                x += 75;

                            else if (SpawnO > 0.25 && SpawnO <= 0.5)
                                y += 75;
                            else if (SpawnO > 0.5)
                            {
                                y += 75;
                                x += 75;
                            }
                        }

                        WasTryToCreate = false;

                    }

                    


                }

            }
        }
    }
    void TreeGenerate()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                coords = new Vector3Int(x, y, 0);
                Vector3Int coordsUP = new Vector3Int(x, y+1,0);
                float SpawnR = Random.Range(0f, 1f);
                if (botMap.GetTile(coords) == Grass1)
                {
                    if (SpawnR > 0.95)
                    {
                        
                        midgroundMap.SetTile(coords, Tree1p1);
                        Instantiate(Collider2D, new Vector3(x, y, 0), Quaternion.identity);
                        midgroundMap.SetTile(coordsUP, Tree1p2);
                    }

                }
                    
            }
        }
    }
    void Start()
    {
        ListTownCenter = new List<GameObject>();
        width = 1000;
        height = 1000;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float SpawnR = Random.Range(0f, 1f);
                if(SpawnR > 0.15)
                    botMap.SetTile(new Vector3Int(x, y, 0), Grass1);
                else if(SpawnR > 0.1 && SpawnR <= 0.15)
                    botMap.SetTile(new Vector3Int(x, y, 0), Grass2);
                else if (SpawnR > 0 && SpawnR <= 0.10)
                    botMap.SetTile(new Vector3Int(x, y, 0), Grass3);
            }
        }
        GenerateTown(TownCount);
        TreeGenerate();

    }

    

    // Update is called once per frame
    void Update()
    {

    
    }
}