using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    private CharacterController2D characterController;
    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private float horizontalMove = 0f;
    private bool jumpButtonPressed = false;
    private bool jumpButtonPressing = false;

    [Header("Health")]
    [SerializeField] [Range(1, 6)] private int maxHealth = 0;
    private int health = 0;
    [SerializeField] private GameObject prefabLifeBar;
    [SerializeField] private RectTransform mainCanvas;
    private LifeBar lifeBar;

    void Awake()
    {
        characterController = GetComponent<CharacterController2D>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        health = maxHealth;
        var InstantiateLifeBar = Instantiate(prefabLifeBar, mainCanvas, false);
        lifeBar = InstantiateLifeBar.GetComponent<LifeBar>();
        lifeBar.CreateHearts(maxHealth);
    }

    public void SetMoviment(InputAction.CallbackContext action) => horizontalMove = action.ReadValue<float>();

    public void SetJumpButtonPressed(InputAction.CallbackContext action) => jumpButtonPressed = action.performed;

    public void SetJumpButtonPressing(InputAction.CallbackContext action) => jumpButtonPressing = action.performed;

    void Update()
    {
        SetAnimations();
    }

    void FixedUpdate()
    {
        characterController.Move(horizontalMove, jumpButtonPressed, jumpButtonPressing);
        jumpButtonPressed = false;
    }

    void SetAnimations(){
        var isRunning = horizontalMove != 0;
        var isJumping = !characterController.IsGrounded();
        animator.SetBool("IsRunning", isRunning);
        animator.SetBool("IsJumping", isJumping);
        animator.SetFloat("YVelocity", rb.velocity.y);
    }

    public void OnDamageDone(int damage) {
        health = Mathf.Clamp(damage, 0, health);
        lifeBar.Damage(damage);
        StartCoroutine(DamagePlayer());
    }

    IEnumerator DamagePlayer() {
        spriteRenderer.color = new Color(255, 255, 255, 0.3f);
        yield return new WaitForSeconds(0.3f);
        spriteRenderer.color = new Color(255, 255, 255);
    }
}
