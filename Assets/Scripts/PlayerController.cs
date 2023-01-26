using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float forwardSpeed = 12;
    public float laneDistance;
    public float jumpForce = 6;
    public float crounchSpeed = 3;

    private CharacterController _controller;
    private PlayerInput _playerInput;
    private InputAction _m_Jump;
    private InputAction _m_Left;
    private InputAction _m_Right;
    private InputAction _m_Crounch;
    private Vector3 direction;
    private int _currentLane = 1;
    private float _gravity = 9.807f;
    private float _initialHeight;

    public int CurrentLane
    {
        get { return _currentLane; }
        set { if (value < 0) _currentLane = 0; else if (value > 2) _currentLane = 2; else _currentLane = value; }
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
        _initialHeight = _controller.height;
    }

    // Update is called once per frame
    void Update()
    {
        direction.z = forwardSpeed;

        if (_controller.isGrounded)
        {
            if (_m_Jump.WasPressedThisFrame())
            {
                PlayerJump();
            }

            if (_m_Crounch.IsPressed())
            {
                _controller.height = 0.5f * _initialHeight;
            }
            else
            {
                _controller.height = Mathf.Lerp(_controller.height, _initialHeight, 5.0f * Time.deltaTime);
            }
        }
        else
        {
            direction.y -= _gravity * Time.deltaTime;
        }

        if (_m_Left.triggered)
        {
            CurrentLane--;
        }
        else if (_m_Right.triggered)
        {
            CurrentLane++;
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

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.CompareTag("Obstacle"))
        {
            Debug.Log("Touched");
            if (GameManager.restEsquive > 0)
            {
                GameManager.restEsquive--;
                StartCoroutine(Flasher());
                hit.collider.enabled = false;
            }
            else
            {
                GameManager.playerIsDied = true;
            }
        }
    }

    IEnumerator Flasher()
    {
        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();

        Color normalColor = meshRenderer.material.color;
        Color collideColor = Color.clear;

        for (int i = 0; i < 5; i++)
        {
            meshRenderer.material.color = collideColor;
            yield return new WaitForSeconds(.1f);
            meshRenderer.material.color = normalColor;
            yield return new WaitForSeconds(.1f);
        }
    }
}
