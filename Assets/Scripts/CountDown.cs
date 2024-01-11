using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CountDown : MonoBehaviour
{
    [SerializeField] private TMP_Text counter;
    public bool countdownFinished = false;

    // Start is called before the first frame update
    void Start()
    {
        countdownFinished = false;
        StartCoroutine(ShowNewImage());
    }
    IEnumerator ShowNewImage()
    {
        int countdown = 5;
        while (countdown > 0)
        {
            counter.text = countdown.ToString();
            yield return new WaitForSeconds(1.3f);
            countdown--;
        }

        countdownFinished = true;
    }

    void Update()
    {
        if (countdownFinished)
        { 
            SceneManager.LoadScene("Test");
        }
        if (Input.GetKeyDown("escape"))
            Application.Quit();
    }
}
