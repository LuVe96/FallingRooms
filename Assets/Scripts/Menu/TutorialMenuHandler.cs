using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialMenuHandler : MonoBehaviour
{

    public TutorialItemContent[] tutorialItemContents;
    public GameObject tutorialItemPrafab;

    public Button backBatton;
    public Button lastButton;
    public Button nextButton;

    private int itemsPerSide = 3;
    private int currentPage = 0;
    private int maxPages;

    // Start is called before the first frame update
    void Start()
    {
        backBatton.onClick.AddListener(backButtonClicked);
        lastButton.onClick.AddListener(lastButtonClicked);
        nextButton.onClick.AddListener(nextButtonClicked);

        maxPages = (int)(tutorialItemContents.Length-1) / 3 ;
        updateButtons();
        updateItems();
    }

    private void updateButtons()
    {
        lastButton.gameObject.SetActive(currentPage > 0);
        nextButton.gameObject.SetActive(currentPage < maxPages);
    }

    private void updateItems()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var go = transform.GetChild(i).gameObject;
            if (go.name != "LNB" && go.name != "back") Destroy(go);
        }

        for (int i = currentPage*itemsPerSide; i < (currentPage+1) * itemsPerSide; i++)
        {
            if (i >= tutorialItemContents.Length) return;

            var ti = Instantiate(tutorialItemPrafab, transform);
            ti.transform.SetAsLastSibling();
            ti.GetComponent<TutorialItem>().Setup(tutorialItemContents[i]);
        }
    }

    private void nextButtonClicked()
    {
        currentPage++;
        updateItems();
        updateButtons();
    }

    private void lastButtonClicked()
    {
        currentPage--;
        updateItems();
        updateButtons();
    }

    private void backButtonClicked()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
