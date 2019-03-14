using UnityEngine;
 using UnityEngine.UI;
 using UnityEngine.EventSystems;
 using System.Collections;

public class mainmenu_buttonhover : MonoBehaviour,
     IPointerEnterHandler,
     IPointerExitHandler {

	public GameObject img;

	public void OnPointerEnter(PointerEventData eventData)
    {
        img.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        img.SetActive(false);
    }
}
