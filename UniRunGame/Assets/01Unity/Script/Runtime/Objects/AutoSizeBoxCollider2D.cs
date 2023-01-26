using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSizeBoxCollider2D : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector2 objSize_ = default;

        BoxCollider2D boxCollider_ =
            gameObject.GetComponentMust<BoxCollider2D>();

        RectTransform parentRectTransform_ =
            transform.parent.gameObject.GetComponentMust<RectTransform>();
        RectTransform rectTransform_ =
            gameObject.GetComponentMust<RectTransform>();

        objSize_.x = parentRectTransform_.sizeDelta.x;
        objSize_.y = rectTransform_.sizeDelta.y;

        boxCollider_.size = objSize_;
    }   // Start()

}
