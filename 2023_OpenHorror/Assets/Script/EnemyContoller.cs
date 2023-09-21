using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class EnemyContoller : MonoBehaviour
{
    [Tooltip("Player")]
    [SerializeField] GameObject _player = null;
    [SerializeField] float _moveSpeed = 7f;
    [SerializeField] GameObject gameManager;
    Rigidbody _rb;
    Transform _playerTransform;
    Animator _anim;
    GameManager _gm;
    NavMeshAgent _agent;

    private void Start()
    {
        if (_player == null)
        {
            Debug.Log("Playerがアタッチされていません");
        }
        _rb = GetComponent<Rigidbody>();
        _playerTransform = _player.GetComponent<Transform>();
        _gm = gameManager.GetComponent<GameManager>();
        _agent = GetComponent<NavMeshAgent>();
        //_anim.GetComponent<Animator>();

        _agent.speed = _moveSpeed;
    }

    private void Update()
    {
        if (_gm.Is_GO)
        { 
            //Vector3 direction = (_playerTransform.position - gameObject.transform.position);
            //_rb.velocity = direction.normalized * _moveSpeed;
            ////_anim.SetFloat("SpeedF", 10);
            _agent.destination = _playerTransform.position;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == _player)
        {
            _gm.Is_Game = false;
        }
    }
}
