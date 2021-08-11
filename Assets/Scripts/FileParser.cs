using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class FileParser : MonoBehaviour {
    private static string path = "Assets/Resources/Dialogs/";

    public List<Dialog> ParseDialog(string filename) {
        List<Dialog> dialogs = new List<Dialog>();
        Dialog dialog = new Dialog();
        Option option;
        string[] lines = File.ReadAllLines(path + filename);
        foreach (string line in lines) {
            switch (line.Substring(0, 1)) {
                case "-":
                    dialogs.Add(dialog);
                    dialog = new Dialog();
                    dialog.id = line.Substring(2, line.Length-2);
                    Debug.Log("NEW DIALOG: " + dialog.id);
                    break;
                case "*":
                    option = new Option();
                    option.text = line.Substring(line.IndexOf("\""), line.LastIndexOf("\""));
                    option.destination = line.Substring(line.IndexOf(">")+2, line.Length-line.IndexOf(">")-2);
                    dialog.options.Add(option);
                    Debug.Log("OPTION ADDED: " + option.text + " | DESTINATION: " + option.destination);
                    break;
                default:
                    dialog.texts.Add(line);
                    Debug.Log("RESPONSE ADDED: " + dialog.texts[dialog.texts.Count()-1]);
                    break;
            }
        }
        dialogs.RemoveAt(0);

        return dialogs;

    }
}
