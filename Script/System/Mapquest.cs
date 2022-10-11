using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mapquest : MonoBehaviour
{
    List<MonsterInteraction> monsters;
    [SerializeField] NPCInteraction NPC1;
    private void Update()
    {
        for(int i = 0; i < monsters.Count; i++)
        {
            if (monsters[i] != null)
            {
                NPC1.finished = false;
            }
            else
            {
                NPC1.finished = true;
            }
        }
        
    }
}
