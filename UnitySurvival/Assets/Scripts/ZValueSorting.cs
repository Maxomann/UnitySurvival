using UnityEngine;
using System.Collections;

public class ZValueSorting : MonoBehaviour {

    private SpriteRenderer tempRend;

    void Start()
    {
        tempRend = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        tempRend.sortingOrder = (int)Camera.main.WorldToScreenPoint(tempRend.bounds.min).y * -1;
    }
}
