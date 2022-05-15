using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseGame : MonoBehaviour
{
    public GameObject PauseOBject;
    public GameObject SkillMenu;
    public GameObject Equipment;
    

    void Start()
    {
        
    }
    public void ExitGame()
    {
        SceneManager.LoadScene(0);
    }
    public void ReturnGame()
    {
        PauseOBject.SetActive(false);
        Time.timeScale = 1;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SkillMenu.SetActive(false);
            Equipment.SetActive(false);
            if (PauseOBject.active)
            {
 
                PauseOBject.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                PauseOBject.SetActive(true);
                Time.timeScale = 0;
            }

            
        }
    }
}
