using UnityEngine;

public class LifeBar : MonoBehaviour
{
    [SerializeField] private GameObject heart;
    private Transform heartTransform;
    void Awake(){
        heartTransform = heart.GetComponent<Transform>();
        CreateHearts(4);
    }

    void CreateHearts(int numberHearts) {
        for (int i = 0; i < numberHearts; i++) {
            GameObject HeartClone = Instantiate(heart, transform.position, transform.rotation, transform);
            RectTransform rt = HeartClone.GetComponent<RectTransform>();
            rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, (rt.rect.width + 5f) * i, rt.rect.width);
        }
    }
}
