using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;  // กระสุนที่ยิง
    [SerializeField] private float force;              // แรงที่ยิงกระสุน
    [SerializeField] private Transform firePoint;      // ตำแหน่งปากปืน
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // คลิกเมาส์ซ้ายเพื่อยิง
        {
            Shooting();
        }
    }

    void Shooting()
    {
        // สร้างกระสุนจากตำแหน่งปากปืน
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        
        // เพิ่มแรงไปข้างหน้า (ในทิศทางที่ปากปืนหันไป)
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.AddForce(firePoint.forward * force, ForceMode.Impulse);
    }
}
