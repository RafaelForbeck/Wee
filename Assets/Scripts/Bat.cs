using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    float period;

    void Start()
    {
        period = Random.Range(0f, Mathf.PI * 2);    
    }

    // Update is called once per frame
    void Update()
    {
        period += Time.deltaTime * 3;
        print(Mathf.Sin(period));
        float high = Mathf.Sin(period) * 1.5f + 2.5f - 2.36f;
        print("High: " + high);
        transform.position = new Vector3(transform.position.x - GameManager.instance.GetSpeed() * Time.deltaTime, high);
    }
}
