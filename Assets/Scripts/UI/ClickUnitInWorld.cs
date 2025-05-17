using UnityEngine;

//Mouse event handling to detect if an IClickable has been hovered over or clicked on
public class ClickUnitInWorld : MonoBehaviour
{
    private IClickable unitCO;

    void Update()
    {
        PointerCleanUp();
        UpdateCursor();
        CheckForClickables();
    }

    //Clean up references to ClickableObjects on units that have been destroyed
    private void PointerCleanUp()
    {
        if (unitCO != null && unitCO.Equals(null))
        {
            unitCO = null;
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
            if (temp != unitCO)
            {
                if (unitCO != null)
                {
                    unitCO.OnHoverExit();
                }
                unitCO = temp;
            }
            CheckForInteraction();
        }
        else if (unitCO != null)
        {
            unitCO.OnHoverExit();
            unitCO = null;
        }
    }

    private void CheckForInteraction()
    {
        if (unitCO != null)
        {
            if (Input.GetMouseButton(0))
            {
                unitCO.OnClick();
            }
            else
            {
                unitCO.OnHover();
            }
        }
    }
}
