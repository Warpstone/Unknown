using UnityEngine;

public class ParallaxEffect: MonoBehaviour
{
    private float startTargetPosX;
    private float startLayerPosX;   
    
    [Header("Настройки")]
    public Transform cameraTransform; // Ссылка на камеру
    [Range(0f, 1f)]
    public float parallaxFactor;      // 0 - фон стоит на месте, 1 - фон привязан к камере
    
    void Start()
    {
        // Запоминаем начальные позиции
        if (cameraTransform == null) cameraTransform = Camera.main.transform;
        
        startTargetPosX = cameraTransform.position.x;
        startLayerPosX = transform.position.x;
    }
    
    void Update()
    {
        // Считаем, насколько сдвинулась камера
        float relativeDistance = cameraTransform.position.x - startTargetPosX;
        
        // Двигаем слой с учетом коэффициента
        transform.position = new Vector3(startLayerPosX + (relativeDistance * parallaxFactor), transform.position.y, transform.position.z);
    }
}
