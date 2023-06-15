using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    [Header("MoveSpeed")]
    [SerializeField] public float MoveSpeed = 5f;
    Rigidbody _rb;
    Animator _anim;

    void Start()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void Update()
    {
        var cameraDir = Camera.main.transform.forward;
        cameraDir.y = 0;
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 dir =  cameraDir * v + Camera.main.transform.right * h;
        float hv = dir != Vector3.zero ? 10f : 0;

        if (dir != Vector3.zero)
        {
            gameObject.transform.forward = dir;
        }
        _anim.SetFloat("SpeedF", hv);
        _rb.velocity = dir.normalized * MoveSpeed;
    }
}
