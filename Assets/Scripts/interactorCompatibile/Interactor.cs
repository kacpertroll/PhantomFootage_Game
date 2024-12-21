using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Interact();
}

public interface IInteractableHold : IInteractable
{
    void InteractHold();
}


public class Interactor : MonoBehaviour
{
    public static Interactor Interact;

    public Transform InteractorSource;
    public float InteractRange;
    private float holdThreshold = 0.3f; // Czas, po kt�rym uznajemy przytrzymanie

    private float holdStartTime;
    private bool isHolding = false;
    private bool holdInvoked = false;

    private IInteractableHold currentInteractableHold;

    void Update()
    {
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.E)) // Sprawdzamy, czy klawisz E zosta� wci�ni�ty
        {
            holdStartTime = Time.time;
            isHolding = true;
            holdInvoked = false;

            // Wykonujemy raycast
            
            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    // Przypisujemy obiekt interakcji, aby m�c p�niej wywo�a� InteractHold
                    if (interactObj is IInteractableHold interactableHold)
                    {
                        currentInteractableHold = interactableHold;
                    }
                }
            }
        }

        // Sprawdzamy, czy klawisz E jest przytrzymany
        if (isHolding && !holdInvoked)
        {
            if (Time.time - holdStartTime >= holdThreshold && currentInteractableHold != null)
            {
                currentInteractableHold.InteractHold();
                holdInvoked = true; // Zaznaczamy, �e wywo�ali�my InteractHold
            }
        }

        if (Input.GetKeyUp(KeyCode.E)) // Sprawdzamy, czy klawisz E zosta� zwolniony
        {
            isHolding = false;
            
            if (!holdInvoked) // Je�li nie wywo�ano InteractHold, wykonaj zwyk�� interakcj�
            {

                if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
                {
                    if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                    {
                        interactObj.Interact();
                    }
                }
            }
            // Resetujemy currentInteractableHold
            currentInteractableHold = null;
        }
    }
}