using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GirlMove : MonoBehaviour
{
    [Tooltip("少女を出現させる時間")]
    [SerializeField] private float _moveTime = 5f;
    [Tooltip("少女の移動速度")]
    [SerializeField] private float _moveSpeed = 5f;
    Rigidbody rb;
    Animator _animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(Move());
        }
    }

    IEnumerator Move()
    {
        _animator.SetBool("WalkBool", true);
        rb.velocity = gameObject.transform.forward.normalized * _moveSpeed;
        yield return new WaitForSeconds(_moveTime);
        gameObject.SetActive(false);
    }
}
