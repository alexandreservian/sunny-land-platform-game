using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LifeBar : MonoBehaviour
{
    [SerializeField] private GameObject heart;
    private List<HeartManager> heartsList;
    void Awake(){
        heartsList = new List<HeartManager>();
    }

    public void CreateHearts(int maxNumberHearts) {
        for (int i = 0; i < maxNumberHearts; i++) {
            var HeartClone = Instantiate(heart, transform, false);
            var rt = HeartClone.GetComponent<RectTransform>();
            var hM = HeartClone.GetComponent<HeartManager>();
            rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, (rt.rect.width + 5f) * i, rt.rect.width);
            heartsList.Add(hM);
        }
    }

    public void Damage(int damage) {
        var activeHearts = heartsList.Where(heart => heart.active);
        var takeHearts = Mathf.Clamp(damage, 0, activeHearts.Count());
        var list = activeHearts.Reverse().Take(takeHearts).ToList();
        foreach(HeartManager heart in list) {
            heart.ActiveHeart(false);
        }
    }
}
