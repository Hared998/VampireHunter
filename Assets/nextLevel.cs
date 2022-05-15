using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextLevel : MonoBehaviour
{
    public GameObject PressToEnter;
    public ProgressMission Mission;
    bool isNextLevel;
    // Start is called before the first frame update
    void Start()
    {
        isNextLevel = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Mission.isComplete())
            {
                PressToEnter.SetActive(true);
                if (Input.GetKey(KeyCode.F) && !isNextLevel)
                {
                    isNextLevel = true;
                    Saving.SaveGameData(collision.GetComponent<PlayerStats>());
                    SceneManager.LoadScene(1);
                }
            }
        }

    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PressToEnter.SetActive(false);
        }

    }


}
