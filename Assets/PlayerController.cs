using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // ต้องมีบรรทัดนี้เพื่อเปลี่ยนด่าน

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public TextMeshProUGUI scoreText;
    public GameObject winTextObject;
    public GameObject gameOverPanel; // ลากหน้า GameOver มาใส่ที่นี่

    private Rigidbody rb;
    private int count;
    public int totalItems = 10; // ตั้งจำนวนเหรียญที่ต้องเก็บให้ครบ

    void Start() {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetScoreText();
        winTextObject.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    void Update() {
        float moveH = Input.GetAxis("Horizontal");
        float moveV = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveH, 0.0f, moveV);
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other) 
    {
        
        // 1. เก็บไอเทม
        if (other.gameObject.CompareTag("Item")) 
        {
            other.gameObject.SetActive(false);
            count++;
            SetScoreText();
        }

        // 2. ตกเหว หรือ ชนโซนอันตราย (Hazard)
        if (other.gameObject.CompareTag("Hazard")) 
        {
            // ถ้าชน Hazard ให้เริ่มด่านนั้นใหม่ทันที (หรือจะสั่ง GameOver(); ก็ได้ครับ)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
    }

    // ฟังก์ชันตรวจจับการชน "อุปสรรค"
    void OnCollisionEnter(Collision collision) {
        // บรรทัดนี้จะตะโกนบอกเราว่า "ฉันชนกับวัตถุชื่อ... และมันมี Tag ชื่อว่า..."
        Debug.Log("ชนกับวัตถุ: " + collision.gameObject.name + " | Tag ที่ตรวจเจอคือ: " + collision.gameObject.tag);

        if (collision.gameObject.CompareTag("Obstacle")) {
            Debug.Log("ยืนยัน! Tag ตรงกับคำว่า Obstacle กำลังจะเรียก GameOver()");
            GameOver();
        }
    }

    void SetScoreText() {
        scoreText.text = "Score: " + count.ToString();
        if (count >= totalItems) {
            Win();
        }
    }

    void Win() {
        winTextObject.SetActive(true);
        // เช็คว่าถ้าตอนนี้อยู่ด่านแรก ให้วาร์ปไปด่าน Final หลังจากชนะ 2 วินาที
        if (SceneManager.GetActiveScene().name != "FinalScene") {
            Invoke("LoadFinalScene", 2f);
        }
    }

    void LoadFinalScene() {
        SceneManager.LoadScene("FinalScene");
    }

    void GameOver() {
        gameOverPanel.SetActive(true);
        this.enabled = false; // ปิดการควบคุมตัวละคร
        rb.linearVelocity = Vector3.zero; // หยุดลูกบอลทันที
    }

    // ฟังก์ชันสำหรับกดปุ่ม Restart (เอาไปเชื่อมกับปุ่มใน UI)
    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}