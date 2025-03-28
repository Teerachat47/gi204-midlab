using UnityEngine;

public class PickUpSystem : MonoBehaviour
{
    public Transform holdPosition;  // ตำแหน่งที่ถือของ
    public GameObject gunPrefab;    // ปืนที่ต้องการให้เกิด
    public float pickUpRange = 3f;  // ระยะที่หยิบของได้
    public LayerMask pickUpLayer;   // กำหนดเฉพาะ Layer ที่สามารถหยิบได้
    public Transform gunPosition;

    private GameObject heldObject;  // เก็บอ้างอิงของที่หยิบ
    private Rigidbody heldRb;       // Rigidbody ของของที่หยิบ
    private GameObject currentGun;  // เก็บอ้างอิงของปืนที่ถือ

    void Start()
    {
        EquipGun();  // เรียกฟังก์ชั่นเพื่อให้ปืนเกิดในมือของตัวละครเมื่อเริ่มเกม
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))  // กด E เพื่อหยิบหรือปล่อยของ
        {
            if (heldObject == null)
            {
                TryPickUp();
            }
            else
            {
                DropObject();
            }
        }
    }

    void TryPickUp()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, pickUpRange, pickUpLayer))
        {
            Debug.Log("Hit Object: " + hit.collider.gameObject.name);  // ดูว่าวัตถุถูก Raycast จับหรือไม่
            heldObject = hit.collider.gameObject;
            heldRb = heldObject.GetComponent<Rigidbody>();

            if (heldRb != null)
            {
                heldRb.useGravity = false;
                heldRb.isKinematic = true;
                heldObject.transform.position = holdPosition.position;
                heldObject.transform.parent = holdPosition;
            }
        }
        else
        {
            Debug.Log("No Object Detected");  // ถ้าไม่มีวัตถุในระยะ
        }
    }

    void DropObject()
    {
        if (heldObject != null)
        {
            heldRb.useGravity = true;
            heldRb.isKinematic = false;
            heldObject.transform.parent = null;
            heldObject = null;
            heldRb = null;
        }
    }

    // ฟังก์ชั่นสำหรับให้ปืนเกิดในมือของตัวละคร
    
    void EquipGun()
    {
        if (gunPrefab != null && currentGun == null)
        {
            currentGun = Instantiate(gunPrefab, gunPosition.position, gunPosition.rotation);  
            currentGun.transform.SetParent(gunPosition);  
        }
    }

}
