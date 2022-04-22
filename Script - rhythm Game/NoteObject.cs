using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;
    public KeyCode keyToPress;
    public GameObject hitEffect,goodEffect,perfectEffect,missEffect;
    public Transform effectTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if (canBePressed)
            {
                gameObject.SetActive(false);

                //GameManager.instance.NoteHit();
                if (Mathf.Abs(transform.position.y) > 0.25f)
                {
                    Debug.Log("Bad Hit");
                    GameManager.instance.BadHit();
                    Instantiate(hitEffect, effectTransform.position, hitEffect.transform.rotation);
                }  
                else if (Mathf.Abs(transform.position.y) > 0.05f)
                {
                    Debug.Log("Good Hit");
                    GameManager.instance.GoodHit();
                    Instantiate(goodEffect, effectTransform.position, goodEffect.transform.rotation);
                }
                else
                {
                    Debug.Log("Perfect Hit");
                    GameManager.instance.PerfectHit();
                    Instantiate(perfectEffect, effectTransform.position, perfectEffect.transform.rotation);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="Activator")
        {
            canBePressed = true;
        }  
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Activator" && gameObject.activeSelf)//On the Unity later on version, exit2d works differently ways. So here need a bool to control(This one is the simplest one)(some different ways also fine)
        {
            canBePressed = false;
            GameManager.instance.NoteMiss();
            Instantiate(missEffect, effectTransform.position, missEffect.transform.rotation);
        }
    }
}
