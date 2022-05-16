using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Vector2 smoothEffect;
    private Transform cam;
    private Vector3 lastCameraPosition;
    private float textureUnitSizeX;
    void Awake()
    {
        cam = Camera.main.transform;
        lastCameraPosition = cam.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
    }

    void LateUpdate()
    {
        Vector3 deltaMoviment = cam.position - lastCameraPosition;
        transform.position += new Vector3(deltaMoviment.x * smoothEffect.x, deltaMoviment.y * smoothEffect.y, transform.position.z);
        lastCameraPosition = cam.position;
        bool isLimit = Mathf.Abs(cam.position.x - transform.position.x) >= textureUnitSizeX;

        if(isLimit) {
            float offsetPositionX = (cam.position.x - transform.position.x) % textureUnitSizeX;
            transform.position = new Vector3(cam.position.x + offsetPositionX, transform.position.y);
        }

    }
}
