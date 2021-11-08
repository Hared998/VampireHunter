using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public float Health = 100;
    public float Armor = 10;
    public int Level = 1;
    public int BaseDamage = 5;
    public int Power = 50;

    

    public Slider HealthBar;
    void Start()
    {
        
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
        if(Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(BaseDamage);
        }
        if (Health <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(0);
        }
    }
}
