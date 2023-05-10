using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCharacterMovement : MonoBehaviour
{
    //private
    public CharacterController cc;
    float fallSpeed;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject[] _vCams = new GameObject[2];

    int cci; // CurrentCameraIndex

    [Header("Settings")]
    public float movementSpeed = 6f;
    public float jumpForce = 5f;
    public float gravity = 9.8f;
    public int maxJumps = 2;


    bool _orientObjectToCamera = true;
    public bool orientObjectToCamera { get { return _orientObjectToCamera; } set { _orientObjectToCamera = value; } }
    
    bool _canMove = true;
    public bool canMove { get { return _canMove; } set { _canMove = value; } }

    bool _canJump = true;
    public bool canJump { get { return _canJump; } set { _canJump = value; } }

    bool _jumpPass = false;
    public bool jumpPass { get { return _jumpPass; } set { _jumpPass = value; } }

    bool _gravityEnabled = true;
    public bool gravityEnabled { get { return _gravityEnabled; } set { _gravityEnabled = value; } }


    // Start is called before the first frame update
    void Awake()
    {
        MyGameManager.Instance.charRef = this.gameObject;
        cc = GetComponent<CharacterController>();
        SetVCamActiveAtIndex(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(_canMove)
            Move();
        if (_canJump)
            Jump();
        if(gravityEnabled)
            Gravity();

        if(_orientObjectToCamera)
        {
            OrientCharacterToCamera();
        }

        CycleCameras();
    }

    private void Move()
    {
        float horizontal = Input.GetAxisRaw(GameConstants.k_AxisNameHorizontal);
        float vertical = Input.GetAxisRaw(GameConstants.k_AxisNameVertical);

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        direction = direction.x * transform.right + direction.z * transform.forward;

        cc.Move(direction * movementSpeed * Time.deltaTime);
    }

    int jumpCount;
    private void Jump()
    {
        if(cc.isGrounded || _jumpPass)
        {
            jumpCount = 0;
        }
        
        if (jumpCount < maxJumps)
        {
            if(Input.GetButtonDown("Jump"))
            {
                jumpCount++;
                fallSpeed = jumpForce;
            }
            
        }
        
    }

    private void Gravity()
    {
        fallSpeed += -gravity * Time.deltaTime;
        cc.Move(fallSpeed * Time.deltaTime * transform.up);
    }

    void OrientCharacterToCamera()
    {
        transform.rotation = Quaternion.Euler(0, mainCamera.transform.rotation.eulerAngles.y, 0);
    }

    void CycleCameras()
    {
        if(Input.GetButtonDown(GameConstants.k_ChangeView))
        {
            if(cci >= _vCams.Length - 1)
            {
                cci = 0;
            }
            else
            {
                cci++;
            }
            SetVCamActiveAtIndex(cci);
        }
    }

    void SetVCamActiveAtIndex(int index)
    {
        for (int i = 0; i < _vCams.Length; i++)
        {
            if(i != index)
            {
                _vCams[i].SetActive(false);
            }
        }
        cci = index;
        _vCams[cci].SetActive(true);
    }
}
