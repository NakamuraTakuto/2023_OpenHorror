using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    [Tooltip("MoveSpeed")]
    [SerializeField] public float MoveSpeed = 5f;
    [Tooltip("IK âEéËÇÃëŒè€Obj")]
    [SerializeField] private GameObject IK_handRight = null;
    [Tooltip("IK ç∂éËÇÃëŒè€Obj")]
    [SerializeField] private GameObject IK_handLeft = null;
    Rigidbody _rb;
    Animator _anim;

    void Start()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        IKSetter();
    }

    private void IKSetter()
    {
        if (IK_handLeft != null && IK_handRight != null)
        {
            //âEéË
            _anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            _anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
            _anim.SetIKPosition(AvatarIKGoal.RightHand, IK_handRight.transform.position);
            //_anim.SetIKRotation(AvatarIKGoal.RightHand, IK_handRight.transform.rotation);

            //ç∂éË
            _anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            _anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
            _anim.SetIKPosition(AvatarIKGoal.LeftHand, IK_handLeft.transform.position);
            //_anim.SetIKRotation(AvatarIKGoal.RightHand, IK_handLeft.transform.rotation);
        }
    }

    private void FixedUpdate()
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
}
