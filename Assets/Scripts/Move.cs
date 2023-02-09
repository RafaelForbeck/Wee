using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed = 0;

    void Update()
    {
        transform.position += Vector3.left * (GameManager.instance.GetSpeed() + speed) * Time.deltaTime;

        if (transform.position.x < -20)
        {
            Destroy(gameObject);
        }
    }
}
