using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
public class SpawnPlayer : MonoBehaviour
{
    public GameObject Player;
    public DungeonGenerator dngroom;
    public SkillTree skillTree;
    public List<Chunk> chunks;
    public Vector3 offset;
    public Tilemap map;
    public GameObject AbilityMenuHandler;
    public Slider hp;
    public Slider exp;
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
      
       
   

        Vector3 SpawnPoint = new Vector3(najx, najy, 0);
        GameObject CopyPlayer = Instantiate(Player, SpawnPoint + offset, Quaternion.identity);
        ParticleSystem DParticle = Instantiate(DashParticle, Vector3.zero, Quaternion.identity);
        PlayerStats ps = CopyPlayer.GetComponent<PlayerStats>();
        ps.HealthBar = hp;
        ps.ExpBar = exp;

        CopyPlayer.GetComponent<Walk>().dashParticle = DParticle;
        skillTree.Player = CopyPlayer;
        ps.sk = skillTree;
        skillTree.UpdateAllSkills();
        
    
        CopyPlayer.GetComponent<Walk>().cam = cam;
        cam.GetComponent<CameManager>().player = CopyPlayer.transform;
        
        cam.GetComponent<CameManager>().enabled = true;
        List<ConnectToAbillity> abillityMenu = new List<ConnectToAbillity>();
        List<AbilityHolder> abilityHolder = new List<AbilityHolder>();
        abilityHolder.AddRange(CopyPlayer.GetComponentsInChildren<AbilityHolder>());
        abillityMenu.AddRange(AbilityMenuHandler.GetComponentsInChildren<ConnectToAbillity>());
        
        foreach (var i in abilityHolder)
        {
            foreach (var j in abillityMenu)
            {
                if(i.ID == j.ID)
                {
                    j.abHolder = i;
                }
            }
        }
        ps.LoadData(abillityMenu);
        skillTree.UpdateAllSkills();
    }        
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
