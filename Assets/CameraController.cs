using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;

    // เพิ่มตัวแปรความนุ่ม (ปรับเลขได้ ยิ่งน้อยยิ่งตามช้า/นุ่ม)
    public float smoothTime = 0.15f; 
    private Vector3 currentVelocity = Vector3.zero;

    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        // ตำแหน่งเป้าหมายที่กล้องควรจะไป
        Vector3 targetPosition = player.transform.position + offset;

        // ใช้ SmoothDamp เพื่อค่อยๆ เลื่อนกล้องจากจุดปัจจุบันไปจุดเป้าหมาย
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
    }
}