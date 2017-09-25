using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler
{
    private Dictionary<string, PointerController> Buttons;
    private Notebook PlayerNotebook;

    public void AddButton(string Name)
    {
        Debug.Log("Creating button " + Name);
        Buttons.Add(Name, GameObject.Find(Name).GetComponent<PointerController>());

    }

    public void SetButtonsState(bool State, List<string> ButtonList)
    {
        foreach (string b in ButtonList)
        {
            Debug.Log("Setting button " + b + " state to " + State);
            Buttons[b].gameObject.SetActive(State);
        }
    }

    private bool ButtonIsClicked(string Name)
    {
        return Buttons[Name].GetPointerDown();
    }

    private void CreateDefaultButtons()
    {
        foreach (string b in new List<string>() {
            "BtnNote", "BtnMap", "BtnTalk", "BtnSearch", "BtnBack", "BtnBackNote","BtnItem1"})
        {
            AddButton(b);
        }
        SetButtonsState(false, new List<string>() { "BtnBack", "BtnBackNote", "BtnItem1" });
    }

    public void Clicked()
    {
        if (ButtonIsClicked("BtnBack"))
        {
            SetButtonsState(false, new List<string>() { "BtnBack" });
            SetButtonsState(true, new List<string>() { "BtnSearch", "BtnMap", "BtnTalk", "BtnNote" });
        }

        if (ButtonIsClicked("BtnNote"))
        {
            SetButtonsState(false, new List<string>() { "BtnSearch", "BtnMap", "BtnTalk", "BtnNote" });
            SetButtonsState(true, new List<string>() { "BtnBackNote" });
            this.RevealButtons();
        }

        if (ButtonIsClicked("BtnBackNote"))
        {
            SetButtonsState(false, new List<string>() { "BtnBackNote" });
            SetButtonsState(true, new List<string>() { "BtnSearch", "BtnNote", "BtnMap", "BtnTalk" });
        }

        if (ButtonIsClicked("BtnMap"))
        {
            SetButtonsState(false, new List<string>() { "BtnMap", "BtnSearch", "BtnTalk", "BtnNote" });
            SetButtonsState(true, new List<string>() { "BtnBack" });
        }

        if (ButtonIsClicked("BtnTalk"))
        {
            SetButtonsState(false, new List<string>() { "BtnTalk", "BtnMap", "BtnNote", "BtnSearch" });
            SetButtonsState(true, new List<string>() { "BtnBack" });
        }

        if (ButtonIsClicked("BtnSearch"))
        {
            SetButtonsState(false, new List<string>() { "BtnSearch", "BtnMap", "BtnTalk", "BtnNote" });
            SetButtonsState(true, new List<string>() { "BtnBack" });
        }
    }

    public ButtonHandler(Notebook notebook)
    {
        Buttons = new Dictionary<string, PointerController>();
        CreateDefaultButtons();
        this.PlayerNotebook = notebook;
    }

    public void RevealButtons()
    {
        string ButtonName;
        for (int i = 0; i < this.PlayerNotebook.GetClues().Count; i++)
        {
            ButtonName = "BtnInv" + i;
            Debug.Log(ButtonName);
            this.SetButtonsState(true, new List<string>() { ButtonName });

        }
    }
}

/*
public class ButtonHandler
{
    private PointerController ButtonNotebook;
    private PointerController ButtonMap;
    private PointerController ButtonTalk;
    private PointerController ButtonSearch;
    private PointerController ButtonBack;
    private PointerController ButtonBackNote;
    public ButtonHandler()
    {
        ButtonNotebook = GameObject.Find("BtnNote").GetComponent<PointerController>();
        ButtonMap = GameObject.Find("BtnMap").GetComponent<PointerController>();
        ButtonTalk = GameObject.Find("BtnTalk").GetComponent<PointerController>();
        ButtonSearch = GameObject.Find("BtnSearch").GetComponent<PointerController>();
        ButtonBack = GameObject.Find("BtnBack").GetComponent<PointerController>();
        ButtonBackNote = GameObject.Find("BtnBackNote").GetComponent<PointerController>();
        ButtonBack.gameObject.SetActive(false);
        ButtonBackNote.gameObject.SetActive(false);

    }
    public void Clicked()
    {
        if (ButtonNotebook.GetPointerDown())
        {
            ButtonBackNote.gameObject.SetActive(true);
            ButtonSearch.gameObject.SetActive(false);
            ButtonMap.gameObject.SetActive(false);
            ButtonNotebook.gameObject.SetActive(false);
            ButtonTalk.gameObject.SetActive(false);
        }
        if (ButtonBackNote.GetPointerDown())
        {
            ButtonBackNote.gameObject.SetActive(false);
            ButtonSearch.gameObject.SetActive(true);
            ButtonMap.gameObject.SetActive(true);
            ButtonNotebook.gameObject.SetActive(true);
            ButtonTalk.gameObject.SetActive(true);
        }
        if (ButtonMap.GetPointerDown())
        {
            Debug.Log("Map Works");
        }
        if (ButtonTalk.GetPointerDown())
        {
            Debug.Log("Talk Works");
        }
        if (ButtonSearch.GetPointerDown())
        {
            ButtonBack.gameObject.SetActive(true);
            ButtonSearch.gameObject.SetActive(false);
            ButtonMap.gameObject.SetActive(false);
            ButtonNotebook.gameObject.SetActive(false);
            ButtonTalk.gameObject.SetActive(false);
        }
        if (ButtonBack.GetPointerDown())
        {
            ButtonBack.gameObject.SetActive(false);
            ButtonSearch.gameObject.SetActive(true);
            ButtonMap.gameObject.SetActive(true);
            ButtonTalk.gameObject.SetActive(true);
            ButtonNotebook.gameObject.SetActive(true);
        }
    }
}
*/
