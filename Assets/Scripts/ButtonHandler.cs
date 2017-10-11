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
    public Text DescriptionText, DialogText, CharacterNameText, CharactersInRoomText;
    public Canvas mapCanvas, InvCanvas, TalkCanvas;
    public Turn Turn;
    public Transform ContentPanel;
    private Character CharacterTalk;
    private GameObject ButtonSearch, ButtonNote, ButtonMap, ButtonTalk, ButtonBack;
    private Player MyPlayer;
    private Room CurrentRoom;
    private Dictionary<Clue, GameObject> Clues = new Dictionary<Clue, GameObject>();
    private List<GameObject> Choices = new List<GameObject>();
    private List<GameObject> ClueButtons = new List<GameObject>();
    private Dictionary<Canvas, List<Button>> Silouettes = new Dictionary<Canvas, List<Button>>();
    private List<Character> CharactersInRoom;
    private bool Talking = false;

    /// <summary>
	/// Calls PointerController and returns true if button is clicked
	/// </summary>
    private bool ButtonIsClicked(GameObject btn)
    {
        return btn.GetComponent<PointerController>().GetPointerDown();
    }

    /// <summary>
    /// Creates the default buttons.
    /// Disables all clues & Adds them to a dictionary
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
            if (clueButton.name.Contains("BtnC"))
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
        //Activates the clues in all rooms once the search button is clicked.
        if (ButtonSearch != null && ButtonIsClicked(ButtonSearch))
        {
            this.SetActionUI();
            foreach (GameObject clueButton in this.ClueButtons)
            {
                clueButton.SetActive(true);
            }
        }
        //Opens Inv Canvas and generated Buttons for items in inv.
        if (ButtonIsClicked(ButtonNote))
        {
            this.SetActionUI();
            this.InvCanvas.enabled = true;
            int y;
            if (Application.platform == RuntimePlatform.Android)
            {
                y = 160;
            }
            else
            {
                y = 530;
            }


            foreach (Clue item in this.MyPlayer.GetNotebook().GetClues())
            {
                GameObject obj = Instantiate(BtnInv);
                Vector2 pos = obj.transform.position;
                pos.y = y;
                obj.transform.position = pos;
                obj.GetComponentInChildren<Text>().text = item.GetName();
                obj.transform.SetParent(ContentPanel, false);
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
            this.TalkAction();
        }
        //Disables other canvases, destroys all item buttons and sets Default UI
        if (ButtonBack != null && ButtonIsClicked(ButtonBack))
        {
            this.SetDefaultUI();
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
            this.ProgressInDialogue();
        }
    }
    /// <summary>
    /// Generates the required NPC choices && starts the conversation
    /// </summary>
    private void TalkAction()
    {
        this.SetActionUI();
        this.Talking = true;
        this.TalkCanvas.enabled = true;
        this.CharactersInRoom = this.CurrentRoom.GetCharacters();
        int y = 160;
        if (CharactersInRoom.Count > 1)
        {
            this.CharactersInRoomText.text = "Characters you can talk to:";
            foreach (Character character in CharactersInRoom)
            {
                this.GenerateChoice(y, character.GetName());
                y -= 60;
            }
        }
        else if (this.CharactersInRoom != null && this.CharactersInRoom.Count == 1)
        {
            this.GenerateChoice(y, CharactersInRoom[0].GetName());
        }
        this.CharacterNameText.text = "";
    }
    /// <summary>
    /// Goes through the choices.
    /// If there is a character to talk to & the choice is available it asks for the next node 
    /// from the character dialogue tree.
    /// If there is no character to talk to, the choices are the characters and it sets the character
    /// that you want to talk to.
    /// If the choice is not the end node, it generates the next choices.
    /// </summary>
    private void ProgressInDialogue()
    {
        bool clicked = false;
        foreach (GameObject item in this.Choices)
        {
            if (item != null && this.CharacterTalk != null && ButtonIsClicked(item))
            {
                clicked = true;
                try
                {
                    if (!this.CharacterTalk.NextChoice(item.GetComponentInChildren<Text>().text))
                    {
                        this.CharacterTalk.Next();
                    }
                    if (item.GetComponentInChildren<Text>().text.Equals("Goodbye"))
                    {
                        this.SetDefaultUI();
                        clicked = false;
                    }
                }
                catch (Exception)
                {

                    this.SetDefaultUI();
                    clicked = false;
                }

            }
            else if (this.CharacterTalk == null && item != null)
            {
                if (ButtonIsClicked(item))
                {
                    foreach (Character character in this.CharactersInRoom)
                    {
                        if (character.GetName().Equals(item.GetComponentInChildren<Text>().text))
                        {
                            this.CharacterTalk = character;
                            clicked = true;
                        }
                    }
                }
            }
        }
        if (clicked) this.UpdateChoices();
    }
    /// <summary>
    /// Updates choices and the dialogue text.
    /// Destroys past answer choices.
    /// If there is only 1 choice, it only displays button with "next".
    /// If end of conversation, displays button with "back".
    /// </summary>
    private void UpdateChoices()
    {
        foreach (GameObject choice in this.Choices)
        {
            Destroy(choice);
        }
        string[] choices = this.CharacterTalk.GetChoices();
        int y = 160;

        if (choices.Length != 0)
        {
            foreach (string choice in choices)
            {
                this.GenerateChoice(y, choice);
                y -= 60;
            }
        }
        else if (!this.CharacterTalk.IsEndOfConversation())
        {
            this.GenerateChoice(y, "Next");
        }
        else
        {
            this.GenerateChoice(y, "Goodbye");
        }
        this.CharactersInRoomText.text = "";
        this.DialogText.text = this.CharacterTalk.GetDialogue();
    }
    /// <summary>
    /// Resets UI back to default state, by disabling all other canvases, except for the parent canvas.
    /// Clears the list of clues, & the description text box.
    /// </summary>
    public void SetDefaultUI()
    {
        ButtonMap.SetActive(true);
        ButtonNote.SetActive(true);
        ButtonSearch.SetActive(true);
        ButtonTalk.SetActive(true);

        ButtonBack.SetActive(false);

        foreach (GameObject choice in this.Choices)
        {
            Destroy(choice);
        }

        foreach (Clue item in this.Clues.Keys)
        {
            Destroy(this.Clues[item]);
        }

        foreach (GameObject clueButton in this.ClueButtons)
        {
            clueButton.SetActive(false);
        }

        this.mapCanvas.enabled = false;
        this.TalkCanvas.enabled = false;
        this.InvCanvas.enabled = false;
        this.Clues.Clear();
        this.DescriptionText.text = "";
        if (this.CharacterTalk != null)
        {
            this.CharacterTalk.ResetConversation();
            this.CharacterTalk.Talked();
            if (MyPlayer.GetNotebook().GetClue(this.CharacterTalk.GetName()) == null)
            {
                if (this.CharacterTalk.GetClue() != null) MyPlayer.GetNotebook().AddClue(this.CharacterTalk.GetClue());
                if (this.CharacterTalk.GetName().Equals("Reporter") && MyPlayer.MadeAgreementWithReporter())
                {
                    MyPlayer.GetNotebook().DeleteClue("Diary");
                }
                if (this.CharacterTalk.GetName().Equals("Reporter")) MyPlayer.MakeAgreement();

                if (this.CharacterTalk.GetTreeName().Equals("BusinessmanReporterAgreement"))
                {
                    MyPlayer.GetNotebook().AddClue(new Clue("Letter (fake)", "It seems to be a letter written by you, filled with threats to the victim. " +
                        "You don't remember writing it. It must be a fake, right?"));
                }
            }
        }
        this.CharacterTalk = null;
        this.DialogText.text = "";

        this.Talking = false;
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

        this.Talking = false; // enabled later in Clicked() when in talk mode
    }
    /// <summary>
    /// Takes the current room & canvas from GC.
    /// Updates the number of characters in the room based on that.
    /// </summary>
    /// <param name="currentRoom"></param>
    /// <param name="currentCanvas"></param>
    public void UpdateRoom(Room currentRoom, Canvas currentCanvas)
    {
        if (this.CurrentRoom != currentRoom)
        {
            this.CurrentRoom = currentRoom;

            if (!this.Silouettes.ContainsKey(currentCanvas))
            {
                List<Button> temp = new List<Button>();
                foreach (Button btn in currentCanvas.GetComponentsInChildren<Button>())
                {
                    if (btn.name.Contains("BtnP"))
                    {
                        btn.gameObject.SetActive(false);
                        temp.Add(btn);
                    }
                }
                this.Silouettes.Add(currentCanvas, temp);
            }
            for (int i = 0; i < 3; i++)
            {
                this.Silouettes[currentCanvas][i].gameObject.SetActive(false);
            }

            for (int i = 0; i < this.CurrentRoom.GetCharacters().Count; i++)
            {
                this.Silouettes[currentCanvas][i].gameObject.SetActive(true);
            }
        }
    }

    private void GenerateChoice(int y, string buttonText)
    {
        GameObject obj = Instantiate(BtnChoice);
        Vector2 pos = obj.transform.position;
        pos.y = y;
        obj.transform.position = pos;
        obj.GetComponentInChildren<Text>().text = buttonText;
        obj.transform.SetParent(Canvas.transform, false);
        this.Choices.Add(obj);
    }

    /// <summary>
    /// GameController can query if we are in talk mode.
    /// </summary>
    /// <returns>True or false.</returns>
    public bool GetTalkActive()
    {
        return Talking;
    }
    /// <summary>
    /// Opens Talk Menu At the beginning of the game
    /// </summary>
    public void GameStart()
    {
        this.TalkAction();
    }

    public void DiningRoomAction()
    {
        this.TalkAction();
    }
}