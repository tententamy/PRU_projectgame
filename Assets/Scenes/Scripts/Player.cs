using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    public Sprite[] activeSprites;
    public Sprite[] inactiveSprites;
    private int spriteIndex;
    public bool active;
    private bool isStopped = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), 0.11f, 0.11f);
    }

    private void AnimateSprite()
    {
        if (isStopped) return;

        spriteIndex++;
        Sprite[] currentSprites = active ? activeSprites : inactiveSprites;

        if (spriteIndex >= currentSprites.Length)
        {
            spriteIndex = 0;
        }

        spriteRenderer.sprite = currentSprites[spriteIndex];
    }

    public void StopMovement()
    {
        isStopped = true;
    }

    public void ResumeMovement()
    {
        isStopped = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemies"))
        {
            GameManager.Instance.Question(this, other.GetComponent<Enemies>());
        }
    }
}
