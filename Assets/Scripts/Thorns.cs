using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CompositeCollider2D))]
public class Thorns : MonoBehaviour
{
    [SerializeField] [Range(1, 6)] private int damage;
    public delegate void DamageDoneHandler(int damage);
    public event DamageDoneHandler DamageDone;

    private void OnTriggerStay2D(Collider2D collider) {
        if(collider.gameObject.CompareTag("Player") && DamageDone != null) {
            DamageDone(damage);
        }
    }
}
