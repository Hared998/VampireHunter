using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Walk : MonoBehaviour
{

    public Animator anim;
    public Rigidbody2D Player;
    public float DefaultSpeed;
    public float SpeedPlayer;
    public Vector2 movement;
    Vector2 mousePos;
    

    public float dashCooldown;
    public float dashDistance;
    public bool isDash = false;
    public Transform DashDetector;

    public ParticleSystem dashParticle;

    public Vector3 offset;
    public Camera cam;
    public ParticleSystem blood;

    public Skill ESkill;

    public bool walkingWithCamera =false;
    public bool IsWalking = false;

    private Character character;
    // Start is called before the first frame update
    void Start()
    {     
        DefaultSpeed = 4;
        character = gameObject.GetComponent<Character>();
        List<Statistic> statisticsList = character.ReturnStatiticList();
        foreach(var stat in statisticsList)
        {
            if(stat.statisticId == StatName.Speed)
            {
                if (stat.actualPoint > 0)
                    DefaultSpeed = (float)stat.actualPoint;
            }
        }

        SpeedPlayer = DefaultSpeed;
    }

    void Update()
    {
    }


    void FixedUpdate()
    {

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        anim.SetFloat("speed", movement.y);
        if(walkingWithCamera)
            movement = transform.TransformDirection(movement);
        Vector2 movewithcamera = (Player.position + movement.normalized * SpeedPlayer * Time.deltaTime);

        Player.MovePosition(movewithcamera);
        Vector2 lookDir = mousePos - Player.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        Player.rotation = angle;
        if (movement.x != 0 || movement.y != 0)
            IsWalking = true;
        else
            IsWalking = false;
        //SkillsManager();
        if (IsWalking == true)
        {
            List<Statistic> statisticsList = character.ReturnStatiticList();
            foreach (var stat in statisticsList)
            {
                if (stat.statisticId == StatName.Speed)
                {
                    if (stat.actualPoint > 0)
                        SpeedPlayer = (float)stat.actualPoint;
                }
            }
        }
    }
    // Update is called once per frame

   

}
