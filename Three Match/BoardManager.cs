using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    public int xSize, ySize;
    public Dictionary<Vector2, GameObject> tiles=new Dictionary<Vector2, GameObject>();
    GameObject canvas;
    internal GameObject board;
    public List<GameObject> difTiles;
    Vector2 tileSize;
    public List<GameObject> boomPictures;
    public GameObject iceFragments;
    public int maxAmount;
    public float xOffset;
    public float yOffset;
    public float offect;
    [SerializeField] GameObject column;
    internal Color32 color1= new Color32(247, 193, 199, 255);
    internal Color32 color2= new Color32(255, 229, 161, 255);


    void Start()
    {
        FindObjectOfType<ShelveManager>().quit.SetActive(false);
        GameObject.FindGameObjectWithTag("mainCanvas").GetComponent<Canvas>().enabled = false;
        GameObject.FindGameObjectWithTag("pictureCanvas").GetComponent<Canvas>().enabled = false;
        canvas = GameObject.Find("ThreeMatchCanvas(Clone)");
        board = FindObjectOfType<TileManager>().gameObject;
        offect = FindObjectOfType<CakeConstructor>().offect;
        CreateBoard();

    }

   public void CreateBoard()
    {
        RectTransform rt = board.GetComponent<RectTransform>();
        xOffset = (rt.sizeDelta.x * offect) / xSize;
        yOffset =( rt.sizeDelta.y * offect) / ySize;
        Vector3[] v = new Vector3[4];
        rt.GetWorldCorners(v);
        float startX = v[0].x + xOffset / 2;
        float startY = v[0].y + yOffset / 2;
        CreateTiles(xOffset, yOffset, startX, startY);

    }

    public virtual void CreateTiles(float xOffset, float yOffset, float startX, float startY)
    {
        for (int i = 0; i < xSize; i++)
        {
            GameObject obj = Instantiate(column);
            obj.AddComponent<Column>();
            obj.transform.SetParent(board.transform);
            for (int l = 0; l < ySize; l++)
            {
                GameObject tile = difTiles[Random.Range(0, difTiles.Count - 1)];
                Vector2 pos = new Vector2(startX + (xOffset * i), startY + (yOffset * l));
                GameObject newTile = Instantiate(tile, pos, Quaternion.identity);
                newTile.GetComponent<RectTransform>().sizeDelta = new Vector2(xOffset, yOffset);
                newTile.GetComponent<Tile>().coords = new Vector2(i, l);
                ChangeTileColor(i, l, newTile);
                newTile.transform.SetParent(obj.transform);
                tiles.Add(new Vector2(i, l), newTile);

            }
        }
    }

    private void ChangeTileColor(int i, int l, GameObject newTile)
    {
        Image im = newTile.GetComponent<Image>();
        if ((i % 2 == 0 && l % 2 == 0) || (i % 2 == 1 && l % 2 == 1))
        {
            im.color = color1;
        }
        else { im.color = color2; }
    }

    public bool IsNeighbour(GameObject lastClick, GameObject newClick)
    {
        Vector2 lastCoord = lastClick.GetComponent<Tile>().coords;
        Vector2 newClickCoord = newClick.GetComponent<Tile>().coords;

        if ((lastCoord + new Vector2(0, 1) == newClickCoord) 
           || (lastCoord + new Vector2(1, 0) == newClickCoord)
           || (lastCoord - new Vector2(0, 1) == newClickCoord)
           || (lastCoord - new Vector2(1, 0) == newClickCoord))
        {
            return true;
        }
        return false;
    }

    public void Stop()
    {
        canvas.GetComponent<fadeEffect>().enabled = true;
    }

}
