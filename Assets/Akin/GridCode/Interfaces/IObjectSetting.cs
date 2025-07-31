using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;

public interface IObjectSetting
{
    IInventoryObject InventoryObjectData { get; set; }

    void IntegrationCaller();
    void RegisterCaller();

    void ObjectStarterCaller();

    void ObjectOutOfGridCaller();
}
