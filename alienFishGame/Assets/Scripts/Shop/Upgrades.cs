using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour
{
    public int price;
    public int basePrice;
    public int multiplier;
    public string upgradeType;
    public string upgradeDescription;
    public GameObject image;
    
    public int currentLevel = 1;
    public int maxLevel;
    public GameObject soldOverlay;

    public purchaseMenu purchaseMenu;

    public bool bossUnlocked;

    public FMODUnity.EventReference clickEvent;
    public FMODUnity.EventReference hoverEvent;

    public Sprite suspiciousSprite;

    void Start()
    {
    
    }

    // checks for the ultimate bait upgrade
    void Update()
    {
        if (upgradeType == "bait" && maxLevel == 4 && currentLevel == 3 && !bossUnlocked)
        {
            image.GetComponent<Image>().sprite = suspiciousSprite;
            upgradeDescription = "A mysterious aura emanates from the bait. You wonder what new things you might fish up with this";
            price = 3000;
            bgmScript.instance.SetParameter(8);

            // makes sure this is only done once
            bossUnlocked = true;
        }
    }
    
    public void OnHoverEnter()
    {
        LeanTween.scale(image, new Vector3(0.9f, 0.9f, 0.9f), 0.1f);
        FMODUnity.RuntimeManager.PlayOneShot(hoverEvent, transform.position);
    }

    public void OnHoverExit()
    {
        LeanTween.scale(image, new Vector3(1, 1 , 1), 0.1f);
    }

    public void OnClick()
    {
        FMODUnity.RuntimeManager.PlayOneShot(clickEvent, transform.position);
        purchaseMenu.transform.localScale = new Vector3(0, 0, 0);
        purchaseMenu.gameObject.SetActive(true);
        LeanTween.scale(purchaseMenu.gameObject, new Vector3(1, 1, 1), 0.15f);

        purchaseMenu.UpdatePurchaseInfo(upgradeType, price, upgradeDescription);
    }

    public void LoadData(SaveData saveData)
    {
        foreach (var upgrade in saveData.upgradeLevels)
        {
            if (upgrade.type == upgradeType)
            {
                currentLevel = upgrade.currentLevel;
            }
        }

        price = basePrice * (int)(Mathf.Pow(multiplier, currentLevel - 1));
        Debug.Log("on load data, " + upgradeType + "price is " + price.ToString());

        if (currentLevel < maxLevel)
        {
            this.enabled = true;
            GetComponent<EventTrigger>().enabled = true;
        }
        else
        {
            GetComponent<EventTrigger>().enabled = false;
            soldOverlay.SetActive(true);
        }
    }

    public void UnlockBoss()
    {
        maxLevel = 4;
        this.enabled = true;
        gameObject.GetComponent<EventTrigger>().enabled = true;
        soldOverlay.SetActive(false);
    }
}
