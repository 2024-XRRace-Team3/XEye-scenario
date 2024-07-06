using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class MoveTest : MonoBehaviour
{

    public float speed = 1f;
    private Rigidbody rigidbody;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //rigidbody.AddForce(Vector3.right * speed, ForceMode.Acceleration);
        //transform.Translate(Vector3.forward * (Time.deltaTime * speed));
        if (crashTransform is not null) {
            this.transform.position = crashTransform.position;
            this.transform.rotation = crashTransform.rotation;
            rigidbody.velocity = Vector3.zero;
        }
        else
        {
            rigidbody.velocity += transform.forward * (speed * Time.deltaTime);

        }
    }

    private Transform crashTransform = null;
    
}
