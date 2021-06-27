using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    #region Public Variables
    public WheelCollider frontWheelLeft, frontWheelRight;
    public WheelCollider backWheelLeft, backWheelRight;
    public Transform frontWheelLeftT, frontWheelRightT;
    public Transform backWheelLeftT, backWheelRightT;

    public float maxSteerAngle = 30;
    public float motorForce = 50;

    #endregion

    #region Private Variables
    Rigidbody _rigidbody;



    float _horizontalInput;
    float _verticalInput;
    float _steeringAngle;



    #endregion


    #region MonoBehaviour
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        GetInput();
        Steer();
        Accelerate();
        UpdateWheelPoses();
    }
    #endregion


    public void GetInput()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
    }

    private void Steer()
    {
        _steeringAngle = maxSteerAngle * _horizontalInput;
        frontWheelLeft.steerAngle = _steeringAngle;
        frontWheelRight.steerAngle = _steeringAngle;
    }

    private void Accelerate()
    {
        frontWheelLeft.motorTorque = _verticalInput * motorForce;
        frontWheelRight.motorTorque = _verticalInput * motorForce;
    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(frontWheelLeft, frontWheelLeftT);
        UpdateWheelPose(frontWheelRight, frontWheelRightT);
        UpdateWheelPose(backWheelLeft, backWheelLeftT);
        UpdateWheelPose(backWheelRight, backWheelRightT);

    }

    private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 pos = _transform.position;
        Quaternion quat = _transform.rotation;

        _collider.GetWorldPose(out pos, out quat);
        _transform.position = pos;
        _transform.rotation = quat;
    }
}
