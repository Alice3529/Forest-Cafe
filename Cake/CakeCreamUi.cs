using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CakeCreamUi : MonoBehaviour
{
    [SerializeField] GameObject imageUI;
    void Start()
    {
        AnotherData data = FindObjectOfType<CakeConstructor>().GetData();
        int amount = data.creams.Count;
        for (int i = 0; i < amount; i++)
        {
            GameObject image = Instantiate(imageUI, transform.position, Quaternion.identity);
            ShouldCreateFinger(data, i, image);
            image.GetComponent<Image>().sprite = data.creams[i].sprite;
            CreateCream createcCream = image.GetComponent<CreateCream>();
            createcCream.SetCream(data.creams[i].cream);
            createcCream.SetIndex(data.creams[i].index);
            createcCream.SetIndexAsCake(data.creams[i].indexAsCake);
            image.transform.SetParent(this.gameObject.transform);
            image.GetComponent<RectTransform>().localScale = Vector3.one;

        }

    }

    private static void ShouldCreateFinger(AnotherData data, int i, GameObject image)
    {
        if (data.creams[i].withFinger)
        {
            image.AddComponent<pointer>();
            image.GetComponent<pointer>().SetEndPos(data.creams[i].movePos, Vector3.zero);
        }
    }
}
