using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 100f;

    private CharacterController controller;
    private Camera playerCamera;
    
    private float xRotation = 0f;
    private float gravity = -9.81f;
    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMouseLook();
        HandleMovement();
    }

    void HandleMouseLook()
    {
        // Получаем данные от мыши
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Поворачиваем игрока влево-вправо
        transform.Rotate(Vector3.up * mouseX);

        // Поворачиваем камеру вверх-вниз (с ограничением, чтобы не вывернуть шею)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    void HandleMovement()
    {
        // Получаем данные от клавиатуры (WASD)
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Рассчитываем направление движения относительно того, куда смотрит игрок
        Vector3 move = transform.right * x + transform.forward * z;

        // Двигаем персонажа
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Применяем гравитацию, чтобы персонаж не летал (если не на земле)
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Немного подтягиваем к земле
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}