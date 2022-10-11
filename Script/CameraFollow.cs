using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float followSpeed = 2.0f;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    [SerializeField]
    public Transform playerTransform;
    private Vector2 targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = playerTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform != null)
        {
            //targetPosition.x = Mathf.Clamp(playerTransform.position.x, minX, maxX);
            // targetPosition.y = Mathf.Clamp(playerTransform.position.y, minY, maxY);
            //transform.position = Vector2.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
            transform.position = playerTransform.position;
        }
    }
}
