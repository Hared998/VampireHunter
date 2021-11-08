using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class MenuController : MonoBehaviour
{
    public AudioSource click;
    public AudioSource Hover;
    public void exit()
    {
        Application.Quit();
    }
    public void start()
    {
        SceneManager.LoadScene(1);
    }
    public void OnEnterMouse()
    {
        Hover.Play();
    }
    public void OnClickMouse()
    {
        click.Play();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
