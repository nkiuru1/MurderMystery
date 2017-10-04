using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHandler : MonoBehaviour
{
    public GameObject BtnSearch, BtnNote, BtnMap, BtnTalk, BtnBack, BtnInv, BtnChoice, Canvas;
    public Text DescriptionText, DialogText;
    public Canvas mapCanvas, InvCanvas, TalkCanvas;
    public Turn Turn;
    private Character CharacterTalk;
    private GameObject ButtonSearch, ButtonNote, ButtonMap, ButtonTalk, ButtonBack;
    private Player MyPlayer;
    private Room CurrentRoom;
    private Dictionary<Clue, GameObject> Clues = new Dictionary<Clue, GameObject>();
    private List<GameObject> Choices = new List<GameObject>();
    private List<GameObject> ClueButtons = new List<GameObject>();

    /// <summary>
	/// Calls PointerController and returns true if button is clicked
	/// </summary>
    private bool ButtonIsClicked(GameObject btn)
    {
        return btn.GetComponent<PointerController>().GetPointerDown();
    }

    /// <summary>
    /// Creates the default buttons.
    /// </summary>
    /// <param name="MyPlayer">My player.</param>
    public void CreateDefaultButtons(Player MyPlayer)
    {
        this.mapCanvas.enabled = false;
        this.InvCanvas.enabled = false;
        this.TalkCanvas.enabled = false;

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

        this.MyPlayer = MyPlayer;

        foreach (Button clueButton in Canvas.GetComponentsInChildren<Button>())
        {
            if (clueButton.name.Equals("BtnClue"))
            {
                this.ClueButtons.Add(clueButton.transform.gameObject);
                clueButton.transform.gameObject.SetActive(false);
            }
        }
    }
    /// <summary>
    /// Method called from GameController, checks if buttons have been clicked. 
    /// </summary>
    public void Clicked()
    {
        //Activates the clues in all rooms
        if (ButtonSearch != null && ButtonIsClicked(ButtonSearch))
        {
            this.SetActionUI();
            foreach (GameObject clueButton in this.ClueButtons)
            {
                clueButton.SetActive(true);
            }
        }
        //Opens Inv Canvas and generated Buttons for items in inv
        if (ButtonIsClicked(ButtonNote))
        {
            this.SetActionUI();
            this.InvCanvas.enabled = true;
            int y = 180;

            foreach (Clue item in this.MyPlayer.GetNotebook().GetClues())
            {
                GameObject obj = Instantiate(BtnInv);
                Vector2 pos = obj.transform.position;
                pos.y = y;
                obj.transform.position = pos;
                obj.GetComponentInChildren<Text>().text = item.GetName();
                obj.transform.SetParent(Canvas.transform, false);
                Clues.Add(item, obj);
                y -= 40;
            }
        }
        //Opens Map canvas
        if (ButtonMap != null && ButtonIsClicked(ButtonMap))
        {
            this.SetActionUI();
            this.mapCanvas.enabled = true;
        }
        //Generates the initial choices and text
        if (ButtonTalk != null && ButtonIsClicked(ButtonTalk))
        {
            this.SetActionUI();
            this.TalkCanvas.enabled = true;
            this.CharacterTalk = this.CurrentRoom.GetCharacters()[0];
            this.UpdateChoices();
        }
        //Disables other canvases, destroys all item buttons and sets Default UI
        if (ButtonBack != null && ButtonIsClicked(ButtonBack))
        {
            foreach (Clue item in this.Clues.Keys)
            {
                Destroy(this.Clues[item]);
            }

            foreach (GameObject clueButton in this.ClueButtons)
            {
                clueButton.SetActive(false);
            }
            this.SetDefaultUI();
            this.CharacterTalk.ResetConversation();
        }

        //Checks if  an inventory button has been clicked
        if (this.InvCanvas.enabled)
        {
            foreach (Clue item in this.Clues.Keys)
            {
                if (ButtonIsClicked(this.Clues[item]))
                {
                    this.DescriptionText.text = item.GetDescription();
                }
            }
        }

        if (this.TalkCanvas.enabled)
        {
            bool clicked = false;
            foreach (GameObject item in this.Choices)
            {
                if (ButtonIsClicked(item))
                {
                    clicked = true;
                    if (!this.CharacterTalk.NextChoice(item.GetComponentInChildren<Text>().text))
                    {
                        this.CharacterTalk.Next();
                    }
                }
            }
            if (clicked)
            {
                this.UpdateChoices();
            }
        }
    }
    /// <summary>
    /// Updates choices and the dialogue text
    /// </summary>
    private void UpdateChoices()
    {
        foreach (GameObject choice in this.Choices)
        {
            choice.SetActive(false);
        }
        string[] choices = this.CharacterTalk.GetChoices();
        int y = 180;

        if (choices.Length != 0)
        {
            foreach (string choice in choices)
            {
                GameObject obj = Instantiate(BtnChoice);
                Vector2 pos = obj.transform.position;
                pos.y = y;
                obj.transform.position = pos;
                obj.GetComponentInChildren<Text>().text = choice;
                obj.transform.SetParent(Canvas.transform, false);
                this.Choices.Add(obj);
                y -= 40;
            }
        }
        else if (!this.CharacterTalk.IsEndOfConversation())
        {
            GameObject obj = Instantiate(BtnChoice);
            Vector2 pos = obj.transform.position;
            pos.y = y;
            obj.transform.position = pos;
            obj.GetComponentInChildren<Text>().text = "Next";
            obj.transform.SetParent(Canvas.transform, false);
            this.Choices.Add(obj);
        }
        this.DialogText.text = this.CharacterTalk.GetDialogue();
    }
    /// <summary>
    /// Resets UI back to default state  
    /// </summary>
    public void SetDefaultUI()
    {
        ButtonMap.SetActive(true);
        ButtonNote.SetActive(true);
        ButtonSearch.SetActive(true);
        ButtonTalk.SetActive(true);

        ButtonBack.SetActive(false);

        this.mapCanvas.enabled = false;
        this.TalkCanvas.enabled = false;
        this.Clues.Clear();
        this.DescriptionText.text = "";
    }
    /// <summary>
    /// Disables all buttons except the back button. Called when any UI button is clicked.
    /// </summary>
    private void SetActionUI()
    {
        ButtonMap.SetActive(false);
        ButtonNote.SetActive(false);
        ButtonSearch.SetActive(false);
        ButtonTalk.SetActive(false);
        ButtonBack.SetActive(true);
    }

    public void UpdateRoom(Room currentRoom)
    {
        this.CurrentRoom = currentRoom;
    }
}