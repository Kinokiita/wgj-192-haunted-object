using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookTriggerScript : MonoBehaviour
{

    public GameObject codexBook;
    public bool isShowing;
    public AudioSource codexClose;
    public AudioSource codexOpen;


    void Start()
    {
        codexBook.SetActive(isShowing);
        //load first 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("i"))
        {
            isShowing = !isShowing;
            codexBook.SetActive(isShowing);
            codexBook.GetComponent<BookHandler>().isReading = true;

            if (isShowing)
            {
                codexOpen.Play();
            }
            else
            {
                codexClose.Play();
            }
        }
    }
}
