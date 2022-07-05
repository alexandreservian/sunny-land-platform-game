using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class HeartManager : MonoBehaviour
{
    public bool active = true;
    private Animator animator;

    void Awake() {
        animator = GetComponent<Animator>();
    }

    public void Disable() {
        active = false;
        animator.Play("Disable");
    }
}
