using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject slot;
    Slot slotComponent;

    Rigidbody2D rig;
    public float jump;
    public float walkSpeed;
    public KeyCode key;

    public AudioSource popSound;
    public AudioSource jumpSound;

    public PlayerStatus status = PlayerStatus.waiting;
    bool wallColliding = false;
    Animator animator;

    public List<AudioClip> weeSounds;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        slotComponent = slot.GetComponent<Slot>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (status)
        {
            case PlayerStatus.waiting:
                UpdateWaiting();
                break;
            case PlayerStatus.wakingUp:
                break;
            case PlayerStatus.walking:
                UpdateWalking();
                break;
            case PlayerStatus.died:
                UpdateDied();
                break;
            default:
                break;
        }
    }

    void UpdateWaiting()
    {
        transform.position += Vector3.left * GameManager.instance.GetSpeed() * Time.deltaTime;
        if (transform.position.x <= slot.transform.position.x)
        {
            status = PlayerStatus.walking;
            slot.GetComponent<Slot>().UpdateKeyStatus();
            animator.SetTrigger("walk");
        }
    }

    void UpdateWalking()
    {
        if (Input.GetKeyDown(key))
        {
            jumpSound.clip = weeSounds[Random.Range(0, weeSounds.Count)];
            jumpSound.Play();
            rig.velocity = new Vector2(0, jump);
            status = PlayerStatus.jumping;
            animator.SetTrigger("jump");
        }

        if (wallColliding)
        {
            return;
        }

        var slotDistance = slot.transform.position.x - transform.position.x;
        if (Mathf.Abs(slotDistance) > 0.01f) // Se tem que caminhar para chegar ao slot
        {
            if (slotDistance > 0) // Se tem que caminhar para a direita
            {
                transform.position += Vector3.right * walkSpeed * Time.deltaTime;
                if (transform.position.x > slot.transform.position.x)
                {
                    // Se chegou
                    transform.position = new Vector3(slot.transform.position.x, transform.position.y);
                }
            } else // Se tem que caminhar para a esquerda
            {
                transform.position += Vector3.left * walkSpeed * Time.deltaTime;
                if (transform.position.x < slot.transform.position.x)
                {
                    // Se chegou
                    transform.position = new Vector3(slot.transform.position.x, transform.position.y);
                }
            }
        }    
    }

    public void Die()
    {
        popSound.Play();
        if (status == PlayerStatus.died)
        {
            return;
        }
        status = PlayerStatus.died;
        animator.SetTrigger("hurt");
        slotComponent.occupied = false;
        slotComponent.UpdateKeyStatus();
    }

    void UpdateDied()
    {
        transform.position += Vector3.left * Time.deltaTime * GameManager.instance.GetSpeed();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Die();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (status == PlayerStatus.jumping)
        {
            if (collision.gameObject.tag == "ground")
            {
                status = PlayerStatus.walking;
                animator.SetTrigger("walk");
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            wallColliding = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            wallColliding = false;
        }
    }
}


public enum PlayerStatus
{
    waiting,
    wakingUp,
    walking,
    jumping,
    colliding, 
    died
}