using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public Text hintText;
    public GameObject dialogPanel;
    public string hint;
    public string finish;
    public GameObject Contoroller;
    public bool isShutDown;
    private void Update()
    {
        isShutDown = Contoroller.GetComponent<BoxInteraction>().shutDown;
        if (Input.GetKeyDown(KeyCode.Q)&&dialogPanel.activeSelf)
        {
            dialogPanel.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player"&&!isShutDown)
        {
            Debug.Log("Hint to player");
            dialogPanel.SetActive(true);
            hintText.text = hint;
        }
        else if(other.tag == "Player" && isShutDown)
        {
            dialogPanel.SetActive(true);
            hintText.text = finish;

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            dialogPanel.SetActive(false);
            
        }
        else if(other.tag == "Player" && isShutDown)
        {
            dialogPanel.SetActive(false);
            Destroy(this.gameObject, 2);
        }
    }
}
