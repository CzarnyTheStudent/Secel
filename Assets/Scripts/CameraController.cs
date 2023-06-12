using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float zoomSpeed = 2f;  // Szybkość przybliżania/oddalania
    public float moveSpeed = 5f;  // Szybkość poruszania się kamery

    private Vector3 defaultPosition;
    private float defaultZoom;
    private bool isMovingEnabled = true;  // Czy poruszanie się kamery jest włączone
    
    
    //TO ŻE NIE MA OGRANICZENIA CO DO ODDALANIA I PRZYBLIŻANIA JEST SPECJALNIE. UWIERZ PROSZĘ
    //Ale tak na serio. Bawiliśmy się tą kamerą i uznaliśmy że nie ma sensu tego ograniczać, można się dobrze przy tym bawić w grze 

    void Start()
    {
        defaultPosition = transform.position;
        defaultZoom = Camera.main.orthographicSize;
    }

    void Update()
    {
        // Przybliżanie i oddalanie kamery za pomocą klawiszy "+/-"
        float zoomInput = Input.GetKey("=") ? 1f : Input.GetKey("-") ? -1f : 0f;
        ZoomCamera(zoomInput);

        // Przesuwanie kamery za pomocą strzałek
        if (isMovingEnabled)
        {
            float horizontalInput = 0f;
            float verticalInput = 0f;

            if (Input.GetKeyDown(KeyCode.LeftArrow))
                horizontalInput = -1f;
            else if (Input.GetKeyDown(KeyCode.RightArrow))
                horizontalInput = 1f;

            if (Input.GetKeyDown(KeyCode.DownArrow))
                verticalInput = -1f;
            else if (Input.GetKeyDown(KeyCode.UpArrow))
                verticalInput = 1f;

            MoveCamera(horizontalInput, verticalInput);
        }
        // Resetowanie kamery do normalnej pozycji za pomocą klawisza "Tab"
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ResetCameraPosition();
            ResetCameraZoom();
        }
    }

    void ZoomCamera(float zoomInput)
    {
        // Zmiana wielkości pola widzenia kamery
        Camera.main.orthographicSize += zoomInput * zoomSpeed * Time.deltaTime;

        // Ograniczenie minimalnej wartości przybliżenia
        Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize, 1f);
    }

    void MoveCamera(float horizontalInput, float verticalInput)
    {
        // Przesunięcie kamery z wygładzeniem
        Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0f).normalized;
        Vector3 targetPosition = transform.position + moveDirection * (moveSpeed * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.5f);
    }
    
    void ResetCameraPosition()
    {
        // Resetowanie pozycji kamery do wartości początkowej
        transform.position = defaultPosition;
    }
    
    void ResetCameraZoom()
    {
        // Resetowanie przybliżenia kamery do wartości początkowej
        Camera.main.orthographicSize = defaultZoom;
    }

    public void ToggleMoving()
    {
        // Przełączenie poruszania się kamery
        isMovingEnabled = !isMovingEnabled;
    }
}