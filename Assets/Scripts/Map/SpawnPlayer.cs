using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
public class SpawnPlayer : MonoBehaviour
{
    public GameObject Player;
    public DungeonGenerator dngroom;
    public List<Chunk> chunks;
    public Vector3 offset;
    public Tilemap map;
    public Slider hp;
    public ParticleSystem DashParticle;

    public PlayerStats ps;
    public Camera cam;
    
    // Start is called before the first frame update
    void Start()
    {
       
       
    }
    public void SetPlayer(Chunk chunk, Coords spawncoords)
    {

        
        map = dngroom.botMap;

        int najx = 0;
        int najy = 0;
     

        najx = spawncoords.x;
        najy = spawncoords.y;
      
       
        Debug.Log(najx + " " + najy);

        Vector3 SpawnPoint = new Vector3(najx, najy, 0);
        GameObject CopyPlayer = Instantiate(Player, SpawnPoint + offset, Quaternion.identity);
        ParticleSystem DParticle = Instantiate(DashParticle, Vector3.zero, Quaternion.identity);
        CopyPlayer.GetComponent<PlayerStats>().HealthBar = hp;
        CopyPlayer.GetComponent<Walk>().dashParticle = DParticle;
        cam.GetComponent<CameManager>().player = CopyPlayer.transform;
        CopyPlayer.GetComponent<Walk>().cam = cam;
        cam.GetComponent<CameManager>().enabled = true;

    }        
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
