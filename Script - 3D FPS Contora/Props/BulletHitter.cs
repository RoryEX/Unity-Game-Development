using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHitter : MonoBehaviour
{
    [SerializeField] float headCheckDistance = 0.35f;
    [SerializeField] LayerMask layerMask;



    // Update is called once per frame
    void OnCollisionEnter(Collision objectCollider)
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, headCheckDistance, layerMask))
        {
            Debug.Log(objectCollider.transform.name);
            hit.transform.gameObject.BroadcastMessage("OnHit", SendMessageOptions.DontRequireReceiver);
        }
    }
}
