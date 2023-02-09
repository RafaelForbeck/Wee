using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public SpriteRenderer keySprite;

    public AnimatorOverrideController animator;

    public bool occupied = false;
    public KeyCode key;

    public void UpdateKeyStatus()
    {
        if (occupied)
        {
            keySprite.color = new Color(1, 1, 1, 1f);
        }
        else
        {
            keySprite.color = new Color(1, 1, 1, 0f);
        }
    }
}
