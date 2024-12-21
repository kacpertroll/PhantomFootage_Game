using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public static InventoryManager Inventory;

    public bool mainGateCard = false;

    void Awake()
    {
        // Ustawia na start singletona jako instancjê
        if (Inventory == null)
        {
            Inventory = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
