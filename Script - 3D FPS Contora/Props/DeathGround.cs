using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathGround : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.transform.name == "Player")
        {
            Destroy(gameObject);
            SceneManager.LoadScene("Level1");
        }
        
    }
}
