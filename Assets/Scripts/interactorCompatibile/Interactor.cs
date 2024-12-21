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
    private float holdThreshold = 0.3f; // Czas, po którym uznajemy przytrzymanie

    private float holdStartTime;
    private bool isHolding = false;
    private bool holdInvoked = false;

    private IInteractableHold currentInteractableHold;

    void Update()
    {
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.E)) // Sprawdzamy, czy klawisz E zosta³ wciœniêty
        {
            holdStartTime = Time.time;
            isHolding = true;
            holdInvoked = false;

            // Wykonujemy raycast
            
            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    // Przypisujemy obiekt interakcji, aby móc póŸniej wywo³aæ InteractHold
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
                holdInvoked = true; // Zaznaczamy, ¿e wywo³aliœmy InteractHold
            }
        }

        if (Input.GetKeyUp(KeyCode.E)) // Sprawdzamy, czy klawisz E zosta³ zwolniony
        {
            isHolding = false;
            
            if (!holdInvoked) // Jeœli nie wywo³ano InteractHold, wykonaj zwyk³¹ interakcjê
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