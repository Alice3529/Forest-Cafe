using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragTile : EventTrigger
{
    List<RaycastResult> results = new List<RaycastResult>();
    bool a=true;
    GameObject m;
    TileManager tileManager;

    private void Start()
    {
        tileManager = FindObjectOfType<TileManager>();
    }
    public override void OnBeginDrag(PointerEventData data)
    {
        EventSystem.current.RaycastAll(data, results);
        if (results.Count != 0)
        {
            m = results[0].gameObject;
            tileManager.SetLastClicked(results[0].gameObject);
        }
    

    }
    public override void OnDrag(PointerEventData data)
    {
        if (a == true)
        {
            EventSystem.current.RaycastAll(data, results);
             if (results.Count > 0)
            {
                if (results[0].gameObject != m)
                {
                    tileManager.SetLastClicked(results[0].gameObject);
                    a = false;
                }
            }
            
        }
    }

    public override void OnEndDrag(PointerEventData data)
    {
        results.Clear();
        a = true;
    }
}
