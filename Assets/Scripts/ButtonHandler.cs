using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHandler : MonoBehaviour
{
    public GameObject BtnSearch, BtnNote, BtnMap, BtnTalk, BtnBack, BtnInv, Canvas;
    public Text DescriptionText;
    public Canvas mapCanvas;
    public Canvas InvCanvas;
    private GameObject ButtonSearch, ButtonNote, ButtonMap, ButtonTalk, ButtonBack;
    private Player MyPlayer;
    private Dictionary<Clue, GameObject> Clues = new Dictionary<Clue, GameObject>();

    /*
        Calls PointerController to see if Button is clicked
    */
    private bool ButtonIsClicked(GameObject btn)
    {
        return btn.GetComponent<PointerController>().GetPointerDown();
    }

    /*
        Create Default UI Buttons and set-up Canvas
    */
    public void CreateDefaultButtons(Player MyPlayer)
    {
        this.mapCanvas.enabled = false;
        this.InvCanvas.enabled = false;

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
    }
    /*
      Method called from GameController, checks if buttons have been clicked.  
    */
    public void Clicked()
    {

        if (ButtonSearch != null && ButtonIsClicked(ButtonSearch))
        {
            this.SetActionUI();
            foreach (Button clueButton in Canvas.GetComponentsInChildren<Button>())
            {
                if (clueButton.name.Equals("BtnClue"))
                {
                    clueButton.enabled = true;
                }
            }
        }
        //Opens Inv Canvas and generated Buttons for items in inv
        if (ButtonIsClicked(ButtonNote))
        {
            this.SetActionUI();
            this.InvCanvas.enabled = true;
            int y = 180;
            Debug.Log(MyPlayer.GetNotebook().GetClues()[0].GetName());

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
        if (ButtonMap != null && ButtonIsClicked(ButtonMap))
        {
            this.SetActionUI();
            this.mapCanvas.enabled = true;
        }
        if (ButtonTalk != null && ButtonIsClicked(ButtonTalk))
        {
            this.SetActionUI();
        }
        //Disables other canvases, destroys all item buttons and sets Default UI
        if (ButtonBack != null && ButtonIsClicked(ButtonBack))
        {
            foreach (Clue item in this.Clues.Keys)
            {
                Destroy(this.Clues[item]);
            }
            foreach (Button clueButton in Canvas.GetComponentsInChildren<Button>())
            {
                if (clueButton.name.Equals("BtnClue"))
                {
                    clueButton.enabled = true;
                }
            }
            this.SetDefaultUI();
            this.InvCanvas.enabled = false;
        }
        //Checks if  an inventory button has been clicked
        foreach (Clue item in this.Clues.Keys)
        {
            if (ButtonIsClicked(this.Clues[item]))
            {
                this.DescriptionText.text = item.GetDescription();
            }
        }
    }
    /*
        Resets UI back to default state  
    */
    public void SetDefaultUI()
    {
        ButtonMap.SetActive(true);
        ButtonNote.SetActive(true);
        ButtonSearch.SetActive(true);
        ButtonTalk.SetActive(true);

        ButtonBack.SetActive(false);

        this.mapCanvas.enabled = false;
        this.Clues.Clear();
        this.DescriptionText.text = "";
    }
    /*
        Disables all buttons except the back button  
    */
    private void SetActionUI()
    {
        ButtonMap.SetActive(false);
        ButtonNote.SetActive(false);
        ButtonSearch.SetActive(false);
        ButtonTalk.SetActive(false);
        ButtonBack.SetActive(true);
    }
}