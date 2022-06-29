using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuItem_Controller : MonoBehaviour
{
    [System.Serializable]
    public class MenuItem
    {
        public ItemSelection_Controller ItemSelectionController;
        public MenuSubcatigories MenuSubcatigories;
    }

    public List<MenuItem> MenuItems;

    //public List<ItemSelection_Controller>  ItemSelectionControllers;
    
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (var menuItem in MenuItems)
        {
            menuItem.ItemSelectionController.Init(menuItem.MenuSubcatigories.MenuTitle, menuItem.MenuSubcatigories.menuItems);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
