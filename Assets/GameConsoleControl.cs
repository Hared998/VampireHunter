using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameConsoleControl : MonoBehaviour
{
    Text textconsole;
    private bool isShowing = false;
    private float nextShow = 0f;
    public float availabletime = 5f;

    public void ShowInfoGame(string text)
    {
        textconsole.text = text;
        StartCoroutine(WaitAndPrint());
    }
    // Start is called before the first frame update
    void Start()
    {
        textconsole.enabled = false;
    }
    private IEnumerator WaitAndPrint()
    {
       textconsole.enabled = true;
       yield return new WaitForSeconds(availabletime);
        textconsole.enabled = false;
;    }
    private void Awake()
    {
        textconsole = GetComponent<Text>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
