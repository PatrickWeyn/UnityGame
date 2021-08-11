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
                    dialog.id = line.Substring(2, line.Length - 2);
                    Debug.Log("NEW DIALOG: " + dialog.id);
                    break;
                case "*":
                    option = new Option();
                    option.text = line.Substring((line.IndexOf("\"")+1), (line.LastIndexOf("\"") - line.IndexOf("\"")-1));
                    option.destination = line.Substring(line.IndexOf(">") + 2, (line.Length - line.IndexOf(">") - 2));
                    dialog.options.Add(option);
                    Debug.Log("OPTION ADDED: " + option.text + " | DESTINATION: " + option.destination);
                    break;
                default:
                    if (line.Contains(">")) {
                        string identifier = line.Substring(line.IndexOf(">") + 2, (line.Length - line.IndexOf(">") - 2));
                        if (identifier == "DONE") {
                            dialog.texts.Add(line.Substring(0, line.IndexOf(">")));
                        }
                        else {
                            dialog.texts.Add(line.Substring(0, line.IndexOf(">")));
                            foreach (Option o in GetDialog(dialogs, identifier).options) {
                                dialog.options.Add(o);
                            }
                        }
                    }
                    else {
                        dialog.texts.Add(line);
                    }
                    Debug.Log("RESPONSE ADDED: " + dialog.texts[dialog.texts.Count() - 1]);
                    break;
            }
        }
        dialogs.RemoveAt(0);

        return dialogs;

    }

    private Dialog GetDialog(List<Dialog> dialogs, string id) {
        foreach (Dialog d in dialogs) {
            if (d.id == id) return d;
        }
        return null;
    }
}
