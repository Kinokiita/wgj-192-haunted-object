using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookHandler : MonoBehaviour
{
    public AudioSource BookPage;
    private GameObject buttonNext;
    private GameObject buttonPrevious;
    public bool isReading;

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
            BookPage.Play();
        }
        else if (Input.GetKeyDown("e"))
        {
            BookPage.Play();
        }
        patchVisibility();
    }

    private void patchVisibility()
    {
        buttonNext.SetActive(isReading);
        buttonPrevious.SetActive(isReading);
    }
}
