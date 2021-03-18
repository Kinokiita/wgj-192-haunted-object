using UnityEngine;
using UnityEngine.UI;

public class BookHandler : MonoBehaviour
{
    public AudioSource pageSound;
    public AudioSource codexClose;
    public AudioSource codexOpen;

    private GameObject buttonNext;
    private GameObject buttonPrevious;
    private const int clueSize = 2;
    private int clueIndex;
    private bool isSetup;

    private void OnEnable()
    {
        if (!isSetup)
        {
            buttonNext = GameObject.Find("Clue-Book/Button_Next");
            buttonPrevious = GameObject.Find("Clue-Book/Button_Previous");
            isSetup = true;
        }
    }

    private void setupPages()
    {
        if (clueIndex == 0)
        {
            GameObject.Find("Clue-Book/Headline").GetComponent<Text>().text = "Heracles fight with the Nemean lion";
            GameObject.Find("Clue-Book/Author").GetComponent<Text>().text = "by Pieter Paul Rubens";
            GameObject.Find("Clue-Book/Description").GetComponent<Text>().text =
                "Heracles wandered the area until he came to the town of Cleonae. There he met a boy who said that if Heracles slew the Nemean lion and returned alive within 30 days, the town would sacrifice a lion to Zeus; but if he did not return within 30 days or he died, the boy would sacrifice himself to Zeus. Another version claims that he met Molorchos, a shepherd who had lost his son to the lion, saying that if he came back within 30 days, a ram would be sacrificed to Zeus. If he did not return within 30 days, it would be sacrificed to the dead Heracles as a mourning offering.";
            GameObject.Find("Clue-Book/Image").GetComponent<Image>().sprite =
                Resources.Load<Sprite>("Images/Heracles_fight_with_the_nemean_lion");
        }
        else if (clueIndex == 1)
        {
            GameObject.Find("Clue-Book/Headline").GetComponent<Text>().text = "Bust of Emperor Commodus";
            GameObject.Find("Clue-Book/Author").GetComponent<Text>().text = "";
            GameObject.Find("Clue-Book/Description").GetComponent<Text>().text =
                "Here, the Roman Emperor has taken on the guise of the mythological hero, __Heracles__. He has been given the attributes of the hero: the lion skin placed over his head, the club placed in his right hand, and the golden apples of Hesperides in his left. Each of these objects has been placed as a reminder of the hero's accomplishments, as well as allowing the Emperor to associate and refer to himself as the Roman Hercales.";
            GameObject.Find("Clue-Book/Image").GetComponent<Image>().sprite =
                Resources.Load<Sprite>("Images/Buste_Of_Emperor_Commodus");
        }

        buttonNext.SetActive(clueIndex + 1 < clueSize);
        buttonPrevious.SetActive(clueIndex > 0);
    }

    public void toggleVisibility()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
            OnShow();
        }
        else
        {
            OnHide();
            gameObject.SetActive(false);
        }
    }

    private void OnShow()
    {
        codexOpen.Play();
        buttonNext.SetActive(true);
        buttonPrevious.SetActive(true);
        setupPages();
    }

    private void OnHide()
    {
        codexClose.Play();
        buttonNext.SetActive(false);
        buttonPrevious.SetActive(false);
        setupPages();
    }

    public void IncreasePage()
    {
        if (gameObject.activeSelf && clueIndex > 0)
        {
            clueIndex--;
            onPageChange();
        }
    }

    public void DecreasePage()
    {
        if (gameObject.activeSelf && clueIndex + 1 < clueSize)
        {
            clueIndex++;
            onPageChange();
        }
    }

    private void onPageChange()
    {
        if (gameObject.activeSelf)
        {
            pageSound.Play();
            setupPages();
        }
    }
}