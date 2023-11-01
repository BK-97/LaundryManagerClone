using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    public float turnSpeed = 30f;
    public float speed = 10f;

    void Update()
    {
        transform.Rotate(0f, turnSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, 0f, Space.World);
        transform.Translate(0f, speed * Input.GetAxis("Vertical") * Time.deltaTime, 0f, Space.Self);
    }
}
