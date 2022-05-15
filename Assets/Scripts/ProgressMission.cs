using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressMission : MonoBehaviour
{
    // Start is called before the first frame update
    public int CounterMaxEnemy;
    public Slider slider;
    public Text changetext;
    public float killed = 0;
    public string QuestInfo;
    public float secondToKill;
    private float calcToKill;
    public int mission = 0;

    public void Start()
    {
        killed = 0;
      
    }
    public void KillEnemys()
    {

        QuestInfo = "Kill " + killed + " / " + CounterMaxEnemy + " vampires and find a way to leave Dungeon";
        changetext.text = QuestInfo;
        if (killed > 0)
        {
            slider.value = PercentCount(CounterMaxEnemy, killed);


            if (slider.value == 100)
            {
                PlayerStats stats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
                
                Saving.SaveGameData(stats);

            }
        }
    }
    public void calcKill(int PlayerLevel)
    {
        calcToKill = secondToKill - ((PlayerLevel * 0.01f) * secondToKill);
    }    
    public void KillEnemysOnTime()
    {


        //QuestInfo = "Kill " + killed + " / " + CounterMaxEnemy + " vampires on time: " + calcToKill * CounterMaxEnemy + " and find a way to leave dungeon on this time.";
        QuestInfo = "Kill " + killed + " / " + CounterMaxEnemy + " vampires and find a way to leave Dungeon";
        changetext.text = QuestInfo;
        if (killed > 0)
        {
            slider.value = PercentCount(CounterMaxEnemy, killed);


            if (slider.value == 100)
            {
                PlayerStats stats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
                Saving.SaveGameData(stats);
     
            }
        }

    }
    public bool isComplete()
    {
        if (killed == CounterMaxEnemy)
            return true;
        return false;
    }
    public void calcEnemy()
    {
     
        if (mission == 1)
            KillEnemys();
        else if (mission == 2)
            KillEnemysOnTime();
    }
    public void pickMission(int level)
    {
        if (level == null)
            level = 1;
        mission = Random.Range(1, 3);
        if (mission == 2)
        {
            calcKill(level);
        }
    }
    public float PercentCount(int maxEnemy, float Killed)
    {
        return (Killed / maxEnemy) *100;
    }
    public void addEnemy()
    {
        CounterMaxEnemy++;
    }
    public void Killed()
    {

        if (killed < CounterMaxEnemy)
            killed++;
        else
            killed = CounterMaxEnemy;
    }
    public void SetEnemys()
    {
        CounterMaxEnemy = (int)(CounterMaxEnemy * 0.75f);
        if(mission == 2)
        {
            QuestInfo = "Kill " + killed + " / " + CounterMaxEnemy + " vampires and find a way to leave Dungeon";
        }
        else if(mission ==  1)
        {
            QuestInfo = "Kill " + killed + " / " + CounterMaxEnemy + " vampires and find a way to leave Dungeon";
        }

        changetext.text = QuestInfo;
        slider.value = PercentCount(CounterMaxEnemy, killed);
  
    }

    // Update is called once per frame
    void Update()
    {

    }
}
