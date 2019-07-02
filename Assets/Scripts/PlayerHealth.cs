using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float Health;
    public float actualHealth;
    public damageScreen ds;
    public AudioClip hit;
    bool canTakeDamage = true;
    AudioSource source;
    
    // Start is called before the first frame update
    void Start()
    {
        actualHealth = Health;
        source = GetComponent<AudioSource>();

    }

    void enemyHit(float damage)
    {if (canTakeDamage)
        {
            source.PlayOneShot(hit);
            actualHealth -= damage;
            ds.GetDamage();
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(1);
        canTakeDamage = true;
    }
}
