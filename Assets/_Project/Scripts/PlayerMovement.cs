using UnityEngine;
using UnityEngine.InputSystem; // Подключаем новую систему ввода

public class PlayerMovement: MonoBehaviour
{
    [Header("Настройки движения")]
    public float moveSpeed = 5f;     // Скорость ходьбы
    public float jumpForce = 10f;    // Сила прыжка
    
    private Rigidbody2D rb;          // Ссылка на компонент физики
    private float horizontalInput;   // Значение по оси А-D (влево-вправо)
    
    // Метод Start запускается один раз при старте игры
    void Start()
    {
        // Ищем Rigidbody2D на этом же объекте
        rb = GetComponent<Rigidbody2D>();
    }
    
    // Этот метод вызывается автоматически Input System (нужно будет настроить в Unity)
    public void OnMove(InputAction.CallbackContext context)
    {
        // Читаем значение как Vector2
        Vector2 moveVector = context.ReadValue<Vector2>();
        horizontalInput = moveVector.x;
    }
    
    public void OnJump(InputAction.CallbackContext context)
    {
        // context.started — это момент самого первого нажатия кнопки
        if (context.started)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }
    
    // Update работает каждый кадр. Но для физики лучше использовать FixedUpdate
    void FixedUpdate()
    {
        // Двигаем тело: по X — наш ввод, по Y — оставляем текущую скорость падения
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
    }
}
