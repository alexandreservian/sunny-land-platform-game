using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Heart : MonoBehaviour
{
    public bool active = true;
    [SerializeField] Sprite heartOff;
    private Image heart;
    private Sprite heartOn;

    void Awake() {
        heart = GetComponent<Image>();
        heartOn = heart.sprite;
        ActiveHeart(active);
    }

    public void ActiveHeart(bool active) {
        heart.sprite = active ? heartOn : heartOff;
    }
}
