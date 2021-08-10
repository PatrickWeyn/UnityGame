using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog
{
    private Sprite character;
    private string firstname;
    private string lastname;
    private string response;
    private string option;

    public Dialog(Sprite character, string firstname, string lastname, string response, string option) {
        Character = character;
        Firstname = firstname;
        Lastname = lastname;
        Response = response;
        Option = option;
    }

    public Sprite Character { get => character; set => character = value; }
    public string Firstname { get => firstname; set => firstname = value; }
    public string Lastname { get => lastname; set => lastname = value; }
    public string Response { get => response; set => response = value; }
    public string Option { get => option; set => option = value; }
}
