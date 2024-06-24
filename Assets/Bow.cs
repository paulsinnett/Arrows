using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public Rigidbody arrowPrefab;
    public float force = 700.0f;
    public float [] angles = { -10, -10, -10, -10 };

    void Update()
    {
        if (Input.GetButtonUp("Jump"))
        {
            StartCoroutine(Fire());
        }
    }

    IEnumerator Fire()
    {
        foreach (float angle in angles)
        {
            transform.rotation = Quaternion.Euler(angle, 0, 0);
            Rigidbody arrow = Instantiate(arrowPrefab);
            arrow.transform.position = transform.position;
            arrow.transform.rotation = transform.rotation;
            arrow.AddForce(transform.forward * force, ForceMode.Impulse);

            yield return new WaitForSeconds(1.0f);
        }
    }
}
