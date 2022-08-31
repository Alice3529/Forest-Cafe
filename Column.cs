using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour
{
    int p = 0;
    List<GameObject> pictures;
    List<GameObject> objWhichMove=new List<GameObject>();
    int l = 0;
    public int prepared = 0;
    BoardManager boardManager;
    TileManager tileManager;

    private void Start()
    {
        tileManager = FindObjectOfType<TileManager>();
        pictures = tileManager.pictures;
        boardManager = FindObjectOfType<BoardManager>();
    }

    public void IncreasePrepared()
    {
        prepared += 1;
    }

    public void ChangeElements()
    {
        int counter = -1;
        int indexOfLastElement = 0;
        bool can = false;
        p = 0;


        for (int i = 0; i < transform.childCount; i++)
        {
            Transform el = transform.GetChild(i);
            counter += 1;
            if (el.childCount == 0)
            {
                p += 1;

            }
            if (el.childCount == 0 && can==false)
            {
                indexOfLastElement = counter;
                can = true;
            }

        }

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform el = transform.GetChild(i);
            if (i > indexOfLastElement && transform.GetChild(i).childCount > 0)
            {
                Im im1 = el.GetChild(0).GetComponent<Im>();
                im1.transform.SetParent(tileManager.transform);
                im1.Move(transform.GetChild(indexOfLastElement), false, true, gameObject);
                objWhichMove.Add(el.gameObject);
                indexOfLastElement++;
            }

        }

        if (p==0)
        {
            tileManager.CheckNoAdd();
        }
        else
        {
            StartCoroutine(M());

        }

    }

    public void EndMove()
    {
        l += 1;
        if (l == p)
        {
            Reset1();
            tileManager.NewWasAdd();

        }
    }

    IEnumerator M()
    {
        yield return new WaitForEndOfFrame();
        if (prepared == objWhichMove.Count)
        {
            prepared = 0;
            objWhichMove.Clear();
            StartCoroutine(CreateNewElements());
            StopCoroutine(M());
        }
        else
        {
            StartCoroutine(M());
        }

    }
    IEnumerator CreateNewElements()
    {
        if (p > 0)
        {
            tileManager.newWasAdded = true;
        } 
        for (int i=0; i < p; i++)
        {
            int inA=UnityEngine.Random.Range(0, pictures.Count);
            GameObject pic=Instantiate(pictures[inA], transform.GetChild(transform.childCount - 1).position, Quaternion.identity);
            pic.GetComponent<RectTransform>().sizeDelta = new Vector2(boardManager.xOffset, boardManager.yOffset);
            pic.transform.SetParent(tileManager.transform);
            pic.GetComponent<Im>().Move(FindFirstEmpty(),true, false, gameObject);
            yield return new WaitForSeconds(0.2f);
        }
    }

    void Reset1()
    {
        l = 0;
        p = 0;
        foreach (Transform child in transform)
        {
            Tile tile = child.GetComponent<Tile>();
            tile.engaged = false;
            tile.marked = false;
        }
    }

    private Transform FindFirstEmpty()
    {
        Transform a = null;
        for (int i=0; i<transform.childCount; i++)
        {
            Transform nextChild = transform.GetChild(i);
            if (nextChild.childCount == 0 && nextChild.GetComponent<Tile>().engaged == false)
            {
                nextChild.GetComponent<Tile>().engaged = true;
                a = nextChild;
                break;
            }
        }
        if (a == null)
        {
            Debug.LogError("Null cell");
        }
        return a;
    }

    public void Stop()
    {
        tileManager.canClick = false;
    }
}
