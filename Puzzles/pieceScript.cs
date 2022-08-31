using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coffee.UIExtensions;
using UnityEngine.Rendering;

public class pieceScript : MonoBehaviour
{
    Vector3 rightPosition;
    public int puzzles = 0;
    public bool inCell = false;
    float distance = 37f;
    Vector2 rightAndLeft=new Vector2(0.115f, 0.17f);
    Vector2 topAndBottom = new Vector2(0.2f, 0.8f);
    float maxPuzzles = 6;
    void Awake()
    {
        rightPosition = transform.position;
    }

    private void Start()
    {
        if (puzzles < maxPuzzles)
        {
            float x = Random.Range(Screen.width * rightAndLeft.x, Screen.width * rightAndLeft.y);
            float y = Random.Range(Screen.height * topAndBottom.x, Screen.height * topAndBottom.y);
            transform.position = new Vector3(x, y);
        }
        else
        {
            float x = Random.Range(Screen.width - Screen.width * rightAndLeft.y, Screen.width - Screen.width * rightAndLeft.x);
            float y = Random.Range(Screen.height * topAndBottom.x, Screen.height * topAndBottom.y);
            transform.position = new Vector3(x,y);
        }
    }

    internal void CheckInCell()
    {
        if (Vector3.Distance(transform.position, rightPosition) < distance)
        {
            transform.position = rightPosition;
            GetComponentInParent<puzzles>().ChangeAmount();
            this.gameObject.transform.SetAsFirstSibling();
            GetComponent<DragAndDrop>().enabled = false;
            GetComponent<pieceScript>().enabled = false;
            music.Music.Berry();

        }
     
    }

}

