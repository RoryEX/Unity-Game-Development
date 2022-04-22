using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] float rotSpeed;
    [SerializeField] float moveSpeed;
    private Vector3 _startPosition;
    void Start()
    {
        _startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * rotSpeed * 5 * Time.deltaTime, Space.Self);
        transform.position = _startPosition + new Vector3(0f, Mathf.Sin(moveSpeed * Time.time), 0.0f);
    }

    void OnTriggerEnter(Collider objectCollider)
    {
        if(objectCollider.transform.tag == "Player")
        {
            GameManager.Gman.CoinsCount++;
            Destroy(gameObject,0.1f);
        }
    }
}
