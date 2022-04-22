using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongNote : MonoBehaviour
{
    public bool canBePressed;
    public KeyCode keyToPress;
    public GameObject hitEffect, goodEffect, perfectEffect, missEffect;
    //public float timerHolder;
    public SpriteRenderer longNotes;
    public Transform effectTransform;
    private bool allowRlease;
    public float distanceToButton;
    public float selfOffset;
    // Start is called before the first frame update
    void Start()
    {
        longNotes = this.GetComponent<SpriteRenderer>();
        allowRlease = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(keyToPress))
        {
            
            if (canBePressed)
            {
                //timerHolder -= Time.deltaTime;
                longNotes.color = Color.green;
                allowRlease = true;
                distanceToButton = (transform.position.y + selfOffset) - effectTransform.position.y;
            }
        }
        if (Input.GetKeyUp(keyToPress)&& allowRlease)
        {

            longNotes.color = Color.white;

            if (distanceToButton > 0.65f)
            {
                Debug.Log("Missed");
                GameManager.instance.NoteMiss();
                Instantiate(missEffect, effectTransform.position, perfectEffect.transform.rotation);
                gameObject.SetActive(false);
            }
            else if (distanceToButton > 0.35f)
            {
                Debug.Log("Bad Hit");
                GameManager.instance.BadHit();
                Instantiate(hitEffect, effectTransform.position, hitEffect.transform.rotation);
                gameObject.SetActive(false);

            }
            else if (distanceToButton > 0.15)
            {
                Debug.Log("Good Hit");
                GameManager.instance.GoodHit();
                Instantiate(goodEffect, effectTransform.position, goodEffect.transform.rotation);
                gameObject.SetActive(false);
            }
            else if (distanceToButton >= 0.05)
            {
                Debug.Log("Perfect Hit");
                GameManager.instance.PerfectHit();
                Instantiate(perfectEffect, effectTransform.position, perfectEffect.transform.rotation);
                gameObject.SetActive(false);
            }
            /*if (timerHolder > 0.65f)
            {
                Debug.Log("Missed");
                GameManager.instance.NoteMiss();
                Instantiate(missEffect, effectTransform.position, perfectEffect.transform.rotation);
                gameObject.SetActive(false);
            }
            else if (timerHolder > 0.35f)
            {
                Debug.Log("Bad Hit");
                GameManager.instance.BadHit();
                Instantiate(hitEffect, effectTransform.position, hitEffect.transform.rotation);
                gameObject.SetActive(false);
              
            }
            else if(timerHolder > 0.15)
            {
                Debug.Log("Good Hit");
                GameManager.instance.GoodHit();
                Instantiate(goodEffect, effectTransform.position, goodEffect.transform.rotation);
                gameObject.SetActive(false);
            }
            else if(timerHolder>=0.05)
            {
                Debug.Log("Perfect Hit");
                GameManager.instance.PerfectHit();
                Instantiate(perfectEffect, effectTransform.position, perfectEffect.transform.rotation);
                gameObject.SetActive(false);
            }*/
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Activator")
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
