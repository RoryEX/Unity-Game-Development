using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseCombat : MonoBehaviour
{

    [SerializeField] private Transform weaponTransform = null;
    [SerializeField] private float maxRayDistance = 100.0f;
    [SerializeField] private LayerMask weaponMask;

    private GameObject combatObject = null;
    [SerializeField] private float pushForce = 15.0f;
    private int hitTimes = 0;

    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Punch();
            Debug.Log("Punch out!");
        }

    }
    // Update is called once per frame
    void Punch()
    {
            RaycastHit hit;

            Ray ray = new Ray(weaponTransform.position, weaponTransform.forward);

            if (Physics.Raycast(ray, out hit, maxRayDistance, weaponMask))
            {
                //combatObject = hit.transform.gameObject;
                Debug.Log("Hit");
                if (hit.transform.gameObject.GetComponent<Rigidbody>())
                {
                    hit.transform.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                    hit.transform.gameObject.GetComponent<Rigidbody>().AddForce(weaponTransform.forward * pushForce, ForceMode.Impulse);
                    hitTimes++;
                }

                if (hitTimes == 3) 
                {
                    Destroy(hit.transform.gameObject);
                    hitTimes = 0;
                }
                
                Debug.Log("Close Combat hit!");
            }
        
        Debug.DrawRay(weaponTransform.position, weaponTransform.forward * maxRayDistance, Color.red, 2.5f);
    }
}

