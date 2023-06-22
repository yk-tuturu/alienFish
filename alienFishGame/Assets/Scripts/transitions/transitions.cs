using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class transitions : MonoBehaviour
{
    public GameObject gameCamera;
    public GameObject panelPrefab;
    public GameObject finalRect;
    public GameObject blackOutPanel;
    // Start is called before the first frame update
    void Start()
    {
        FadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            transitionToBossFight();
        }
    }

    public void transitionToBossFight()
    {
        StartCoroutine(transition());
    }

    IEnumerator transition()
    {
        // GameObject.Find("ShopScreen").SetActive(false);
        iTween.ShakePosition(gameCamera, new Vector3(20, 20, 20), 6f);

        yield return new WaitForSeconds(1f);

        GameObject panel = Instantiate(panelPrefab, new Vector3(0, 0, 0), Quaternion.identity, this.transform);
        var rect = panel.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector3(0, 0, 0);

        finalRect = panel;

        iTween.ValueTo(panel, iTween.Hash("from", 0.1f, "to", 0.95f, "time", 0.6f, "onupdate", "updateColor", "onupdatetarget", this.gameObject));

        yield return new WaitForSeconds(0.8f);

        iTween.ValueTo(panel, iTween.Hash("from", 0.95f, "to", 0f, "time", 0.6f, "delay", 0.1f, "onupdate", "updateColor", "onupdatetarget", this.gameObject));

        yield return new WaitForSeconds(0.8f);

        iTween.ValueTo(panel, iTween.Hash("from", 0f, "to", 0.95f, "time", 0.6f, "delay", 0.1f, "onupdate", "updateColor", "onupdatetarget", this.gameObject));

        yield return new WaitForSeconds(0.8f);

        iTween.ValueTo(panel, iTween.Hash("from", 0.95f, "to", 0f, "time", 0.6f, "delay", 0.1f, "onupdate", "updateColor", "onupdatetarget", this.gameObject));

        yield return new WaitForSeconds(1.1f);

        iTween.ValueTo(panel, iTween.Hash("from", 0f, "to", 1f, "time", 1.2f, "delay", 0.1f, "onupdate", "updateColor", "onupdatetarget", this.gameObject, "oncomplete", "changeScene", "oncompletetarget", this.gameObject));
        // rect.localScale = new Vector3(0, 1, 1);
        // iTween.ScaleTo(panel, iTween.Hash("x", 1f, "time", 1f, "delay", 0.3f));

        // yield return new WaitForSeconds(1.1f);

        // GameObject panel1 = Instantiate(panelPrefab, new Vector3(0, 0, 0), Quaternion.identity, this.transform);
        // var rect1 = panel1.GetComponent<RectTransform>();
        // rect1.anchoredPosition = new Vector3(0, 0, 0);
        // rect1.localScale = new Vector3(0, 1, 1);
        // iTween.ScaleTo(panel1, iTween.Hash("x", 1f, "time", 1f, "delay", 0.1f));

        // yield return new WaitForSeconds(1.1f);

        // GameObject panel2 = Instantiate(panelPrefab, new Vector3(0, 0, 0), Quaternion.identity, this.transform);
        // finalRect = panel2;
        // var rect2 = panel2.GetComponent<RectTransform>();
        // rect2.anchoredPosition = new Vector3(0, 0, 0);
        // rect2.localScale = new Vector3(0, 1, 1);
        // iTween.ScaleTo(panel2, iTween.Hash("x", 1f, "time", 1f, "delay", 0.1f));

        
    }

    public void updateColor(float val)
    {
        Debug.Log(val.ToString());
        Image image = finalRect.GetComponent<Image>();
        image.color = new Color(1, 1, 1, val);
    }

    public void changeScene()
    {
        SceneManager.LoadScene("Scenes/fishOverlord");
    }

    public void FadeIn()
    {
        iTween.ValueTo(blackOutPanel, iTween.Hash("from", 1f, "to", 0f, "time", 1f, "onupdate", "updateColorBlack", "onupdatetarget", this.gameObject));
    }

    void updateColorBlack(float val)
    {
        Debug.Log(val.ToString());
        Image image = blackOutPanel.GetComponent<Image>();
        image.color = new Color(0, 0, 0, val);
    }
}