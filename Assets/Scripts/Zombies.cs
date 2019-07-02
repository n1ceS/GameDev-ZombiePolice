using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Zombies : MonoBehaviour
{   
    public float maxHealth;
    public float health;
    public bool meleeAttack;
    public float meleeDamage;

    private void Start()
    {
        health = maxHealth;
    }

    void ak47hit(float damage)
    {
        health -= damage;
        if(health <=0)
        {
            Destroy(this.gameObject);
        }
        Debug.Log(damage);
        
    }
    void isDeath()
    {
        if(health <=0)
        {
            Die();
        }
    }
    void Die()
    {

    }
}
