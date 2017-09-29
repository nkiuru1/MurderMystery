using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public GameObject BtnSearch, BtnNote, BtnMap, BtnTalk, BtnBack, BtnInv, Canvas;
    public Text DescriptionText;
    private GameObject ButtonSearch, ButtonNote, ButtonMap, ButtonTalk, ButtonBack;
    private Notebook PlayerNotebook;
    private Dictionary<Clue, GameObject> Clues = new Dictionary<Clue, GameObject>();

    private bool ButtonIsClicked(GameObject btn)
    {
        return btn.GetComponent<PointerController>().GetPointerDown();
    }

    public void CreateDefaultButtons(Notebook notebook)
    {
        ButtonSearch = Instantiate(BtnSearch);
        ButtonSearch.transform.SetParent(Canvas.transform, false);
        ButtonNote = Instantiate(BtnNote);
        ButtonNote.transform.SetParent(Canvas.transform, false);
        ButtonMap = Instantiate(BtnMap);
        ButtonMap.transform.SetParent(Canvas.transform, false);
        ButtonTalk = Instantiate(BtnTalk);
        ButtonTalk.transform.SetParent(Canvas.transform, false);
        ButtonBack = Instantiate(BtnBack);
        ButtonBack.transform.SetParent(Canvas.transform, false);
        ButtonBack.SetActive(false);
        this.PlayerNotebook = notebook;
    }

    public void Clicked()
    {

        if (ButtonSearch != null && ButtonIsClicked(ButtonSearch))
        {
            this.ChangeUI();
        }

        if (ButtonIsClicked(ButtonNote))
        {
            this.ChangeUI();
            int y = 0;
            foreach (Clue item in this.PlayerNotebook.GetClues())
            {
                GameObject obj = Instantiate(BtnInv);
                Vector2 pos = obj.transform.position;
                y += 40;
                pos.y = y;
                obj.transform.position = pos;
                obj.GetComponentInChildren<Text>().text = item.GetName();
                obj.transform.SetParent(Canvas.transform, false);

                Clues.Add(item, obj);
            }
        }
        if (ButtonMap != null && ButtonIsClicked(ButtonMap))
        {
            this.ChangeUI();
        }
        if (ButtonTalk != null && ButtonIsClicked(ButtonTalk))
        {
            this.ChangeUI();
        }
        if(ButtonBack != null && ButtonIsClicked(ButtonBack))
        {
            this.SetDefaultUI();
            foreach (Clue item in this.Clues.Keys)
            {
                this.Clues[item].SetActive(false);
            }
        }
    }

    private void SetDefaultUI()
    {
        ButtonMap.SetActive(true);
        ButtonNote.SetActive(true);
        ButtonSearch.SetActive(true);
        ButtonTalk.SetActive(true);

        ButtonBack.SetActive(false);

    }
    private void ChangeUI()
    {
        ButtonMap.SetActive(false);
        ButtonNote.SetActive(false);
        ButtonSearch.SetActive(false);
        ButtonTalk.SetActive(false);
        ButtonBack.SetActive(true);
    }
}