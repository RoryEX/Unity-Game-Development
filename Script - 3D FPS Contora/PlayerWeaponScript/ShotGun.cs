using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : PlayerWeapon
{
    
    [SerializeField]
    private GameObject Bullet = null;
    [SerializeField]
    private Transform muzzleLocation = null;
    private int S_backUpMagzine = 3;
    [SerializeField]
    private int force = 0;
    void Start()
    {
        weaponName = "Shot Gun";
        maxAmmo = 5;
        currentAmmo = 5;
    }

    public override void FireWeapon()
    {
        float offsetZ = 0.7f;
        float spwnPosZ = muzzleLocation.position.z + offsetZ;
        Vector3 SpwnPosB = new Vector3(muzzleLocation.position.x, muzzleLocation.position.y, spwnPosZ); 
        
        if (currentAmmo > 0)
        {
            for (int i = 0; i < 5; i++)
            {
                GameObject SGBullet = Instantiate(Bullet, SpwnPosB, transform.rotation);
                SGBullet.GetComponent<Rigidbody>().AddForce((transform.forward + muzzleLocation.forward) * force, ForceMode.Impulse);
                SpwnPosB.z+= offsetZ;
                currentAmmo--;

            }
            Debug.Log("Fired!" + currentAmmo + " ammo left");
        }
        else
        {
            Debug.Log("Out of ammo, Reload!");
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SG_BulletMagzine"))
        {
            S_backUpMagzine++;
            Debug.Log("New magazine get!");
            Destroy(other.gameObject);
        }
    }
    public override void Reload()
    {
        if (S_backUpMagzine > 0)
        {
            Debug.Log(weaponName + "has reloaded");
            currentAmmo = maxAmmo;
            S_backUpMagzine--;
        }
        else
        {
            Debug.Log(weaponName + "Out of magazine, refill it!");
        }
    }
}
