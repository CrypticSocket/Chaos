using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float movementSpeed = 10f;
    [SerializeField]
    private float mouseSensitivityHorizontal = 5f;
    [SerializeField]
    private float mouseSensitivityVertical = 5f;
    [SerializeField]
    private float jumpForce = 50f;


    private PlayerMotor motor;

    // Start is called before the first frame update
    void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        float _xmov = Input.GetAxisRaw("Horizontal");   //Left right
        float _zmov = Input.GetAxisRaw("Vertical");     //Front back

        Vector3 _movHorizontal = transform.right * _xmov;
        Vector3 _movVertical = transform.forward * _zmov;

        Vector3 velocity = (_movHorizontal + _movVertical).normalized * movementSpeed;
        motor.Move(velocity);

        float _yrot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0, _yrot, 0) * mouseSensitivityHorizontal;

        motor.LookHorizontal(_rotation);

        float _xrot = Input.GetAxisRaw("Mouse Y");
        Vector3 _cameraRotation = new Vector3(_xrot, 0, 0) * mouseSensitivityVertical;
        motor.LookVertical(_cameraRotation);

        float _jumpY = Input.GetAxisRaw("Jump");
        Vector3 _jumpForce = new Vector3(0, _jumpY, 0) * jumpForce;
        motor.Jump(_jumpForce);

    }
}
