using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Tooltip("MoveSpeed")]
    [SerializeField] public float MoveSpeed = 5f;
    [Tooltip("Playerを追従しているCinemachine")]
    [SerializeField] private CinemachineVirtualCamera _cinemacine;
    [Tooltip("IK 右手の対象Obj")]
    [SerializeField] private GameObject IK_handRight = null;
    [Tooltip("IK 左手の対象Obj")]
    [SerializeField] private GameObject IK_handLeft = null;
    [Tooltip("IKのスイッチ")]
    [SerializeField] private bool _ikTF = false;
    public bool GetIK_TF { get => _ikTF; set => _ikTF = value; }
    [Tooltip("Playerが操作可能かのスイッチ")]
    [SerializeField] private bool _moveSwitch = true;
    public bool GetMoveSwitch { get => _moveSwitch; set => _moveSwitch = value; }
    Rigidbody _rb;
    Animator _anim;

    void Start()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_moveSwitch) { PlayerMove(); }
    }

    /// <summary>Playerの移動処理</summary>
    private void PlayerMove()
    {
        var cameraDir = Camera.main.transform.forward;
        cameraDir.y = 0;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 dir = cameraDir * v + Camera.main.transform.right * h;
        float hv = dir != Vector3.zero ? 10f : 0;

        if (dir != Vector3.zero)
        {
            gameObject.transform.forward = dir;
        }

        _anim.SetFloat("SpeedF", hv);
        _rb.velocity = dir.normalized * MoveSpeed;
    }

    /// <summary>Cienmachineのカメラ回転を操作する</summary>
    /// <param name="ON_OFF">trueの時カメラ回転ON。falseのときカメラ回転OFF</param>
    public void CameraControl(bool ON_OFF)
    {
        if (ON_OFF) { _cinemacine.enabled = true; }
        if (!ON_OFF) { _cinemacine.enabled = false; }
    }
    
    private void OnAnimatorIK(int layerIndex)
    {
        IKSetter();
    }

    //腕のIKを設定
    private void IKSetter()
    {
        if (_ikTF)
        {
            if (IK_handRight != null)//右手
            _anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            _anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
            _anim.SetIKPosition(AvatarIKGoal.RightHand, IK_handRight.transform.position);

            if (IK_handLeft != null)//左手
            _anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            _anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
            _anim.SetIKPosition(AvatarIKGoal.LeftHand, IK_handLeft.transform.position);
        }
    }
}
