using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MenuData 
{
   
}

[Serializable]
public class MenuCategories
{
    public List<MenuSubcatigories> menuItems;
}

[Serializable]
public class MenuSubcatigories
{
    public string MenuTitle;
    public List<MenuItemProp> menuItems;
}

[Serializable]
public class MenuItemProp
{
    public string ItemName;
    public Sprite ItemImage = null;
    public string VideoURL;
    public string SceneName;
    public string RecognitionItemName;
}
