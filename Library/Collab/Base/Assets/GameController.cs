using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    ButtonHandler buttons;
    Player MyPlayer;
    Room TestRoom;
    Clue TestClue;

	// Use this for initialization
	void Start () {
        buttons = new ButtonHandler();
        RawImage BackgroundPicture = GameObject.Find("Background").GetComponent<RawImage>();
        BackgroundPicture.texture = (Texture)Resources.Load("Lobby", typeof(Texture));
        RawImage ClueBackground = GameObject.Find("Clue").GetComponent<RawImage>();
        ClueBackground.texture = (Texture)Resources.Load("BrokenGlass", typeof(Texture));
        TestClue = new Clue("Broken Glass", "This is broken glass", ClueBackground);
        this.TestRoom = new Room(BackgroundPicture,"test room");
        this.MyPlayer = new Player("TestDude", this.TestRoom, new Notebook());
    }
	
	// Update is called once per frame
	void Update () {
        //buttons.Clicked();
	}
}
