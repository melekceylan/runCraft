using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;    // Karakter hareket hızı
    public float jumpForce = 7f;   // Zıplama kuvveti
    public Camera mainCamera;      // Kamerayı bağlayacağımız değişken

    private Rigidbody rb;
    private Animator animator;
    private bool isGrounded;

    // Ground Check (yere temas kontrolü) için bir referans
    public Transform groundCheck;
    public LayerMask groundMask;

    private PlayerHealth playerHealth; // PlayerHealth referansı

    void Start()
{
    rb = GetComponent<Rigidbody>(); // Rigidbody bileşenini alıyoruz
    animator = GetComponent<Animator>();
    playerHealth = GetComponent<PlayerHealth>(); // PlayerHealth component'ini al

    // Eğer kamera atanmadıysa otomatik olarak ana kamerayı bul
    if (mainCamera == null)
    {
        mainCamera = Camera.main;
    }
}


    void Update()
    {
        Move();   // Hareket sistemi
        Jump();   // Zıplama sistemi
        CameraFollow(); // Kamera hareketi
    }

    void Move()
{
    // Klavyeden giriş değerlerini al
    float horizontal = Input.GetAxis("Horizontal"); // A/D veya sol/sağ ok tuşları
    float vertical = Input.GetAxis("Vertical");     // W/S veya yukarı/aşağı ok tuşları

    // Hareket yönünü hesapla
    Vector3 moveDirection = new Vector3(horizontal, 0f, vertical);

    // Kamera yönüne göre hareket ettir
    Vector3 cameraForward = mainCamera.transform.forward;
    Vector3 cameraRight = mainCamera.transform.right;

    cameraForward.y = 0f;
    cameraRight.y = 0f;

    Vector3 move = (cameraForward * vertical + cameraRight * horizontal).normalized;

    // Karakteri hareket ettir
    rb.MovePosition(rb.position + move * moveSpeed * Time.deltaTime);

    // Karakterin dönmesini kontrol et
    if (move != Vector3.zero) // Eğer bir hareket girdisi varsa
    {
        Quaternion targetRotation = Quaternion.LookRotation(move); // Hareket yönüne dön
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 3f * Time.deltaTime); // Daha yavaş bir dönüş (5f)
    }
    
    // Animasyon kontrolü
    bool isWalking = move != Vector3.zero; // Eğer hareket varsa koşuyor
    animator.SetBool("isWalking", isWalking); // Koşu animasyonunu tetikle

    // Idle animasyonuna geçiş için isWalking parametresi kullanılacak
    if (!isWalking)
    {
        animator.SetBool("isWalking", false); // Idle animasyonunu tetikle
    }
}



    void Jump()
{
    // Yere temas kontrolü
    isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, groundMask);

    if (isGrounded && Input.GetKeyDown(KeyCode.Space)) // Eğer yere temas ediyorsa ve SPACE tuşuna basılmışsa
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z); // Y eksenindeki mevcut hareketi sıfırla
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Zıplama kuvveti uygula
    }
}



   void CameraFollow()
{
    // Kameranın istenen pozisyonunu ayarla
    Vector3 offset = new Vector3(0, 0.25f, -0.35f); // Kamera biraz yukarıda ve karakterin arkasında
    Vector3 desiredPosition = transform.position + transform.rotation * offset; // Karakterin yönüne göre kamera pozisyonu


    // Kamera pozisyonunu ayarla
    mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, desiredPosition, 0.1f);

    // Kamera açısını ayarla (daha düşük bir açı)
    Quaternion targetRotation = Quaternion.Euler(10f, mainCamera.transform.eulerAngles.y, 10f);
    mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, targetRotation, 0.1f);

    // Kamerayı karaktere hafif eğimli baktır
    mainCamera.transform.LookAt(transform.position + new Vector3(0, 0.10f, 0));
}

// Pickup toplama için OnTriggerEnter ekle
void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("PickUp"))
    {
        Debug.Log("Pickup collected!"); // Debug için
        playerHealth.AddHealth(1);
        Destroy(other.gameObject); // Pickup'ı yok et
    }
}


}