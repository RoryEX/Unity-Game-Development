using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Text Name;
    [SerializeField] Text Level;
    [SerializeField] Slider hpBar;
    [SerializeField] Slider mpBar;
    

    Units _unit;
    
    public void setdata(Units unitBase)
    {
        _unit = unitBase;
        Name.text = unitBase.Base.name;
        Level.text = "Lv" + unitBase.level;
        hpBar.maxValue = unitBase.MaxHP;
        hpBar.value = unitBase.currentHP;
        mpBar.maxValue = unitBase.MaxMp;
        mpBar.value = unitBase.currentMP;
        
    }

    public void UpdateHP()
    {
        hpBar.value = _unit.currentHP;
    }
    public void UpadteMP()
    {
        mpBar.value = _unit.currentMP;
    }
    
}
