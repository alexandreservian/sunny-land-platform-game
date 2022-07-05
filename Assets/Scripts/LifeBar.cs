using System.Collections.Generic;
using UnityEngine;

public class LifeBar : MonoBehaviour
{
    [SerializeField] private GameObject heart;
    private List<HeartManager> heartsList;
    void Awake(){
        heartsList = new List<HeartManager>();
    }

    public void CreateHearts(int numberHearts) {
        for (int i = 0; i < numberHearts; i++) {
            GameObject HeartClone = Instantiate(heart, transform, false);
            RectTransform rt = HeartClone.GetComponent<RectTransform>();
            HeartManager hM = HeartClone.GetComponent<HeartManager>();
            rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, (rt.rect.width + 5f) * i, rt.rect.width);
            heartsList.Add(hM);
        }
    }
}
