using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    public float smallMedicBoxes = 0;
    public TextMeshProUGUI medkitInvientoryUI;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        medkitInvientoryUI.text = smallMedicBoxes.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "smallMedicBox") {
            smallMedicBoxes += 1;
            Destroy(collision.gameObject, 0.1f);
            medkitInvientoryUI.text = smallMedicBoxes.ToString();
        }
    }
}