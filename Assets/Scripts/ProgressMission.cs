using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ProgressMission : MonoBehaviour
{
    // Start is called before the first frame update
    public static int CounterMaxEnemy;
    public Slider slider;
    public float killed;
    
    public float PercentCount(int maxEnemy, float Killed)
    {
        return (Killed / maxEnemy) *100;
    }
    public void Killed()
    {
       killed = CounterMaxEnemy - GameObject.FindGameObjectsWithTag("Enemy").Length;
    }
    public void SetEnemys()
    {
        CounterMaxEnemy = GameObject.FindGameObjectsWithTag("Enemy").Length;
        slider.value = PercentCount(CounterMaxEnemy, killed);
    }

    // Update is called once per frame
    void Update()
    {

        Killed();
        if (killed > 0)
        {
            slider.value = PercentCount(CounterMaxEnemy, killed);


            if (slider.value == 100)
            {
                //  SceneManager.LoadScene(1);
            }
        }

    }
}
