using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private CharacterController _controller;
    private PlayerInput _playerInput;
    private InputAction _m_Jump;
    private InputAction _m_Left;
    private InputAction _m_Right;
    private InputAction _m_Crounch;
    private float _laneDistance;
    private float _jumpForce;
    public float _gravity;
    private Vector3 direction;
    private float _startSpeed;
    private float _forwardSpeed;
    private float _trackedDistance;
    private int _currentLane = 1;
    private float _initialHeight;

    private float trackedDistance = 0;

    public int CurrentLane
    {
        get { return _currentLane; }
        set { if (value < 0) _currentLane = 0; else if (value > 2) _currentLane = 2; else _currentLane = value; }
    } //0 = � gauche, 1= au milieu, 2= � droite

    public delegate void OnTileGenerated();
    public static event OnTileGenerated onTileGenerated;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _playerInput = GetComponent<PlayerInput>();
        attributeScheme();
        _m_Jump = _playerInput.actions["Jump"];
        _m_Left = _playerInput.actions["Left"];
        _m_Right = _playerInput.actions["Right"];
        _m_Crounch = _playerInput.actions["Crounch"];
        _initialHeight = _controller.height;

        _laneDistance = 2.5f;
        _jumpForce = 14f;
        _gravity = 30f;

        switch (LevelSelection.currentLevel)
        {
            case LevelSelector.Easy:
                _startSpeed = 30;
                break;
            case LevelSelector.Medium:
                _startSpeed = 40;
                break;
            case LevelSelector.Hard:
                _startSpeed = 50;
                break;
            case LevelSelector.Infinite:
                _startSpeed = 30;
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (LevelSelection.currentLevel == LevelSelector.Infinite)
        {
            Debug.Log(_forwardSpeed);
            _forwardSpeed = (float)(_startSpeed + (int)(trackedDistance / 100) * 0.5);
            trackedDistance += _forwardSpeed * Time.deltaTime;
        } else
        {
            _forwardSpeed = _startSpeed;
        }

        direction.z = _forwardSpeed;

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
                targetPosition += Vector3.left * _laneDistance;
                break;
            case 2:
                targetPosition += Vector3.right * _laneDistance;
                break;
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, 60);

        
    }

    private void FixedUpdate()
    {
        _controller.Move(direction * Time.fixedDeltaTime);

    }

    void attributeScheme()
    {

        _playerInput.SwitchCurrentControlScheme(SettingsUI.instance.GetCurrentControlScheme(), Keyboard.current);


    }

    private void PlayerJump()
    {
        direction.y = _jumpForce;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.CompareTag("Obstacle"))
        {
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

        if (hit.transform.CompareTag("Finish"))
        {
            GameManager.playerReachedFinishLine = true;
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
