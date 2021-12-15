using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public float Health = 100;
    public float Armor = 10;

    public int BaseDamage = 5;
    public int Power = 50;

    public int SkillPoints = 0;
    public int baseexpNeeded = 100;
    public int expNeeded;
    public int Level = 1;
    public int expierience = 0;

    public SkillTree sk;

    public Slider HealthBar;
    void Start()
    {
        expNeeded = baseexpNeeded;
    }
    public void TakeDamage(float Damage)
    {
        float ReduceDamage = (100 - Armor) / 100;
        float RealDamage = Damage - ReduceDamage;
        RealDamage = Mathf.Round(RealDamage);
        Health -= RealDamage;


    }


    // Update is called once per frame
    void Update()
    {
        HealthBar.value = Health;
        if (Health <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(0);
        }
    }
    public void UpdateLevel(int Giveexp)
    {
        expierience += Giveexp;
        if (expierience > expNeeded)
        {
            Level++;
            expNeeded += (baseexpNeeded * 2);
            SkillPoints++;
            sk.UpdateAllSkills();
        }
    }
}
