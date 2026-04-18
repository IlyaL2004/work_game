using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotationSpeed = 30f;
    
    public Vector3 rotationAxis = Vector3.up; 

    void Update()
    {
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
    }
}