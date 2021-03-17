using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookHandler : MonoBehaviour
{
    public AudioSource BookPage;
    private GameObject buttonNext;
    private GameObject buttonPrevious;
    public bool isReading;
    private int clueIndex = 0;

    public void Awake()
    {
        buttonNext = GameObject.Find("Clue-Book/Button_Next");
        buttonPrevious = GameObject.Find("Clue-Book/Button_Previous");
    }

    void Start()
    {
        patchVisibility();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            if (clueIndex > 0)
            {
                BookPage.Play();
                clueIndex--;
            }
        }
        else if (Input.GetKeyDown("e"))
        {
            BookPage.Play();
            clueIndex++;
        }

        loadPageContent();
        patchVisibility();
    }

    private void loadPageContent()
    {
        //todo grep all data and show based on index
        if (clueIndex > 0)
        {
            GameObject.Find("Clue-Book/Headline").GetComponent<Text>().text = "Heracles fight with the Nemean lion";
            GameObject.Find("Clue-Book/Author").GetComponent<Text>().text = "by Pieter Paul Rubens";
            GameObject.Find("Clue-Book/Description").GetComponent<Text>().text = "Heracles wandered the area until he came to the town of Cleonae. There he met a boy who said that if Heracles slew the Nemean lion and returned alive within 30 days, the town would sacrifice a lion to Zeus; but if he did not return within 30 days or he died, the boy would sacrifice himself to Zeus. Another version claims that he met Molorchos, a shepherd who had lost his son to the lion, saying that if he came back within 30 days, a ram would be sacrificed to Zeus. If he did not return within 30 days, it would be sacrificed to the dead Heracles as a mourning offering.";
            GameObject.Find("Clue-Book/Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Heracles_fight_with_the_nemean_lion");
        }
        else
        {
            GameObject.Find("Clue-Book/Headline").GetComponent<Text>().text = "Bust of Emperor Commodus";
            GameObject.Find("Clue-Book/Author").GetComponent<Text>().text = "";
            GameObject.Find("Clue-Book/Description").GetComponent<Text>().text = "Here, the Roman Emperor has taken on the guise of the mythological hero, __Heracles__. He has been given the attributes of the hero: the lion skin placed over his head, the club placed in his right hand, and the golden apples of Hesperides in his left. Each of these objects has been placed as a reminder of the hero's accomplishments, as well as allowing the Emperor to associate and refer to himself as the Roman Hercales.";
            GameObject.Find("Clue-Book/Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Buste_Of_Emperor_Commodus");
        }
        

    }

    private void patchVisibility()
    {
        buttonNext.SetActive(isReading);
        buttonPrevious.SetActive(isReading);
    }
}
