using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

public class FileParser {
    //constant variables
    private static string path = "Assets/Resources/Dialogs/";
    private static XmlDocument xmlDoc = new XmlDocument();
    private string filename;

    public void InitializeParser(string filename) {
        this.filename = filename;
    }

    public void GetDialog(string id) {
        //Prepare File
        if (System.IO.File.Exists(path + filename)) {
            xmlDoc.LoadXml(System.IO.File.ReadAllText(path + filename));
        }

        //Variables
        XmlNode node = xmlDoc.SelectSingleNode("/dialogs/dialog[@id='" + id + "']");
        Dialog d = new Dialog();
        int counter = 0;

        //1. apply dialog's ID
        d.ID = id;
        Debug.Log("Dialog ID: " + d.ID);

        //2. fetch the various lines
        d.lines = new string[node.SelectNodes("Lines/Line").Count, 2];
        foreach (XmlElement line in node.SelectNodes("Lines/Line")) {
            d.lines[counter, 0] = line.GetAttribute("actor");
            d.lines[counter, 1] = line.InnerText;
            Debug.Log("LINE - spoken by " + d.lines[counter, 0]);
            Debug.Log("CONTENT: \"" + d.lines[counter, 1] + "\"");
            counter++;
        }
        counter = 0;
        d.options = new string[node.SelectNodes("Options/Option").Count, 2];
        foreach (XmlElement option in node.SelectNodes("Options/Option")) {
            d.options[counter, 0] = option.GetAttribute("destination");
            d.options[counter, 1] = option.InnerText;
            Debug.Log("OPTION - destined towards " + d.options[counter, 0]);
            Debug.Log("CONTENT: \"" + d.options[counter, 1] + "\"");
            counter++;
        }

    }
}
