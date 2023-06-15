using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyContoller : MonoBehaviour
{
    [Tooltip("Player")]
    [SerializeField] GameObject _player = null;
    [SerializeField] float _moveSpeed = 7f;
    [SerializeField] GameObject gameManager;
    Rigidbody _rb;
    Transform _playerTransform;

    private void Start()
    {
        if (_player == null)
        {
            Debug.Log("Playerがアタッチされていません");
        }
        _rb = GetComponent<Rigidbody>();
        _playerTransform = _player.GetComponent<Transform>();
    }

    private void Update()
    {
        Vector3 direction = (_playerTransform.position - gameObject.transform.position);
        _rb.velocity = direction.normalized * _moveSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == _player)
        {
            gameManager.GetComponent<GameManager>().Is_Game = false;
        }
    }
}
