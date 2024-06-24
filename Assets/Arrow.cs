using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Air air;
    public float velocityScale = 0.1f;
    Rigidbody physics;
    float maxVelocity = 0.0f;

    void Start()
    {
        physics = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (!physics.isKinematic && physics.velocity.magnitude > 0.0f)
        {
            maxVelocity = Mathf.Max(maxVelocity, physics.velocity.magnitude);
            float dragCoefficient = velocityScale / Mathf.Sqrt(physics.velocity.magnitude);
            physics.AddForce(
                -physics.velocity.normalized *
                0.5f *
                dragCoefficient *
                air.density *
                Mathf.Pow(transform.localScale.x * 0.5f, 2.0f) * Mathf.PI *
                Mathf.Pow(physics.velocity.magnitude, 2.0f));

            physics.MoveRotation(Quaternion.LookRotation(physics.velocity));
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.LogFormat(
            "Mass = {0}, Max Velocity = {1}, Hit Velocity {2}, position = {3}",
            physics.mass,
            maxVelocity,
            collision.relativeVelocity.magnitude,
            transform.position);

        physics.isKinematic = true;
        physics.detectCollisions = false;
    }
}
