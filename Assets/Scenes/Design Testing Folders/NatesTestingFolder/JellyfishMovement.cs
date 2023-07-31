using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyfishMovement : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    [SerializeField] float movementSpeed;
    [SerializeField] bool[] rotationAxis;
    // Update is called once per frame
    void Update()
    {
        if (rotationAxis[0])
        {
            gameObject.transform.Rotate(transform.right * rotationSpeed);
        }
        if (rotationAxis[1])
        {
            gameObject.transform.Rotate(transform.up * rotationSpeed);
        }
        if (rotationAxis[2])
        {
            gameObject.transform.Rotate(transform.forward * rotationSpeed);
        }
        gameObject.transform.Translate(transform.up * movementSpeed);
    }
}
