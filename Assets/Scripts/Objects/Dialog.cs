using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog
{
    public string id;
    public List<string> texts;
    public List<Option> options;

    public Dialog() {
        options = new List<Option>();
        texts = new List<string>();
    }
}
