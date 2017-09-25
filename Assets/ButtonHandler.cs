﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler
{
    private Dictionary<string, PointerController> Buttons;
    private Notebook PlayerNotebook;
    private List<PointerController> clues;

	static readonly List<string> UIDefaultButtons = new List<string>()
		{"BtnNote", "BtnMap", "BtnTalk", "BtnSearch", "BtnBack", "BtnBackNote","BtnItem0"};

	static readonly List<string> UIMainVisible = new List<string>()
		{"BtnNote", "BtnMap", "BtnTalk", "BtnSearch"};
	static readonly List<string> UIMainInvisible = new List<string>()
		{"BtnBack", "BtnBackNote"};

	static readonly List<string> UIActionVisible = new List<string>()
		{"BtnBack"};
	static readonly List<string> UIActionInvisible = new List<string>()
		{"BtnNote", "BtnMap", "BtnTalk", "BtnSearch", "BtnBackNote"};

	static readonly List<string> UINotebookVisible = new List<string>()
		{"BtnBackNote"};
	static readonly List<string> UINotebookInvisible = new List<string>()
		{"BtnNote", "BtnMap", "BtnTalk", "BtnSearch", "BtnBack"};

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
		foreach (string b in UIDefaultButtons)
		{
			AddButton(b);
		}

		SetButtonsState(false, UIMainInvisible);
		SetButtonsState(true, UIMainVisible);
    }

	public void Clicked()
	{
		if (ButtonIsClicked("BtnBack"))
		{
			SetButtonsState(false, UIMainInvisible);
			SetButtonsState(true, UIMainVisible);
		}

		if (ButtonIsClicked("BtnNote"))
		{
			SetButtonsState(false, UINotebookInvisible);
			SetButtonsState(true, UINotebookVisible);
		}

		if (ButtonIsClicked("BtnBackNote"))
		{
			SetButtonsState(false, UIMainInvisible);
			SetButtonsState(true, UIMainVisible);
		}

		if (ButtonIsClicked("BtnMap")
			|| ButtonIsClicked("BtnTalk")
			|| ButtonIsClicked("BtnSearch"))
		{
			SetButtonsState(false, UIActionInvisible);
			SetButtonsState(true, UIActionVisible);
		}

		string InventoryButtonName;

		/*for (int i = 0; i < this.PlayerNotebook.GetClues ().Count; i++) {
			InventoryButtonName = "BtnInventory" + i;
			if (ButtonIsClicked (InventoryButtonName)) {
				// do something here
			}
		}*/
    }

    public ButtonHandler(Notebook notebook)
    {
        Buttons = new Dictionary<string, PointerController>();
        CreateDefaultButtons();
        this.PlayerNotebook = notebook;
    }

    public void RevealInventoryButtons()
    {
        string ButtonName;
        Debug.Log(PlayerNotebook.GetClues().Count);
        for (int i = 0; i < this.PlayerNotebook.GetClues().Count; i++)
        {
            ButtonName = "BtnItem" + i;
            Debug.Log(ButtonName);
            this.SetButtonsState(true, new List<string>() { ButtonName });

        }
    }
}