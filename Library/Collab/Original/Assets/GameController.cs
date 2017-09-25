using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    ButtonHandler buttons;
    Player MyPlayer;
    Room TestRoom;
    Clue TestClue;
    public Transform contentPanel;
    public GameObject prefab;

	// Use this for initialization
	void Start () {
        GameObject newButton = (GameObject)GameObject.Instantiate(prefab);
        newButton.transform.SetParent(contentPanel);

        Button sampleButton = newButton.GetComponent<Button>();
    }
	
	// Update is called once per frame
	void Update () {
        //buttons.Clicked();
	}
}
