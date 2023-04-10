using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;            // kierunek ruchu (-1 to w lewo, 0 brak, 1 w prawo)
    private float speed = 8f;            // wartość przez jaką pomnożymy bazowy ruch by uzyskać większą prędkość
    private float jumpingPower = 16f;    // siła skoku
    private bool isFacingRight = true;   // bool w którym zawarta jest informacja o kierunku zwrócenia modelu
    List<Collider2D> inColliders = new List<Collider2D>(); //Lista Obiektów kolidujących. Dodał Kamil

    private bool canDash = true;         // wskaźnik który zezwala znów wykonać dash
    private bool isDashing;              // wskaźnik, by określić czy obiekt jest w trakcie dasha (np by nie odnosił obrażeń)
    private float dashingPower = 24f;    // odległość na jaką odbywa się dash
    private float dashingTime = 0.2f;    // czas ile trwa dash
    private float dashingCooldown = 1f;  // co ile można wykonać dash

    [SerializeField] private Rigidbody2D rb;           //Rigibody to komponent fizyki unity, zawietający komendy i wskazujący że dany obiekt podlega
                                                       // silnikowi fizycznemu
    [SerializeField] private Transform groundCheck;    // pozycja obiektu (pustego) który sprawdza czy obiekt jest na podłożu
    [SerializeField] private LayerMask groundLayer;    //ustalenie które obiekty na mapie są podłożem
    [SerializeField] private TrailRenderer tr;         //komponent, który pozwoli dodać do dasha efekt smugi za postacią


    private void Update()
    {
        if (isDashing)               // jeśli postać dash'uje fizyka na nią nie działa, sprawdź w następnej klatce
        {
            return;
        }


        
        horizontal = Input.GetAxisRaw("Horizontal");    // pobranie kierunku ruchu

        if (Input.GetButtonDown("Jump") && IsGrounded())    // gdy postać jest na ziemi i klikniemy przycisk skoku...
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);   // ... fizyka naszej postaci przyjmie nowy ruch, który dokłada ruch do góry
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)         // funkcja dodana, aby przy krótkim kliknięciu skok był mniejszy
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)      // uruchomienie dash'a
        {
            StartCoroutine(Dash());
        }

        Flip();        //
    }

    private void FixedUpdate()              // Update który może odpalać się częściej by przeliczać fizykę
    {
        if (isDashing)                 // Podczas dash'a na postać nie działa fizyka
        {
            return;
        }

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);      //... ale gdy nie dashuje, porusza się w podanym kierunku z prędkością speed
    }

    private bool IsGrounded()                 //sprawdzanie czy pozycja groundCheck jest w małym promieniu od groundLayer (czyli czy obiekt jest na ziemi)
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()         // odwrócenie modelu w lewo, gdy ruszamy się w lewo
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private IEnumerator Dash()                  // corutine czyli operacja rozłożona na wiele klatek
    {
        canDash = false;                                                       // gdy dashuje nie może deszować znowu
        isDashing = true;                                                      
        float originalGravity = rb.gravityScale;                               //przechowanie grawitacji sprzed dasha
        rb.gravityScale = 0f;                                                  //ustawienie grawitacji na czas dasha na 0
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);  //ustawienie prędkości dasha na ustawioną noc
        tr.emitting = true;                                                    //ślad za postacią podczas dasha
        yield return new WaitForSeconds(dashingTime);                          //trwanie dasha w czasie którego się właści' śladwości nie zmieniają
        tr.emitting = false;                                                   //gdy minie czas dasha przestaje "wydzielać     
        rb.gravityScale = originalGravity;                                     // wraca grawitacja
        isDashing = false;                                                     //koniec dasha
        yield return new WaitForSeconds(dashingCooldown);                      //odczekuje cooldown
        canDash = true;                                                        //dopiero znowu może dashować
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        inColliders.Add(col);  // Przy kontakcie dodaje do listy
        inColliders.ForEach(n => n.SendMessage("Use", SendMessageOptions.DontRequireReceiver));
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        inColliders.Remove(col);  // Przy wyjściu odejmuje od listy
    }
}