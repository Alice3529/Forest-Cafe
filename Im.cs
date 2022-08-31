using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Im : MonoBehaviour
{
    public bool move = false;
    bool move1 = false;
    bool move2 = true;
    Transform endPosition;
    public int index;
    int indexBoom;
    Vector3 end;
    Vector3 startPos;
    Vector3 d;
    bool increase = false;
    Vector3 startScale;
    float a = 0;
    GameObject newBoard;
    bool c = false;
    bool x = false;
    bool ice1 = false;
    Column column;
    BoardManager manager;
    Transform parent;
    bool clearSound = false;
    bool isIce = false;

    private void Start()
    {
        indexBoom = index;
        manager=FindObjectOfType<BoardManager>();
        parent = this.gameObject.transform.parent;
    }

    void Update()
    {
        if (move == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition.position, Time.deltaTime * 1200);
            if (Mathf.Abs(Vector3.Distance(transform.position, endPosition.position)) <= 0.5f)
            {
                move = false;
                transform.SetParent(endPosition);
                transform.localPosition = Vector3.zero;

                if (c == true)
                {
                    column.EndMove();
                    c = false;
                }
                if (x == true)
                {
                    column.IncreasePrepared();
                    x = false;
                }
            }

        }
        if (move1 == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, end, Time.deltaTime * 300);
            if (Vector3.Distance(transform.position, end) <= 0.05)
            {
                move1 = false;
                move2 = false;
            }
        }

        if (move2 == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, Time.deltaTime * 300);
            if (Vector3.Distance(transform.position, startPos) <= 0.05)
            {
                move2 = true;
            }
        }

        if (increase == true)
        {
            DestroyCellChild();

        }
    }

    private void DestroyCellChild()
    {
        a += Time.deltaTime * 2f;
        float val = Mathf.Lerp(1, 1.07f, a);
        transform.localScale = new Vector3(val, val, val);
        if (val == 1.07f)
        {
            a = 0;
            transform.GetComponent<Image>().enabled = false;

            if (clearSound)
            {
                if (isIce) { music.Music.Ice(); }
                else { music.Music.BubbleSound(); }
            }

            if (ice1)
            {
                GameObject iceDestr=transform.parent.transform.GetChild(1).gameObject;
                iceDestr.transform.SetParent(null);
                Destroy(iceDestr);
                GameObject fragments = Instantiate(manager.iceFragments);
                foreach (Transform frag in fragments.transform)
                {
                    RectTransform rt = frag.GetComponent<RectTransform>();
                    frag.GetComponent<RectTransform>().sizeDelta = new Vector2(rt.sizeDelta.x * manager.offect, rt.sizeDelta.y * manager.offect);
                }
                fragments.GetComponent<RectTransform>().position = transform.parent.GetComponent<RectTransform>().position;
                fragments.transform.parent = FindObjectOfType<TileManager>().gameObject.transform;
                Destroy(fragments, 3f);
                transform.parent.GetComponent<Tile>().ice = false;
                ice1 = false;
            }
            FindObjectOfType<TileManager>().clearAmount += 1;
            increase = false;
        }
    }

    public void Des(bool ice)
    {
        ice1 = ice;
        increase = true;
    }

    public void SetClearSound(bool isIce1)
    {
        isIce = isIce1;
        clearSound = true;
    }

    public void Move(Transform transform, bool a, bool b, GameObject column1)
    {
        c = a;
        x = b;

        column = column1.GetComponent<Column>();
        move = true;
        endPosition = transform;
    }

    public void SlightMovement(Vector3 dir)
    {
        startScale = transform.localScale;
        d = dir / 2;
        startPos = transform.position;
        move1 = true;
        if (dir.y == 0)
        {
            end = (transform.position + dir / 6);
        }

        if (dir.x == 0)
        {
            end = (transform.position + dir / 15);

        }

    }

   
}
