using UnityEngine;

public class LifeBar : MonoBehaviour
{
    [SerializeField] private GameObject heart;
    private Transform heartTransform;
    void Awake(){
        heartTransform = heart.GetComponent<Transform>();
        CreateHearts(2);
    }

    void CreateHearts(int numberHearts) {
        Debug.Log(heartTransform.position.x);
        for (int i = 0; i < numberHearts; i++) {
            GameObject HeartClone = Instantiate(heart, new Vector2(35f * i, transform.position.y), transform.rotation, transform);
        }
    }
}
