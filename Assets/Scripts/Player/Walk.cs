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

    public float dashCooldown;
    public float dashDistance;
    public bool isDash = false;
    public Transform DashDetector;

    public ParticleSystem dashParticle;

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
        
      //  movement = transform.TransformDirection(movement);
        Vector2 movewithcamera = (Player.position + movement.normalized * SpeedPlayer * Time.deltaTime);
        Player.MovePosition(movewithcamera);
        Vector2 lookDir = mousePos - Player.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        Player.rotation = angle;
       
        RaycastHit2D hit = Physics2D.Raycast(DashDetector.position, transform.up, dashDistance);
     
        if (hit != null && hit.collider != null && hit.collider.CompareTag("Wall"))
        {
            if(dashDistance > 0f)
            {
                dashDistance--;
            }
        }
        else
        {
            if (dashDistance < 5f)
                dashDistance++;
        }
            if (Input.GetKey(KeyCode.E))
        {
            if(!isDash)
                StartCoroutine(DashTimer());
        }
        if (movement.x != 0 || movement.y != 0)
            IsWalking = true;
        else
            IsWalking = false;

        
    }
    IEnumerator DashTimer()
    {
        if (dashDistance > 0)
        {
            isDash = true;
            dashParticle.transform.position = Player.position;
            dashParticle.Play();

            Vector2 dash = new Vector2(0, dashDistance - 1);
            dash = transform.TransformDirection(dash);
            Vector2 movewithcamera = (Player.position + dash);
            Player.MovePosition(movewithcamera);
        }
        yield return new WaitForSeconds(dashCooldown);
        isDash = false;
    }

    // Update is called once per frame
    
}
