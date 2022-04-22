using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]protected Transform spPosition = null;
    [SerializeField]protected GameObject Enemy = null;
    [SerializeField]protected int enemyAmount = 3;
    
    float offsetX = 0.7f;
    float offsetZ = 0.3f;
    
    private void OnCollisionEnter(Collision collision)
    {

        float spwnPosX = spPosition.position.x + offsetX;
        float spwnPoxZ = spPosition.position.z + offsetZ;
        Vector3 SpwnPos = new Vector3(spwnPosX,spPosition.position.y, spwnPoxZ);
        if (collision.transform.name == "Player")
        {
            for (int i = 0; i < enemyAmount; i++) 
            {  
                GameObject enemy = Instantiate(Enemy, SpwnPos, spPosition.rotation);
                SpwnPos.x += offsetX;
                SpwnPos.z += offsetZ;
            } 
        }
        Debug.Log("Enemy incomming!");
        Destroy(this.gameObject);
        Destroy(spPosition.gameObject);
    }
}
