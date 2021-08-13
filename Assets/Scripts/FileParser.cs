using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class FileParser : MonoBehaviour {
    private static string path = "Assets/Resources/Dialogs/";

    public List<Dialog> ParseDialog(string filename) {
        //Variables
        List<Dialog> dialogs = new List<Dialog>();
        Dialog dialog = new Dialog();
        Option option;

        //1. Load all lines of the text file.
        string[] lines = File.ReadAllLines(path + filename);

        //2 Loop through each line to determine what it is.
        foreach (string line in lines) {
            switch (line.Substring(0, 1)) {
                //NEW DIALOG
                case "-":
                    dialogs.Add(dialog);
                    dialog = new Dialog();
                    dialog.id = line.Substring(2, line.Length - 2);
                    Debug.Log("NEW DIALOG: " + dialog.id);
                    break;
                //NEW OPTION
                case "*":
                    option = new Option();
                    option.SetText(line.Substring((line.IndexOf("\"") + 1), (line.LastIndexOf("\"") - line.IndexOf("\"") - 1)));
                    option.SetDestination(line.Substring(line.LastIndexOf(">") + 2, (line.Length - line.LastIndexOf(">") - 2)));
                    if (line.Contains("[")) {
                        option.SetAbilityName(line.Substring(line.IndexOf("[") + 1, line.IndexOf(":") - (line.IndexOf("[") + 1)));
                        option.SetAbilityValue(int.Parse(line.Substring(line.IndexOf(":") + 1, line.IndexOf("]") - (line.IndexOf(":") + 1))));
                        }
                    if (line.Contains("(")) {
                        option.SetOpinionName(line.Substring(line.IndexOf("(") + 1, line.LastIndexOf(":") - (line.IndexOf("(") + 1)));
                        option.SetOpinionValue(int.Parse(line.Substring(line.LastIndexOf(":") + 1, line.IndexOf(")") - (line.LastIndexOf(":") + 1))));
                        }
                    dialog.options.Add(option);
                    Debug.Log("NEW OPTION: " + option.GetText());
                    break;
                //NEW LINE
                default:
                    if (line.Contains(">")) {
                        string identifier = line.Substring(line.IndexOf(">") + 2, (line.Length - line.IndexOf(">") - 2));
                        if (identifier == "DONE") {
                            dialog.texts.Add(line.Substring(0, line.IndexOf(">")));
                            } else {
                            dialog.texts.Add(line.Substring(0, line.IndexOf(">")));
                            foreach (Option o in GetDialog(dialogs, identifier).options) {
                                dialog.options.Add(o);
                                }
                            }
                        } else {
                        dialog.texts.Add(line);
                        }
                    Debug.Log("RESPONSE ADDED: " + dialog.texts[dialog.texts.Count() - 1]);
                    break;
                }
            }
        dialogs.Add(dialog);
        dialogs.RemoveAt(0);

        return dialogs;

        }

    private Dialog GetDialog(List<Dialog> dialogs, string id) {
        foreach (Dialog d in dialogs) {
            if (d.id == id) return d;
            }
        return null;
        }

    private void AddDialog(List<Dialog> dialogs, Dialog dialog) {
        dialogs.Add(dialog);
        }
    }
