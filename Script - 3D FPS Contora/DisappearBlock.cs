using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearBlock : MonoBehaviour
{
    [SerializeField] private float DisappearTimer = 1;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name == "Player")
        {
            Destroy(this.gameObject, DisappearTimer);
        }
    }
}
