using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hook : MonoBehaviour
{
    // Start is called before the first frame update
    public void Start()
    {
        Destroy(this.gameObject, 3.0f);
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
            SceneManager.LoadScene("SampleScene");
        }
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Shield")
        {
            Destroy(this.gameObject);
        }
        
    }
}
