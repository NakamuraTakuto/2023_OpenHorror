using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Tooltip("MoveSpeed")]
    [SerializeField] public float MoveSpeed = 5f;
    [Tooltip("Player��Ǐ]���Ă���Cinemachine")]
    [SerializeField] private CinemachineVirtualCamera _cinemacine;
    [Tooltip("IK �E��̑Ώ�Obj")]
    [SerializeField] private GameObject IK_handRight = null;
    [Tooltip("IK ����̑Ώ�Obj")]
    [SerializeField] private GameObject IK_handLeft = null;
    [Tooltip("IK�̃X�C�b�`")]
    [SerializeField] private bool _ikTF = false;
    public bool GetIK_TF { get => _ikTF; set => _ikTF = value; }
    [Tooltip("Player������\���̃X�C�b�`")]
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

    /// <summary>Player�̈ړ�����</summary>
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

    /// <summary>Cienmachine�̃J������]�𑀍삷��</summary>
    /// <param name="ON_OFF">true�̎��J������]ON�Bfalse�̂Ƃ��J������]OFF</param>
    public void CameraControl(bool ON_OFF)
    {
        if (ON_OFF) { _cinemacine.enabled = true; }
        if (!ON_OFF) { _cinemacine.enabled = false; }
    }
    
    private void OnAnimatorIK(int layerIndex)
    {
        IKSetter();
    }

    //�r��IK��ݒ�
    private void IKSetter()
    {
        if (_ikTF)
        {
            if (IK_handRight != null)//�E��
            _anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            _anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
            _anim.SetIKPosition(AvatarIKGoal.RightHand, IK_handRight.transform.position);

            if (IK_handLeft != null)//����
            _anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            _anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
            _anim.SetIKPosition(AvatarIKGoal.LeftHand, IK_handLeft.transform.position);
        }
    }
}
