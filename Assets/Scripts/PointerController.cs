using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PointerController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
    private bool PointerDown = false;

	public void OnPointerDown(PointerEventData eventData) {
        PointerDown = true;
	}

	public void OnPointerUp(PointerEventData eventData) {
		PointerDown = false;
	}
	public bool GetPointerDown() {
        if (PointerDown)
        {
            PointerDown = false;
            return true;
        }
        return false;
	}
}