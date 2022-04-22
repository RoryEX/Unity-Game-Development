using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    protected string weaponName;
    protected int maxAmmo;
    protected int currentAmmo;
    protected int damage;

    public virtual void FireWeapon()
    {
        Debug.Log("Fired!"+ currentAmmo +" ammo left");
        
    }

    public virtual void Reload()
    {
        Debug.Log("Reloaded!");
    }
}
