using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform followObj;
    private Vector3 FollowVector;
    // Start is called before the first frame update
    void Start()
    {
        FollowVector = this.transform.position - followObj.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = followObj.position + FollowVector;
    }
}
