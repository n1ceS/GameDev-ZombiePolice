
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(AudioSource))]
public class Ak47 : MonoBehaviour
{
    public Light pointLight;
    public float ak47Damage;
    public float ak47Range;
    public AudioClip shotSound;
    AudioSource source;
    public AudioClip emptySound;
    public AudioClip reloadSound;

    public Text ammoText;
    public int ammoAmount;
    public int ammoClipAmount;
    bool isReloading;
    int ammoLeft;
    int ammoSizeLeft;
    bool Shooting;

    public GameObject bulletHole;

    private void Start()
    {
        pointLight.enabled = false;
        isReloading = false;
    }
    private void Awake()
    {
        source = GetComponent<AudioSource>();
        ammoLeft = ammoAmount; //30
        ammoSizeLeft = ammoClipAmount;//90
           

            }
    void Update()
    {
        ammoText.text = ammoLeft + " / " + ammoSizeLeft;

        if (Input.GetKeyDown(KeyCode.Mouse0))
            {
            Shooting = true;
        }
        if (Input.GetKeyDown(KeyCode.R) && isReloading == false)
        {
            Reload();
        }
     
    }

    void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Shooting && ammoLeft > 0 && isReloading==false)
        { Shooting = false;
            ammoLeft--;
            source.PlayOneShot(shotSound);
            StartCoroutine("shot");
            if(Physics.Raycast(ray, out hit,ak47Range))
            {
                Debug.Log("KOLIZJA z "+ hit.collider.gameObject.name);
                if (hit.transform.CompareTag("Enemy"))
                {
                    hit.collider.gameObject.SendMessage("ak47hit", ak47Damage, SendMessageOptions.DontRequireReceiver);
                    if (hit.collider.gameObject.GetComponent<zombieStates>().currentState == hit.collider.gameObject.GetComponent<zombieStates>().patrolState || hit.collider.gameObject.GetComponent<zombieStates>().currentState == hit.collider.gameObject.GetComponent<zombieStates>().alertState)
                        hit.collider.gameObject.SendMessage("ShootingFrom", transform.parent.transform.position, SendMessageOptions.DontRequireReceiver);
                }
                else
                {
                    Instantiate(bulletHole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                }
                }
        }else if(Shooting && ammoLeft <=0)
        {
            Reload();
        }
    
        
    }
    void Reload()
    {
        int neededAmmo =ammoAmount - ammoLeft;
        if(ammoSizeLeft >= neededAmmo)
        {
            StartCoroutine("ReloadWeapon");
            ammoSizeLeft -= neededAmmo;
            ammoLeft = ammoAmount;
        }else if(ammoSizeLeft <  neededAmmo && ammoSizeLeft > 0)
        {
            StartCoroutine("ReloadWeapon");
            ammoLeft += ammoSizeLeft;
            ammoSizeLeft = 0;
        }else
        {
            source.PlayOneShot(emptySound);
        }
    }
    IEnumerator ReloadWeapon()
    {
        isReloading = true;
        source.PlayOneShot(reloadSound);
        yield return new WaitForSeconds(2);
        isReloading = false;
    }
    IEnumerator shot()
    {
        pointLight.enabled = true;
        yield return new WaitForSeconds(0.1f);
        pointLight.enabled = false;
       
    }

}
