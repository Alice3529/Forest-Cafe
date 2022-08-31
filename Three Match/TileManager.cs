using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    GameObject lastclick;
    GameObject newClick;
    public GameObject matchCanvas;
    bool move = false;
    GameObject imLast;
    GameObject imNew;
    Vector3 startPos;
    Vector3 endPos;
    int m = 0;
    bool ch = true;
    Dictionary<Vector2, GameObject> tiles;
    public List<GameObject> sequenceObjects = new List<GameObject>();
    List<float> columnIndex = new List<float>();
    public List<GameObject> pictures = new List<GameObject>();
    public List<GameObject> objectsToRemove = new List<GameObject>();
    public bool newWasAdded = false;
    GameObject board;
    public bool canClick = false;
    BoardManager manager;
    int ansColumns;
    List<GameObject> iceTiles = new List<GameObject>();
    int columnsWasAdd = 0;
    int b = 0;
    int iceAmount;
    List<GameObject> iceTilestoAdd = new List<GameObject>();
    public int clearAmount=0;
    int taskAmount=0;
    bool checkToChangeElements = false;
    float val = 0;
    bool wasClick = false;


    IEnumerator Start()
    {
        manager = FindObjectOfType<BoardManager>();
        tiles = manager.tiles;
        yield return new WaitForSeconds(1f);
        ch = false;

    }

    public void SetLastClicked(GameObject newClick1)
    {
        if (wasClick == true) { return; }
        if (canClick == false) { return; }
        if (newClick1.GetComponent<Tile>().ice==true) {
            return;
        }
        newClick = newClick1;
        if (lastclick == null)
        {
            lastclick = newClick;

        }

        else if (lastclick != null)
        {
            bool isNeighbour = manager.IsNeighbour(lastclick, newClick);
            if (!isNeighbour)
            {
                lastclick = newClick;

            }
            else if (isNeighbour)
            {
                Reset1();
                music.Music.Swap();
                if (IsSequence(newClick))
                {
                    Swap();
                }
                else if (IsSequence(lastclick))
                {
                    Swap();
                }
                else 
                {
                    lastclick.transform.GetChild(0).GetComponent<Im>().SlightMovement((newClick.transform.position-lastclick.transform.position));
                    newClick.transform.GetChild(0).GetComponent<Im>().SlightMovement((lastclick.transform.position - newClick.transform.position));
                    lastclick = null;
                }
            }
        }
    }

    private void Reset1()
    {
        sequenceObjects.Clear();
        m = 0;
        ansColumns = 0;
    }

    private bool IsSequence(GameObject obj)
    {
        bool answer = false;
        sequenceObjects.Add(obj);
        ChangeIndexes();
        Tile currentTile = obj.GetComponent<Tile>();
        Vector2 coord = currentTile.coords;
        answer = Sequence(answer, currentTile, Vector2.left, Vector2.right);
        m = 0;
        answer = Sequence(answer, currentTile, Vector2.up, Vector2.down);
        Reset1();
        ChangeIndexes();
        return answer;
    }

    private bool Sequence(bool answer, Tile currentTile, Vector2 a, Vector2 b)
    {
        CheckCell(currentTile, a);
        CheckCell(currentTile, b);
        if (m > 1)
        {
            answer = true;
        }

        return answer;
    }


    private void ChangeIndexes()
    {
        int in1 = lastclick.GetComponent<Tile>().transform.GetChild(0).GetComponent<Im>().index;
        lastclick.GetComponent<Tile>().transform.GetChild(0).GetComponent<Im>().index = newClick.GetComponent<Tile>().transform.GetChild(0).GetComponent<Im>().index;
        newClick.GetComponent<Tile>().transform.GetChild(0).GetComponent<Im>().index = in1;

    }

    private bool CheckCell(Tile tile, Vector2 dir)
    {
        Vector2 coord = tile.coords;
        if (tiles.ContainsKey(coord + dir))
        {
            if (tiles[coord + dir].GetComponent<Tile>().marked == true){ return false; }
            if (tiles[coord + dir].transform.childCount != 0 && tiles[coord].transform.childCount != 0)
            {
                if (tiles[coord + dir].GetComponentInChildren<Im>().index == tiles[coord].GetComponentInChildren<Im>().index)
                {
                    m += 1;
                    sequenceObjects.Add(tiles[coord + dir]);
                    CheckCell(tiles[coord + dir].GetComponent<Tile>(), dir);
                }
            }
        }
        if (m >= 2)
        {
            return true;
        }
        return false;
    }

 
    private void Swap()
    {
        wasClick = true;
        imLast = lastclick.transform.GetChild(0).gameObject;
        imNew = newClick.transform.GetChild(0).gameObject;
        imLast.transform.SetParent(matchCanvas.transform);
        imNew.transform.SetParent(matchCanvas.transform);
        startPos = imLast.transform.position;
        endPos = newClick.transform.position;
        move = true;
    }

    private void Update()
    {
        if (move == true)
        {
            SwapMovement();
        }


        if (ch == false)   
        {
            canClick = false;
            Cells();
        }

        if (checkToChangeElements == true)
        {
            if (clearAmount == taskAmount)
            {
                checkToChangeElements = false;
                clearAmount = 0;
                taskAmount = 0;
                P();
            }
        }
    }

    private void SwapMovement()
    {
        val += Time.deltaTime*8;

        imLast.transform.position = Vector3.Lerp(imLast.transform.position, endPos, val);
        imNew.transform.position = Vector3.Lerp(imNew.transform.position, startPos, val);

        if ((Vector3.Distance(imLast.transform.position,endPos)<0.001f) && (Vector3.Distance(imLast.transform.position, endPos) < 0.001f))
        {
            imLast.transform.position = endPos;
            imNew.transform.position = startPos;
            imLast.transform.SetParent(newClick.transform);
            imNew.transform.SetParent(lastclick.transform);

            wasClick = false;
            move = false;
            lastclick = null;
            ch = false;
            val = 0;
        }
    }

    void Cells()
    {
        ch = true; 
        for (int l = 0; l < manager.ySize; l++)
        {
            for (int i = 0; i < manager.xSize; i++)
            {
               if (tiles[new Vector2(i, l)].GetComponent<Tile>().marked == false)
                {
                    if (Check(tiles[new Vector2(i, l)]))
                    {
                        sequenceObjects.Add(tiles[new Vector2(i, l)]);
                        CheckIsAllIce();
                        foreach (GameObject obj in sequenceObjects)
                        {
                            if (objectsToRemove.Contains(obj)==false)
                            {
                                obj.GetComponent<Tile>().marked = true;
                                objectsToRemove.Add(obj);

                            }
                        }
                    }
                    sequenceObjects.Clear();
                    m = 0;
                }
            }
        }

        taskAmount += objectsToRemove.Count;
        ClearMatch();
        checkToChangeElements = true;


    }

    private void CheckIsAllIce()
    {
         iceAmount = 0;
        foreach (GameObject obj in sequenceObjects)
        {
            if (obj.GetComponent<Tile>().ice == true)
            {
                IsTopAndBottomIce(obj.GetComponent<Tile>());
                iceAmount += 1;
            }
        }
        if (iceAmount == sequenceObjects.Count)
        {
            sequenceObjects.Clear();
        }
        else
        {
            foreach (GameObject obj in iceTilestoAdd)
            {
                sequenceObjects.Add(obj);
            }
            iceTilestoAdd.Clear();
        }
       
    }

    private void IsTopAndBottomIce(Tile tile)
    {
        Vector2 topTileCoords = tile.coords + new Vector2(0, 1);  
        Vector2 bottomTileCoords = tile.coords - new Vector2(0, 1);
        if (tiles.ContainsKey(topTileCoords))
        {
           if (tiles[topTileCoords].GetComponent<Tile>().ice == true)
           {
                iceTilestoAdd.Add(tiles[topTileCoords]);
           }
        }

    }

    void P()
    {
        foreach (GameObject obj in objectsToRemove)
        {
                foreach (Transform child in obj.transform)
                {
                    child.transform.SetParent(null);
                    Destroy(child.gameObject);
                }
        }
        if (objectsToRemove.Count == 0)
        {
            NewWasNotAdd();
        }
        else
        {
            objectsToRemove.Clear();
            FallCells();
        }


    }

    public void NewWasAdd()
    {
        b += 1;
        if (FindObjectOfType<Bar>().Check()==false)
        {
            if (b == columnsWasAdd)
            {
                ch = false;  
                b = 0;
                columnsWasAdd = 1000;
            }
            
        }
    
    }

    public void NewWasNotAdd()
    {
        newWasAdded = false;
        canClick = true;
        ch = true; 
  
    }

    public void CheckNoAdd()
    {
        ansColumns += 1;
        if (ansColumns == 5)
        {
            NewWasNotAdd();
            ansColumns = 0;
        }

    }

    void ClearMatch()
    {
        if (objectsToRemove.Count == 0) { return; }
        int am = -1;
        var iceObjects = objectsToRemove.OrderBy(p => p.GetComponent<Tile>().ice).ToArray();
        bool a = iceObjects[iceObjects.Length-1].GetComponent<Tile>().ice;


        foreach (GameObject obj in objectsToRemove)
        {
            Im objIm = obj.transform.GetChild(0).GetComponent<Im>();
            am = CheckOnIceSound(am, a, objIm);
            objIm.Des(obj.GetComponent<Tile>().ice);
            if (!columnIndex.Contains(obj.GetComponent<Tile>().coords.x))
            {
                columnIndex.Add(obj.GetComponent<Tile>().coords.x);
            }
            obj.GetComponentInChildren<Im>().index = -2;
        }
        iceTiles.Clear();

        FindObjectOfType<Bar>().IncreaseAmount(objectsToRemove.Count);


    }

    private int CheckOnIceSound(int am, bool a, Im objIm)
    {
        am++;
        if (am == 0)
        {
            if (a == false) { objIm.SetClearSound(false); }
            else { objIm.SetClearSound(true); }
        }
        return am;
    }

   
    private void FallCells()
    {
        columnsWasAdd = columnIndex.Count;
        foreach (float index in columnIndex)
        {
            Transform column = transform.GetChild((int)index);
            column.gameObject.GetComponent<Column>().ChangeElements();
        }
        columnIndex.Clear();
    }


    private bool Check(GameObject gameObject)
    {
        if (CheckCell(gameObject.GetComponent<Tile>(), new Vector2(0,1))) { return true; }
        sequenceObjects.Clear();
        m = 0;
        if (CheckCell(gameObject.GetComponent<Tile>(), new Vector2(1,0))) { return true; }
        return false;

    }

   


}
