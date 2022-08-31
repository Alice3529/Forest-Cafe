using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    float currentAmount = 0;
    int maxAmount;
    float m;
    float newAm;
    float oldM=0;
    int p = 0;
    bool g = false;
    ShelveManager manager;
    float speed = 10;
    BoardManager boardManager;
    float waitDestroyBoard = 1f;

    private void Start()
    {
        manager = FindObjectOfType<ShelveManager>();
        boardManager = FindObjectOfType<BoardManager>();
        maxAmount = boardManager.maxAmount;
    }
    public void IncreaseAmount(int am)
    {
        p += am;
        g = true;
    }

    void Update()
    {
        if (g == true)
        {
            FillBar();

            if (currentAmount == newAm && p != 0)
            {
                Reset1();
            }

            if (currentAmount == newAm && p == 0)
            {
                g = false;
            }

        }
    }

    private void FillBar()
    {
        m += speed * Time.deltaTime;
        currentAmount = Mathf.Lerp(oldM, newAm, m);
        Image im = GetComponent<Image>();
        im.fillAmount = currentAmount / maxAmount;
    }

    private void Reset1()
    {
        currentAmount = 0;
        oldM = newAm;
        newAm += 1;
        m = 0;
        p -= 1;
    }

    public bool Check()
    {
        if (currentAmount >= maxAmount)
        {
            StartCoroutine(Wait());
            return true;
        }
        return false;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitDestroyBoard);
        Column[] columns = FindObjectsOfType<Column>();
        foreach (Column col in columns)
        {
            col.Stop();
        }
        manager.ActiveEnviroment();
        manager.icecream.SetActive(true);
        manager.EndActions();
        boardManager.Stop();
    }
}
