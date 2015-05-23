using UnityEngine;
using System.Collections;

public class InstructionsSeek : MonoBehaviour {

    private Rect instruction;
    public GUIStyle style;


    // Use this for initialization
    void Start()
    {
        instruction = new Rect(Screen.width * .251f, Screen.height * .90269f, Screen.width * .208385f, Screen.height * .09944f);
        style = new GUIStyle();
        style.normal.textColor = Color.white;
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnGUI()
    {

        GUI.Label(instruction, "Move green box with WASD", style);
    }
}
