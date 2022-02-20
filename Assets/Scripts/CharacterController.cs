using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Rigidbody2D rg;
    void Awake()
    {
        rg = GetComponent<Rigidbody2D>();
    }

    public void move(float speed)
    {
        rg.velocity = new Vector2(speed, rg.velocity.y);
    }
}
