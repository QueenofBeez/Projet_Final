using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// namespace QueenofBeez.Items
public abstract class HotbarItem : ScriptableObject
{
    [Header("Basic Info")] // built in tag fo the UI to make it look nicer
    [SerializeField] private new string name = "New Hotbar Item Name"; // have to add "new" or will not work
    [SerializeField] private Sprite icon = null; // not necessary to add "null" since it already is null when initialised

    public string Name => name; // any external class can reference it and use it but can't change the name
    public abstract string ColouredName { get; }
    public Sprite Icon => icon;

    public abstract string GetInfoDisplayText(); // Different classes will display different information
}
