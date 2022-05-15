using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameconsole
{
    public static void SendInfo(string text)
    {
        GameObject.FindWithTag("console").GetComponent<GameConsoleControl>().ShowInfoGame(text);
    }
    
}
