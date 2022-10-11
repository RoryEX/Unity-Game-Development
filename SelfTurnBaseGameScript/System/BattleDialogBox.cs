using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleDialogBox : MonoBehaviour
{
    [SerializeField] Text dialogText;
    [SerializeField] int lettersPerSecond;
    [SerializeField] GameObject selectorPanel;
    [SerializeField] GameObject skillSelector;
    [SerializeField] GameObject itemBox;

    [SerializeField] List<Text> skillTexts;
    public void SetDialog(string dialog)
    {
        dialogText.text = dialog;
    }

    public IEnumerator TypeDialog(string dialog)
    {
        dialogText.text = "";
        foreach(var letter in dialog.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f/ lettersPerSecond);
        }
    }

    public void EnableDialogText(bool enabled)
    {
        dialogText.enabled = enabled;
    }
    public void EnableSelector(bool enabled)
    {
        selectorPanel.SetActive(enabled);
    }
    public void EnableSkillSelector(bool enabled)
    {
        skillSelector.SetActive(enabled);
    }
    public void EnableItemBox(bool enabled)
    {
        itemBox.SetActive(enabled);
    }

    public void SetSkillNames(List<Skills> skills)
    {
        for(int i = 0; i < skills.Count; i++)
        {
            if (i < skills.Count)
            {
                skillTexts[i].text = skills[i].BaseSkill.Name;
            }
            else
            {
                skillTexts[i].text = "--";
            }
        }
    }
}
