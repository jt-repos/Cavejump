using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] float minRotationSpeed = 360f;
    [SerializeField] float maxRotationSpeed = 720f;
    float rotationSpeed;
    // Update is called once per frame

    void Start()
    {
        rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
    }

    void Update ()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
