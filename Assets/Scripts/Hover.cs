using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    float speed = 10f;
    Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(0, Mathf.Sin(Time.fixedTime) / 4, 0) + startPos;
        transform.Rotate(new Vector3(0, speed, 0) * Time.deltaTime, Space.World);
    }
}
