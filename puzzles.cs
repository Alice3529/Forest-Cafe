using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coffee.UIExtensions;

public class puzzles : MonoBehaviour
{
    int amount;
    int currentAmount;
    [SerializeField] GameObject puzzleCanvas;
    [SerializeField] GameObject picture2;
    ShelveManager shelveManager;
    float timeDisappear = 2f;

    private void Awake()
    {
        SetPuzzleIndex();
       
    }
    void Start()
    {
        shelveManager = FindObjectOfType<ShelveManager>();
        shelveManager.enviroment.SetActive(false);
        shelveManager.quit.SetActive(false);
        GameObject mainCanvas = GameObject.FindGameObjectWithTag("mainCanvas");
        mainCanvas.GetComponent<Canvas>().enabled=false;
        amount = transform.childCount;
    }


    public void ChangeAmount()
    {
        currentAmount += 1;
        if (amount == currentAmount)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.GetComponent<SoftMask>().softness = 0;
            }
            FindObjectOfType<IncAndDicButton>().gameObject.SetActive(false);
            StartCoroutine(Wait());
            currentAmount = -1;
            
        }
    }
    public void SetPuzzleIndex()
    {
        for (int i=0; i<transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<pieceScript>().puzzles = i;
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(timeDisappear);
        picture2.SetActive(false);
        foreach (Transform child in this.gameObject.transform)
        {
            child.gameObject.SetActive(false);
        }
        shelveManager.cooky.SetActive(true);
        puzzleCanvas.GetComponent<fadeEffect>().enabled = true;       

    }
}
