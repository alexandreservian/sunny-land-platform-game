using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private Player player;
    private Thorns thorns;

    void Awake() {
        player = FindObjectOfType<Player>();
        thorns = FindObjectOfType<Thorns>();

        thorns.DamageDone += player.OnDamageDone;
    }
}
