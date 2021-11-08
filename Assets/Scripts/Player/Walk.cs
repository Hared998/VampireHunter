using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Walk : MonoBehaviour
{

    public Animator anim;
    public Rigidbody2D Player;
    public float SpeedPlayer;
    Vector2 movement;
    Vector2 mousePos;


    public Vector3 offset;
    public Camera cam;
    public ParticleSystem blood;

    public bool IsWalking = false; 
    // Start is called before the first frame update
    void Start()
    {

        SpeedPlayer = 4;
    }

    void Update()
    {
       if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);            
        }
        
    }


    void FixedUpdate()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        anim.SetFloat("speed", movement.y);



        Player.MovePosition(Player.position + movement.normalized * SpeedPlayer * Time.deltaTime);
        Vector2 lookDir = mousePos - Player.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        Player.rotation = angle;

        if (movement.x != 0 || movement.y != 0)
            IsWalking = true;
        else
            IsWalking = false;

    }

    // Update is called once per frame
    
}
