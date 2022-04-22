using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponList : MonoBehaviour
{
    [SerializeField] private List<PlayerWeapon> Weapons = new List<PlayerWeapon>();
    private PlayerWeapon currentWeapon = null;
    private int weaponIndex = 0;
    void Start()
    {
        Weapons.Add(GetComponent<ARWeapon>());
        //Weapons.Add(GetComponent<ShotGun>());
        currentWeapon = Weapons[weaponIndex];
        
    }

    // Update is called once per frame
    void Update()
    {
        currentWeapon = Weapons[weaponIndex];
        if (Input.GetMouseButtonDown(0)) { currentWeapon.FireWeapon(); }
        if (Input.GetKeyDown(KeyCode.R)) { currentWeapon.Reload(); }
        if (Input.GetMouseButtonDown(2)) { cycleWeapon(); }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SGBox"))
            {
                Weapons.Add(GetComponent<ShotGun>());
                Destroy(other.gameObject);
            }

    }
    private void cycleWeapon()
    {
        weaponIndex++;
        if(weaponIndex>=Weapons.Count)
        {
            weaponIndex = 0;

        }
        currentWeapon = Weapons[weaponIndex];
    
    }
}
