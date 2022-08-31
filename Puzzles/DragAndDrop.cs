using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : EventTrigger
{
    public GameObject selectedPiece;
    Vector2 pos1;
    float widthOffset = 0.03f;
    float heightOffset = 0.05f;
    pieceScript piece;

    private void Start()
    {
        piece = GetComponent<pieceScript>();
    }


    public override void OnBeginDrag(PointerEventData data)
    {
        transform.SetParent(FindObjectOfType<puzzles>().transform);
    }
    public override void OnDrag(PointerEventData data)
    {
        Vector2 direction = data.position - pos1;
        float left = Screen.width * widthOffset;
        float bottom = Screen.height * heightOffset;
        float right = Screen.width - left;
        float top = Screen.height - bottom;

        if (direction.x < 0 && transform.position.x < left) { return; }
        if (direction.x > 0 && transform.position.x > right) { return; }
        if (direction.y < 0 && transform.position.y < bottom) { return; }
        if (direction.y > 0 && transform.position.y > top) { return; }


        Vector3 dif = data.position - new Vector2(transform.position.x, transform.position.y);
        transform.position = data.position;
        pos1 = data.position;
        piece.CheckInCell();



    }
}
