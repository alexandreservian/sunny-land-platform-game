using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class HeartManager : MonoBehaviour
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

    public void ActiveHeart(bool newActive) {
        active = newActive;
        heart.sprite = newActive ? heartOn : heartOff;
    }
}
