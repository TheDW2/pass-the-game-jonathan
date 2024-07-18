using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField]
    GameObject panel;

    [SerializeField]
    TextMeshProUGUI npcCostText;
    int npcLevel = 1;
    int npcCost = 5;

    [SerializeField]
    TextMeshProUGUI damageCostText;
    int damageLevel = 1;
    int damageCost = 1;

    [SerializeField]
    List<GameObject> purchaseableNPCs = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("enter");
        if (panel.activeSelf) return;
        ToggleMenu();
    }

    public void ToggleMenu()
    {
        Debug.Log("upgrades people upgrades");
        GameManger.instance.LockPlayer();
        panel.SetActive(!panel.activeSelf);
    }

    public void PurchaseNPC()
    {
        if (npcLevel > purchaseableNPCs.Count || ScoreManager.instance.GetScore() < damageCost) return;

        purchaseableNPCs[npcLevel - 1].SetActive(true);
        ScoreManager.instance.RemoveScore(npcCost);
        npcLevel++;
        npcCost = (int)(5 * Mathf.Pow(1.4f, npcLevel));


        npcCostText.text = npcCost.ToString();

        if (npcLevel <= purchaseableNPCs.Count) return;

        npcCostText.text = "MAX";
        
    }

    public void UpgradeDamage()
    {
        if (ScoreManager.instance.GetScore() < damageCost) return;

        GameManger.instance.player.IncreaseDamage(1);
        ScoreManager.instance.RemoveScore(damageCost);
        damageLevel++;
        damageCost = (int)(1 * Mathf.Pow(1.5f, damageLevel));
        damageCostText.text = damageCost.ToString();
    }
}
