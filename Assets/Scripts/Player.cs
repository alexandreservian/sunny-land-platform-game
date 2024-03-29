using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController2D))]
[RequireComponent(typeof(CharacterCollision))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Player : MonoBehaviour
{
    private CharacterController2D characterController;
    private CharacterCollision characterCollision;
    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private float horizontalMove = 0f;
    private float verticalMove = 0f;
    private bool jumpButtonPressed = false;
    private bool jumpButtonPressing = false;

    [Header("Health")]
    [SerializeField] [Range(1, 6)] private int maxHealth = 0;
    [SerializeField] [Range(0, 1)] private float invincibleTime  = 0;
    [SerializeField] [Range(0, 1)] private float tookDamageTime  = 0;
    private float invincibleTimeCounter = 0;
    private int health = 0;
    private bool tookDamage = false;
    [SerializeField] private GameObject prefabLifeBar;
    [SerializeField] private RectTransform mainCanvas;
    private LifeBar lifeBar;

    void Awake()
    {
        characterController = GetComponent<CharacterController2D>();
        characterCollision = GetComponent<CharacterCollision>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        health = maxHealth;
        var InstantiateLifeBar = Instantiate(prefabLifeBar, mainCanvas, false);
        lifeBar = InstantiateLifeBar.GetComponent<LifeBar>();
        lifeBar.CreateHearts(maxHealth);
    }

    public void SetHorizontalMoviment(InputAction.CallbackContext action) => horizontalMove = action.ReadValue<float>();

    public void SetVerticalMoviment(InputAction.CallbackContext action) => verticalMove = action.ReadValue<float>();

    public void SetJumpButtonPressed(InputAction.CallbackContext action) => jumpButtonPressed = action.performed;

    public void SetJumpButtonPressing(InputAction.CallbackContext action) => jumpButtonPressing = action.performed;

    void Update()
    {
        SetAnimations();
        TimersManager();
        ResetAlphaColor();
    }

    void TimersManager () {
        if (invincibleTimeCounter > 0) {
            invincibleTimeCounter -= Time.deltaTime;
        }
    }

    void ResetAlphaColor () {
        if (invincibleTimeCounter <= 0) {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
        }
    }

    void FixedUpdate()
    {
        characterController.Move(horizontalMove, verticalMove, jumpButtonPressed, jumpButtonPressing);
        jumpButtonPressed = false;
    }

    void SetAnimations(){
        var isRunning = horizontalMove != 0;
        var isJumping = !characterCollision.IsGrounded();
        animator.SetBool("IsRunning", isRunning);
        animator.SetBool("IsJumping", isJumping);
        animator.SetFloat("YVelocity", rb.velocity.y);
        animator.SetBool("TookDamage", tookDamage);
    }

    public void OnDamageDone(int damage) {
        if(invincibleTimeCounter <= 0) {
            health = Mathf.Clamp(damage, 0, health);
            lifeBar.Damage(damage);
            StartCoroutine(DamagePlayer());
            invincibleTimeCounter = invincibleTime;
        }
    }

    IEnumerator DamagePlayer() {
        tookDamage = true;
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.6f);
        characterController.KnockBack(tookDamageTime);
        yield return new WaitForSeconds(tookDamageTime);
        tookDamage = false;
    }
}
