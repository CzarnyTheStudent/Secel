using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Vector3 startPoint; // Początkowy punkt docelowy
    public Vector3 endPoint; // Końcowy punkt docelowy
    public float speed = 5f; // Prędkość poruszania się postaci
    private bool isMovingToEnd = true; // Flaga określająca, czy postać porusza się w kierunku końcowego punktu

    private void Update()
    {
        // Wybór bieżącego punktu docelowego na podstawie flagi
        Vector3 currentTarget = isMovingToEnd ? endPoint : startPoint;

        // Obliczanie kierunku i dystansu do bieżącego punktu docelowego
        Vector3 direction = currentTarget - transform.position;
        float distance = direction.magnitude;

        // Poruszanie się postaci w kierunku punktu docelowego z zadaną prędkością
        transform.Translate(direction.normalized * (speed * Time.deltaTime));

        // Jeżeli postać osiągnęła wystarczająco blisko punktu docelowego, zmień kierunek
        if (distance <= 0.1f)
        {
            isMovingToEnd = !isMovingToEnd;
        }
    }
}