using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // ต้องมีบรรทัดนี้เพื่อเปลี่ยนด่าน

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public TextMeshProUGUI scoreText;
    public GameObject winTextObject;
    public GameObject gameOverPanel; 
    public GameObject playAgainButton;

    private Rigidbody rb;
    private int count;
    public int totalItems = 10; 

    void Start() {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetScoreText();

        // ใส่ if เช็คก่อนว่ามีของไหม ถ้ามีค่อยสั่งปิด
        if (winTextObject != null) {
            winTextObject.SetActive(false);
        }
        
        if (gameOverPanel != null) {
            gameOverPanel.SetActive(false);
        }
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
            GameOver(); // เรียกใช้ฟังก์ชันเดิมที่เราทำจอดำไว้ จะได้ดูเป็นระบบเดียวกันครับ
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
        // เช็คก่อนว่าลาก WinText มาใส่หรือยัง
        if (winTextObject != null) {
            winTextObject.SetActive(true);
        }

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
    // ฟังก์ชันสำหรับกดปุ่มย้อนกลับไปด่านแรก
    public void GoToFirstScene() {
        // ใส่ชื่อ Scene แรกของคุณลงไป (เช่น "SampleScene")
        SceneManager.LoadScene("SampleScene"); 
    }
}