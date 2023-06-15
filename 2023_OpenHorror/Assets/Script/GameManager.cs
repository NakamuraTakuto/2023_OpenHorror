using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool Is_Game = true;
    [Tooltip("�`�F�C�X���n�߂�ʒu")]
    [SerializeField] GameObject _reSetPos;
    [Tooltip("EnemyObj")]
    [SerializeField] GameObject _enemy;
    [Tooltip("�X�|�[���̃N�[���^�C��(min, max)")]
    [SerializeField] float[] _ctRange = new float[] { 15f, 30f };
    [Tooltip("�ǂ��Ă��鎞��(min, max)")]
    [SerializeField] float[] _chaseTimeRange = new float[] { 10f, 20f };
    [Tooltip("�Q�[���I�[�o�[Panel")]
    [SerializeField] GameObject _gameOverPanel;
    [Tooltip("clear���ɏo���Ó]Panel")]
    [SerializeField] GameObject _clearPanel;
    public bool Is_Clear = false;
    bool _chase = false;
    float _ct = 0;
    float _chaseTime = 0;
    float _time = 0;

    private void Start()
    {
        _ct = Random.Range(_ctRange[0], _ctRange[1]);
    }

    private void Update()
    {
        _time += Time.deltaTime;

        if (_ct <= _time && !_chase)
        {
            _chaseTime = Random.Range(_chaseTimeRange[0], _chaseTimeRange[1]);
            _chase = true;
            _enemy.transform.position = _reSetPos.transform.position;
            _enemy.SetActive(true);
            _time = 0;
        }
        if (_chase && _time > _chaseTime)
        {
            _enemy.SetActive(false);
            _ct = Random.Range(_ctRange[0], _ctRange[1]);
            _chase = false;
            _time = 0;
        }

        if (!Is_Game)
        {
            _gameOverPanel.SetActive(true);
        }

        if (Is_Clear)
        {
            _clearPanel.SetActive(true);
        }
    }
}
