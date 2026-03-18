using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed = 2f;    // ความเร็วในการเลื่อน
    public float distance = 5f; // ระยะทางที่จะเลื่อนไป-กลับ
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position; // จำค่าตำแหน่งเริ่มต้นไว้
    }

    void Update()
    {
        // ใช้คำสั่ง Mathf.PingPong เพื่อให้ค่าเด้งไปมาเหมือนลูกปิงปอง
        float movement = Mathf.PingPong(Time.time * speed, distance);
        transform.position = startPos + new Vector3(movement, 0, 0);
    }
}