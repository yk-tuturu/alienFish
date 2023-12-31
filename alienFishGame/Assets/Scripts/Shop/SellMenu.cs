using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellMenu : MonoBehaviour
{
    public GameObject sellFishIcon;
    public Transform sellPanel;
    public GameObject blankIcon;

    public FMODUnity.EventReference uiDeniedEvent;
    public FMODUnity.EventReference sellAllEvent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSellInfo()
    {
        // clear out icons from previous run
        foreach (Transform child in sellPanel)
        {
            GameObject.Destroy(child.gameObject);
        }

        var iconCounter = 0;
        for (var i = 0; i < FishDataManager.instance.fishTypeCount; i++)
        {
            Fish fish = FishDataManager.instance.GetFish(i);
            if (fish.type == "boss")
            {
                continue;
            }
            
            if (fish.totalCaught - fish.totalSold > 0)
            {
                GameObject icon = Instantiate(sellFishIcon, new Vector3(0, 0, 0), Quaternion.identity, sellPanel);
                icon.GetComponent<SellFishIcon>().index = i;
                icon.GetComponent<SellFishIcon>().UpdateFishDisplayed();
                iconCounter += 1;
            }
        }

        // hacky fix for the scrollbar
        for (var i = 0; i < 12 - iconCounter; i++)
        {
            GameObject icon = Instantiate(blankIcon, new Vector3(0, 0, 0), Quaternion.identity, sellPanel);
        }

        // fix for the scroll positioning
        if (iconCounter >= 13)
        {
            var sellRect = sellPanel.GetComponent<RectTransform>();
            var ogPos = sellRect.anchoredPosition;
            sellRect.anchoredPosition = new Vector2(ogPos.x, ogPos.y - 200);
        }
    }

    public void SellAll()
    {       
        var counter = 0;
        for (var i = 0; i < FishDataManager.instance.fishTypeCount; i++)
        {
            Fish fish = FishDataManager.instance.GetFish(i);
            if (fish.type == "boss")
            {
                continue;
            }
            
            if (fish.totalCaught - fish.totalSold != 0)
            {
                counter += 1;
                FishDataManager.instance.SellFish(i, fish.totalCaught - fish.totalSold);
            }
        }
        UpdateSellInfo();
        if (counter != 0)
        {
            FMODUnity.RuntimeManager.PlayOneShot(sellAllEvent);
        }
        else
        {
            FMODUnity.RuntimeManager.PlayOneShot(uiDeniedEvent);
        }
    }
}
