using UnityEngine;
using UnityEngine.InputSystem; // Подключаем новую систему ввода

public class PlayerMovement: MonoBehaviour
{
    [Header("Настройки движения")]
    public float moveSpeed = 5f;     // Скорость ходьбы
    public float jumpForce = 10f;    // Сила прыжка
    
    private Rigidbody2D rb;          // Ссылка на компонент физики
    private Animator anim;           // Ссылка на аниматор
    private SpriteRenderer sprite;   // Ссылка на визуальную часть
    private float horizontalInput;   // Значение по оси А-D (влево-вправо)
    
    // Метод Start запускается один раз при старте игры
    void Start()
    {
        // Ищем Rigidbody2D на этом же объекте
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();                        // Находим Аниматор
        sprite = GetComponent<SpriteRenderer>();                // Находим Спрайт
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

    void Update()
    {
        float moveParam = Mathf.Abs(horizontalInput);                       // Передаем Скорость в Аниматор
        anim.SetFloat("Speed", moveParam);                             // Mathf.Abs делает значение всегда положительным (чтобы при беге влево -1 стало 1)
        
        if (horizontalInput > 0)                                            // Разворачиваем героя
            sprite.flipX = false;                                           // Смотрим вправо
        else if (horizontalInput < 0)
            sprite.flipX = true;                                            // Смотрим влево
        
        Debug.Log("Current Move Speed: " + moveParam);                      // Эта строка выводит значение в консоль Unity
    }
    
    // Update работает каждый кадр. Но для физики лучше использовать FixedUpdate
    void FixedUpdate()
    {
        // Двигаем тело: по X — наш ввод, по Y — оставляем текущую скорость падения
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
    }
}
