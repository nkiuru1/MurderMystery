using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    ButtonHandler Buttons;
    Player MyPlayer;
    Room TestRoom;
    Clue TestClue;
    public RawImage BackgroundPicture;

    // Use this for initialization
    void Start () {
        BackgroundPicture.texture = (Texture)Resources.Load("Lobby", typeof(Texture));
        this.TestRoom = new Room(BackgroundPicture,"test room");
        Notebook testNotebook = new Notebook();
        this.MyPlayer = new Player("TestDude", this.TestRoom, testNotebook);
        this.Buttons = new ButtonHandler(this.MyPlayer.GetNotebook());
        this.InitializeClue("BrokenGlass", "Broken Glass", "Wonder how this was broken?");
    }
	
    private void InitializeClue(string resourceName,string name,string description)
    {
        RawImage ClueBackground = GameObject.Find("Clue").GetComponent<RawImage>();
        ClueBackground.texture = (Texture)Resources.Load(resourceName, typeof(Texture));
        this.TestClue = new Clue(name, description, ClueBackground);
        this.MyPlayer.GetNotebook().AddClue(this.TestClue);
    }

	// Update is called once per frame
	void Update ()
    {
        this.Buttons.Clicked();
    }
}
