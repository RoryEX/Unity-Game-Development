using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LVtoVictory : MonoBehaviour
{

    public class TutorialLevelJump : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.name == "Player")
            {
                SceneManager.LoadScene("VictoryScene");
            }
        }
    }
}
