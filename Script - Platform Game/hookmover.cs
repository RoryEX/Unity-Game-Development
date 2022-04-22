using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hookmover : MonoBehaviour
{
    // Start is called before the first frame update
    public enum Direction { Right, Left, Up, Down };
    public Direction direction;
    public float speed = 2.5f;
    [HideInInspector]
    public bool isActive = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive == true)
        {
            if(direction==Direction.Right)
            transform.position += Vector3.right * speed * Time.deltaTime;
            if (direction == Direction.Left)
            transform.position += Vector3.left * speed * Time.deltaTime;
            if (direction == Direction.Up)
            transform.position += Vector3.up * speed * Time.deltaTime;
            if (direction == Direction.Down)
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
    }
}
