using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ClickUnitInWorld : MonoBehaviour
{
    private IClickable unitCB;

    void Update()
    {
        PointerCleanUp();
        UpdateCursor();
        CheckForClickables();
    }

    private void PointerCleanUp()
    {
        if (unitCB != null && unitCB.Equals(null)) 
        {
            unitCB = null;
        }
    }

    private void UpdateCursor()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane + 10f;
        transform.position = Camera.main.ScreenToWorldPoint(mousePos);
    }

    private void CheckForClickables()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            IClickable temp = hit.transform.gameObject.GetComponent<IClickable>();
            if(temp != unitCB)
            {
                if(unitCB != null)
                {
                    unitCB.OnHoverExit();
                }
                unitCB = temp;
            }
            CheckForInteraction();
        }
        else if(unitCB != null)
        {
            unitCB.OnHoverExit();
            unitCB = null;
        }
    }

    private void CheckForInteraction()
    {
        if(unitCB != null)
        {
            if(Input.GetMouseButton(0))
            {
                unitCB.OnClick();
            }
            else
            {
                unitCB.OnHover();
            }
        }
    }
}
