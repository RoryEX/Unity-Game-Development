using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField]
    private GameObject grenade = null;
    [SerializeField]
    private Transform ThrowLocation = null;
    [SerializeField]  private int grenadeCount = 3;
    [SerializeField]
    private float force = 0;
    [SerializeField] LayerMask weaponMask;
    [SerializeField] float explosionSphere = 10.0f;
    //[SerializeField] float explosionOutRage = 15.0f;
    void Start()
    {
        //grenade = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        grenade.name = "Grenade";
        //grenade.AddComponent<Rigidbody>();
        grenade.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        grenade.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (grenadeCount > 0)
        { 
            if (Input.GetKeyDown(KeyCode.G))
            {
                ThrowGrenade();
                grenadeCount--;
            }
        }
    }
    void ThrowGrenade()
    {

        GameObject clone = Instantiate(grenade, ThrowLocation.position + ThrowLocation.forward, transform.rotation);
        clone.GetComponent<Collider>().enabled = false;
        clone.SetActive(true);
        clone.GetComponent<Rigidbody>().AddForce((Vector3.up + ThrowLocation.forward) * force, ForceMode.Impulse);

        Collider[] overlappedColliders = Physics.OverlapSphere(clone.transform.position, explosionSphere, weaponMask);

        List<GameObject> explodeObjects = new List<GameObject>();
        for (int i = 0; i < overlappedColliders.Length; i++)
        {
            foreach (Rigidbody rig in overlappedColliders[i].GetComponentsInChildren<Rigidbody>())
            {
                Vector3 moveDirection = (clone.transform.position - rig.transform.position).normalized;
                rig.velocity = moveDirection * explosionSphere;
                rig.useGravity = false;
            }

            foreach (Collider collider in overlappedColliders[i].GetComponentsInChildren<Collider>())
            {
                collider.enabled = false;
            }
            explodeObjects.Add(overlappedColliders[i].gameObject);
        }
        float objectDistance;
        while (explodeObjects.Count > 0)
        {
            for (int i = 0; i < explodeObjects.Count; i++)
            {
                if (explodeObjects[i] != null)
                {
                    objectDistance = Vector3.Distance(explodeObjects[i].transform.position, clone.transform.position);

                    if (objectDistance > 3.0f)
                    {
                        Destroy(explodeObjects[i]);
                        explodeObjects.RemoveAt(i);
                    }

                }
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Grenade"))
        {
            grenadeCount++;
            Debug.Log("Refill Grenade!");
            Destroy(other.gameObject);
        }
    }
}
