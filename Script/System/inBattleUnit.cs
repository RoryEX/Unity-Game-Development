using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inBattleUnit : MonoBehaviour
{
    [SerializeField] public BattleBaseUnit _base;
    [SerializeField] public int level;
    [SerializeField] bool isPlayerUnit;
    public float exp;
    public Units BattleUnit { get; set; }
    public void SetUp()
    {
        BattleUnit = new Units(_base, level);
        if (isPlayerUnit)
        {
            GetComponent<Image>().sprite = BattleUnit.Base.PlayerSprite;
            
        }
        else
        {
            GetComponent<Image>().sprite = BattleUnit.Base.EnemySprite;
            
        }
    }

    
}
