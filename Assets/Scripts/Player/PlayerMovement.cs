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

    private bool isWallSliding;
    private float wallSlidingSpeed = 0.2f;

    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime= 0.1f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    private Vector2 wallJumpingPower = new Vector2(8f, 16f);

    private bool canDash = true;         // wskaźnik który zezwala znów wykonać dash
    public bool isDashing;              // wskaźnik, by określić czy obiekt jest w trakcie dasha (np by nie odnosił obrażeń)
    private float dashingPower = 24f;    // odległość na jaką odbywa się dash
    private float dashingTime = 0.2f;    // czas ile trwa dash
    private float dashingCooldown = 1f;  // co ile można wykonać dash

    [SerializeField] private Rigidbody2D rb;           //Rigibody to komponent fizyki unity, zawietający komendy i wskazujący że dany obiekt podlega
                                                       // silnikowi fizycznemu
    [SerializeField] private Transform groundCheck;    // pozycja obiektu (pustego) który sprawdza czy obiekt jest na podłożu
    [SerializeField] private LayerMask groundLayer;    //ustalenie które obiekty na mapie są podłożem
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private TrailRenderer tr;         //komponent, który pozwoli dodać do dasha efekt smugi za postacią
    [SerializeField] private Collider2D Col2D;

    [Header("PublicGameObject")]
    public Animator Player;

    private void Start()
    {
        Col2D = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (isDashing)               // jeśli postać dash'uje fizyka na nią nie działa, sprawdź w następnej klatce
        {
            return;
        }

        Player.SetBool("Running", false);

        horizontal = Input.GetAxisRaw("Horizontal");    // pobranie kierunku ruchu

        if (Input.GetButton("Horizontal"))   // Jeżeli klikasz A lub D To postać odpala animacje biegu
        {
            Player.SetBool("Running", true);
        }

        if (Input.GetButtonDown("Jump") && IsGrounded())    // gdy postać jest na ziemi i klikniemy przycisk skoku...
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);   // ... fizyka naszej postaci przyjmie nowy ruch, który dokłada ruch do góry
            Player.SetBool("Jump", true); //Skacze
        }

        if (rb.velocity.y < 0f)
        {
            Player.SetBool("Jump", false);
            Player.SetBool("Falling", true);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)         // funkcja dodana, aby przy krótkim kliknięciu skok był mniejszy
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
           
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)      // uruchomienie dash'a
        {
            StartCoroutine(Dash());
        }

        WallSlide();
        WallJump();

        if (!isWallJumping)
        {
             Flip();        
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player.SetBool("Falling", false);
    }

    private void FixedUpdate()              // Update który może odpalać się częściej by przeliczać fizykę
    {
        if (isDashing)                 // Podczas dash'a na postać nie działa fizyka
        {
            return;
        }

        if(!isWallJumping)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);      //... ale gdy nie dashuje, porusza się w podanym kierunku z prędkością speed
        }

    }

    private bool IsGrounded()                 //sprawdzanie czy pozycja groundCheck jest w małym promieniu od groundLayer (czyli czy obiekt jest na ziemi)
    {
        
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && horizontal != 0f)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private void Flip()         // odwrócenie modelu w lewo, gdy ruszamy się w lewo
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            //Player.SetBool("Running", true);
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    public IEnumerator Dash()                  // corutine czyli operacja rozłożona na wiele klatek
    {
        canDash = false;                                                       // gdy dashuje nie może deszować znowu
        isDashing = true;
        //Col2D.enabled;
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