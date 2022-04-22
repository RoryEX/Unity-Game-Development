using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARWeapon : PlayerWeapon
{
    [SerializeField] 
    private GameObject Bullet = null;
    [SerializeField]
    private Transform muzzleLocation = null;
    private int backUpMagzine = 3;
    [SerializeField]
    private int force=0;
    void Start()
    {
        weaponName = "Automatic Rifle";
        maxAmmo = 30;
        currentAmmo = 30;
    }

    public override void FireWeapon()
    {
        if (currentAmmo > 0)
        {
            GameObject arBullet = Instantiate(Bullet, muzzleLocation.position + muzzleLocation.forward, transform.rotation);
            arBullet.GetComponent<Rigidbody>().AddForce((transform.forward + muzzleLocation.forward) * force, ForceMode.Impulse);
            currentAmmo--;
            Debug.Log("Fired!" + currentAmmo + " ammo left");
        }
        else 
        {
            Debug.Log("Out of ammo, Reload!");
        }
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BulletMagzine"))
        {
            backUpMagzine++;
            Debug.Log("New magazine get!");
            Destroy(other.gameObject);
        }
    }
    public override void Reload() 
    {
        if (backUpMagzine > 0)
        {
            Debug.Log(weaponName + "has reloaded");
            currentAmmo = maxAmmo;
            backUpMagzine--;
        }
        else
        {
            Debug.Log(weaponName + "Out of Magazine, refill it!");
        }
    }
}
