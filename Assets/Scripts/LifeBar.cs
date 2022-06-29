using UnityEngine;

public class LifeBar : MonoBehaviour
{
    [SerializeField] GameObject heart;
    void Start(){
        Instantiate(heart, transform.position, transform.rotation);
    }
}
