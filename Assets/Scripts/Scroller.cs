using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class Scroller : MonoBehaviour
{
    private RawImage rawImage;
    [SerializeField] private float increase;
    [SerializeField] private float speed;
    void Awake()
    {
        rawImage = GetComponent<RawImage>();
    }

    void FixedUpdate()
    {
        rawImage.uvRect = new Rect(rawImage.uvRect.position + new Vector2(increase * speed / 1000, 0), rawImage.uvRect.size);
    }
}
