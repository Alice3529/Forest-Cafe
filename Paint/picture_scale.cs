using UnityEngine;
using UnityEngine.EventSystems;

public class picture_scale : MonoBehaviour, IDragHandler
{
    float currentDistance;
    float startdistance;
    float minSize = 1f;
    float maxSize = 2.3f;
    RectTransform rect;
    bool phase = false;
    Vector2 pos1;
    Vector2 startPosition;
    Vector2 startPivot;
    [SerializeField] Canvas canvas;
    [SerializeField] RectTransform image;
    ColorManager manager;
    float scale = 0.1f;
    float boardVal = 1.8f;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        startPosition = rect.position;
        startPivot = rect.pivot;
        manager = FindObjectOfType<ColorManager>();
    }



    private void Update()
    {
        CheckCanPaint();
        ScalePicture();
    }

    private void CheckCanPaint()
    {
        if (Input.touchCount == 0)
        {
            phase = false;
        }
        if (Input.touchCount == 1 && phase == false)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended)
            {
                manager.canPaint = true;
            }
        }
    }

    private void ScalePicture()
    {
        if (Input.touchCount == 2)
        {
            phase = true;
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startdistance = Vector3.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
            }

            if (touch.phase == TouchPhase.Moved)
            {
                Pinch();
            }

            if (touch.phase == TouchPhase.Ended)
            {
                manager.canPaint = true;

            }
        }
    }

    private void Pinch()
    {
        manager.canPaint = false;
        currentDistance = Vector3.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
        float dif = (currentDistance - startdistance);
        if (dif > 0)
        {
            if (transform.localScale.x + scale <= maxSize)
            {
                transform.localScale = 
                new Vector3(transform.localScale.x + scale, transform.localScale.y + scale, 1);
            }
        }
        else if (dif < 0)
        {
            if (transform.localScale.x - scale > minSize)
            {
                transform.localScale = 
                new Vector3(transform.localScale.x - scale, transform.localScale.y - scale, 1);
            }
        }
        startdistance = currentDistance;
    }


    public void StartPos()
    {
        transform.localScale = Vector3.one;
        rect.pivot = startPivot;
        rect.position = startPosition;
        Destroy(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Input.touchCount == 1)
        {
            manager.canPaint = false;
            Vector2 direction = eventData.position - pos1;
            float halfWidth = image.rect.width / boardVal;
            float halfHeight = image.rect.height / boardVal;

            if (direction.x < 0 && rect.anchoredPosition.x < -halfWidth) {return;}
            if (direction.x > 0 && rect.anchoredPosition.x > halfWidth) { return; }
            if (direction.y < 0 && rect.anchoredPosition.y < -halfHeight) { return; }
            if (direction.y > 0 && rect.anchoredPosition.y > halfHeight) { return; }

            rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
            pos1 = eventData.position;
          
        }
    }
}