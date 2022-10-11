using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hookM : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] hookSpawn;
    public enum Direction { Right, Left ,Up , Down};
    public Direction direction;
    public float spawnDelay = 2;
    void Start()
    {
        InvokeRepeating("hooksp", 3, spawnDelay);
    }

    // Update is called once per frame
    void hooksp()
    {
        int randomInt = Random.Range(0, hookSpawn.Length - 1);
        
        GameObject hook = Instantiate(hookSpawn[randomInt], transform.position, transform.rotation);
        if (direction == Direction.Left)
        {
            transform.eulerAngles = new Vector3(0, 0, 90);
        }
        if (direction == Direction.Right)
        {
            hook.transform.eulerAngles = new Vector3(0, 0, 270);
        }
        if (direction == Direction.Up) {
            hook.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        if (direction == Direction.Down)
        {
            hook.transform.localEulerAngles = new Vector3(0, 0, 180);
        }
        hook.GetComponent<hookmover>().isActive = true;
        Destroy(hook, 5);
    }
}
