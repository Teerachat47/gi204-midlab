using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage = 2;  // ความเสียหายของกระสุน
    [SerializeField] private float lifetime = 5f;  // อายุของกระสุนในเกม

    void Start()
    {
        Destroy(gameObject, lifetime);  // ทำลายกระสุนหลังจากเวลาที่กำหนด
    }

    // ฟังก์ชันนี้จะถูกเรียกเมื่อกระสุนชนกับอะไรบางอย่าง
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))  // ตรวจสอบว่าเป็นศัตรู
        {
            Enemy enemy = collision.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);  // ส่งความเสียหายไปให้ศัตรู
            }
            Destroy(gameObject);  // ทำลายกระสุนหลังจากชน
        }
    }
}
