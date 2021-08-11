using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option
{
    private string text;
    public List<string> addconditions = new List<string>();
    private string destination;
    private string abilityname;
    private int abilityvalue;

    public void SetText(string text) {
        this.text = text;
    }

    public void SetDestination(string dest) {
        this.destination = dest;
    }

    public string GetText() {
        return text;
    }

    public string GetDestination() {
        return destination;
    }

    public void SetAbilityName(string ability) {
        this.abilityname = ability;
    }

    public void SetAbilityValue(int value) {
        this.abilityvalue = value;
    }

    public string GetAbilityName() {
        return abilityname;
    }

    public int GetAbilityValue() {
        return abilityvalue;
    }

    public string GetFullAbilityName() {
        string fullname = "";
        switch (abilityname) {
            case "STR":
                fullname = "Strength";
                break;
            case "DEX":
                fullname = "Dexterity";
                break;
            case "CON":
                fullname = "Constitution";
                break;
            case "INT":
                fullname = "Intelligence";
                break;
            case "WIS":
                fullname = "Wisdom";
                break;
            case "CHA":
                fullname = "Charisma";
                break;
        }
        return fullname;
    }
}
