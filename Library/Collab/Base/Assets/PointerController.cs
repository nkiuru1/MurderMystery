using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PointerController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
	private bool PointerDown = false;

	public void OnPointerDown(PointerEventData eventData) {
		PointerDown = true;

		Button BtnMap = GameObject.Find ("BtnMap").GetComponent<Button> ();
		Button BtnTalk = GameObject.Find ("BtnTalk").GetComponent<Button> ();
		Button BtnNotebook = GameObject.Find ("BtnNotebook").GetComponent<Button> ();
		Button BtnSearch = GameObject.Find ("BtnSearch").GetComponent<Button> ();
		Button BtnBack = GameObject.Find ("BtnBack").GetComponent<Button> ();

		switch (eventData.pointerPress) {
		case BtnSearch:
			BtnMap.gameObject.SetActive (false);
			BtnTalk.gameObject.SetActive (false);
			BtnNotebook.gameObject.SetActive (false);
			BtnSearch.gameObject.SetActive (false);
			BtnMap.gameObject.SetActive (false);
			BtnTalk.gameObject.SetActive (true);
			break;
		}
	}

	public void OnPointerUp(PointerEventData eventData) {
		PointerDown = false;
	}

	public bool GetPointerDown() {
		return PointerDown;
	}
}