using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float forwardSpeed = 12;
    public float laneDistance;
    public float jumpForce = 6;


    private CharacterController _controller;
    private PlayerInput _playerInput;
    private InputAction _m_Jump;
    private InputAction _m_Left;
    private InputAction _m_Right;
    private InputAction _m_Crounch;
    private Vector3 direction;
    private int _currentLane = 1;
    private float gravity = 9.807f;
    public int CurrentLane
    {
        get { return _currentLane; }
        set{ if (value < 0) _currentLane = 0; else if (value > 2) _currentLane = 2; else _currentLane = value;}
    } //0 = à gauche, 1= au milieu, 2= à droite

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _playerInput = GetComponent<PlayerInput>();
        _m_Jump = _playerInput.actions["Jump"];
        _m_Left = _playerInput.actions["Left"];
        _m_Right = _playerInput.actions["Right"];
        _m_Crounch = _playerInput.actions["Crounch"];
    }

    // Update is called once per frame
    void Update()
    {
        direction.z = forwardSpeed;

        if (_controller.isGrounded)
        {
            if (_m_Jump.triggered)
            {
                Debug.Log("YESS");
                PlayerJump();
            }
        } else
        {
            direction.y -= gravity * Time.deltaTime;
        }
        
        if (_m_Left.triggered)
        {
            CurrentLane--;
        }
        else if (_m_Right.triggered)
        {
            CurrentLane++;
        }
        else if (_m_Crounch.triggered)
        {

        }

        Vector3 targetPosition = (transform.position.z * transform.forward) + (transform.position.y * transform.up);


        switch (CurrentLane)
        {

            case 0:
                targetPosition += Vector3.left * laneDistance;
                break;
            case 2:
                targetPosition += Vector3.right * laneDistance;
                break;
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, 60);
    }

    private void FixedUpdate()
    {
        _controller.Move(direction * Time.fixedDeltaTime);

    }

    private void PlayerJump()
    {
        direction.y = jumpForce;
    }

    private void PlayerCrounch()
    {

    }
    
}
