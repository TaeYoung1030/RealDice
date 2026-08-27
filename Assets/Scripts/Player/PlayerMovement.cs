using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    Collider col;

    //플레이어 기본 이동속도
    [Header("Move")]
    [SerializeField] float moveSpeed =12f;
    [SerializeField] float jumpHeight = 2f;
    [SerializeField] float gravity = -9.81f;
    float h;
    float v;

    //마우스 감도 설정
    [Header("Mouse")]
    [SerializeField] float mouseSpeed;

    [Header("Camera")]
    [SerializeField] Transform cameraTransform;

    //시작하고서 화면이 자동적으로 돌아가는 현상 방지
    [Header("MouseControl")]
    [SerializeField] float warmupTime = 0.2f;

    float yRotation;
    float xRotation;

    float mouseX;
    float mouseY;

    float isRunning = 1.0f;

    Coroutine checkCor;

    CharacterController controller;
    Vector3 velocity;

    Camera cam;
    float moveState = 0;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   // 마우스 커서를 화면 안에서 고정
        Cursor.visible = false;                     // 마우스 커서를 보이지 않도록 설정

        cam = Camera.main;                          // 메인 카메라를 할당


    }
    private void Update()
    {
        if (warmupTime > 0.0f) //마우스 시작후 돌아가는거 방지
        {
            warmupTime -= Time.deltaTime;
            return;
        }
        Rotate();
        Move();


    }


    void Rotate()
    {
        mouseX = Input.GetAxisRaw("Mouse X") * mouseSpeed * Time.deltaTime;
        mouseY = Input.GetAxisRaw("Mouse Y") * mouseSpeed * Time.deltaTime;

        yRotation += mouseX;    // 마우스 X축 입력에 따라 수평 회전 값을 조정
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);             // 플레이어 캐릭터의 회전을 조절
        transform.Rotate(Vector3.up * mouseX);
    }

    void Move()
    {
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * x + transform.forward * y;
        controller.Move(move * moveSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            // 물리 공식: v = sqrt(h * -2 * g)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // 중력 적용
        velocity.y += gravity * Time.deltaTime;

        // 중력에 의한 낙하 이동 적용
        controller.Move(velocity * Time.deltaTime);

    }

}
