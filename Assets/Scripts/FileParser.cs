using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

public class FileParser : MonoBehaviour {
    private static string path = "Assets/Resources/Dialogs/";

    public List<Dialog> ParseDialog(string filename) {
        XmlDocument xmlDoc = new XmlDocument();
        if (System.IO.File.Exists(path + filename)) {
            xmlDoc.LoadXml(System.IO.File.ReadAllText(path + filename));
        }

        List<Dialog> dialogs = new List<Dialog>();

        foreach (XmlElement node in xmlDoc.SelectNodes("dialogs/dialog")) {
            string id = node.GetAttribute("id");
            string[,] lines = new string[node.SelectNodes("Lines/Line").Count,2];
            foreach(XmlElement line in node.SelectNodes("Lines/Line")) {
                Debug.Log(line.GetAttribute("actor"));
            }
        }

        return dialogs;

    }
}
