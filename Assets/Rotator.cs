using UnityEngine;

public class Rotator : MonoBehaviour
{
    void Update()
    {
        // สั่งให้หมุนรอบตัวเอง โดยคูณ Time.deltaTime เพื่อให้หมุนนุ่มนวลเท่ากันทุกเครื่อง
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }
}