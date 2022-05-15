using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    public int ID;
    public Ability ability;
    public float cooldownTime;
    public float activeTime;

    public enum State
    {
        ready,
        active,
        cooldown
    }

    
    State state = State.ready;

    public KeyCode Key;
    // Update is called once per frame
    public State GetState()
    {
        return state;
    }
    void Update()
    {
        switch (state)
        {
            case State.ready:
                
                if(Input.GetKeyDown(Key) && ability != null)
                {
                    bool isActivated = ability.BeginActivate(gameObject.transform.root.gameObject);
                   
                    if (!isActivated)
                        state = State.ready;
                    else
                    {
                        activeTime = ability.activeTime;
                        state = State.active;
                    }
                }
  
            break; 
            case State.active:

                if (activeTime > 0 )
                {
                    ability.Activate(gameObject.transform.root.gameObject);
                    activeTime -= Time.deltaTime;
                }
                else
                {
                    ability.BeginCooldown(gameObject.transform.root.gameObject);
                    state = State.cooldown;
                    cooldownTime = ability.cooldownTime;
                }
            break;
            case State.cooldown:

                if (cooldownTime > 0)
                {
                    cooldownTime -= Time.deltaTime;
                }
                else
                {
                    state = State.ready;
                }
                break;
        }
    }
}
