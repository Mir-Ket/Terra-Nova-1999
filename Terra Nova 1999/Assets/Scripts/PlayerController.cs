using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
   
  
    [Header("Vector Settings")]
    private float _verticalSpeed;
    private float _horizontalSpeed;
    private Vector3 _moveDirection;

    [Header("Look Settings")]
    [SerializeField] float _lookLimitX=60;// fps oyunlarý için 60 veya 45 tir
    [SerializeField] float _lookSpeed;// fare hasasiyeti
    private float _rotationX;// x eksenindeki rotasyonnumuz kamera ve gövdeyi birlikte döndürmek için


    [Header("References Settings")]
    private Camera _camera;
    private CharacterController _characterController;


    [Header("Move Settings")]
    [SerializeField] float _walkSpeed;
    [SerializeField] float _runSpeed;
    [SerializeField] bool _isruning;

    [Header("Jump Settings")]
    [SerializeField] float _jumpForce;
    [SerializeField] float _gravity;
    [SerializeField] float _gravityDirectionPower;




    [SerializeField] Animator _anim;
    private void Awake()
    {
        // refarans alýyoruz
        _camera = Camera.main;
        _characterController = GetComponent<CharacterController>();
   

        // fareyi kililiyoruz ve gizliyoruz
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovementCalculate();
        Jump();
        Look();

        // karakterin y sini yani aþaðý yukarý deðerini gravityi direction powera eþitliyoruz eðer zýplamýyorsak yani gravity etkili olacak ama y deðeri 0 olacak
        _moveDirection.y = _gravityDirectionPower;

        // hareket veriyoruz time delta time olmadan yapma yoksa karakter uçar
        _characterController.Move(_moveDirection * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _anim.SetTrigger("Shoot");
           
        }
    }

    private void MovementCalculate()
    {
        // local transformun ileri geri saðý solunu alýyor
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // hareket hýzýmýzý baktýðýmýz yöne eþitliyoruz
        _moveDirection = (forward * _verticalSpeed) + (right * _horizontalSpeed);


        // koþma kontrolu
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _isruning = true;
            _anim.SetBool("Run", true);

        }
        else
        {
            _isruning = false;
            _anim.SetBool("Run", false);

        }
        // koþmuyorsa yürütme kontrolü
        if (_isruning)
        {
            _verticalSpeed = _runSpeed * Input.GetAxis("Vertical");
            _horizontalSpeed = _runSpeed * Input.GetAxis("Horizontal");
        }
        else
        {
            _verticalSpeed = _walkSpeed * Input.GetAxis("Vertical");
            _horizontalSpeed = _walkSpeed * Input.GetAxis("Horizontal");
        }
    }
    private void Jump()
    {
        if (_characterController.isGrounded)
        {
          ;

            // graviti direction power aslýnda yer çekiminin tersine hareket uyguluyor bunu jump forcetan alýyor gravity direction powerý gravitiden çýakrýnca zýplýyor kýsaca
            _gravityDirectionPower = -_gravity * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _gravityDirectionPower = _jumpForce;
            }
        }
        else
        {
            _gravityDirectionPower -= _gravity * Time.deltaTime;
        }
    }
    private void Look()
    {
        _rotationX += -Input.GetAxis("Mouse Y") * _lookSpeed;
        _rotationX = Mathf.Clamp(_rotationX, -_lookLimitX, _lookLimitX);
        _camera.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);

        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * _lookSpeed, 0);
    }

}
