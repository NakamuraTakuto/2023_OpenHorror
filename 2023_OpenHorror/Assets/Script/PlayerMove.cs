using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    [Header("MoveSpeed")]
    [SerializeField] float _moveSpeed = 5f;
    Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 dir = Vector3.forward * v + Vector3.right * h;

        if (dir != Vector3.zero)
        {
            gameObject.transform.forward = dir;
        }

        _rb.velocity = dir.normalized * _moveSpeed;
    }
}
