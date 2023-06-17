using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;

public class settingsMenu : MonoBehaviour
{
    public float autosaveTime = 2f;
    private float timer;

    public List<TextMeshProUGUI> saveDates = new List<TextMeshProUGUI>();
    public List<Image> saveImages = new List<Image>();

    public Sprite bgImage;

    // Start is called before the first frame update
    void Start()
    {
        bgImage = Resources.Load<Sprite>("placeholderSprites/background");

        // fetch previous saves
        FetchSaves();

    }

    // Update is called once per frame
    void Update()
    {
        // autosaves
        timer += Time.deltaTime;
        if (timer >= autosaveTime)
        {
            Save(0);
            timer = 0;
        }
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Save(int index)
    {
        SaveSystem.instance.Save(index);
        saveDates[index].text = System.DateTime.UtcNow.ToLocalTime().ToString("dd-MM-yyyy  HH:mm");
        saveImages[index].sprite = bgImage;
    }

    public void FetchSaves()
    {
        string savePath = SaveSystem.instance.saveFolder;
        if (Directory.Exists(savePath))
        {
            string[] files = Directory.GetFiles(savePath);
            foreach (var file in files)
            {
                string jsonString = File.ReadAllText(file);
                SaveData saveData = JsonUtility.FromJson<SaveData>(jsonString);
                saveDates[saveData.saveIndex].text = saveData.date;
                saveImages[saveData.saveIndex].sprite = bgImage;
            }
        }
    }
}
