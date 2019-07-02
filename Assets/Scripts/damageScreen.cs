using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class damageScreen : MonoBehaviour
{
    // Start is called before the first frame update
    Image damageSs;
    void Start()
    {
        damageSs = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(damageSs.color.a > 0)
        {
            Color newColor = new Color(damageSs.color.r, damageSs.color.g, damageSs.color.b, 0);
            damageSs.color = Color.Lerp(damageSs.color, newColor, 10 * Time.deltaTime);
        }
    }
    public void GetDamage()
    {
        damageSs.color = new Color(1, 0, 0, 0.7f);
    }
}
