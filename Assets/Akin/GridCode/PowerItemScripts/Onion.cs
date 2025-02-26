using UnityEngine;

public class Onion : MonoBehaviour,IPowerItem ,IInventoryObject
{
    public bool OnUp => throw new System.NotImplementedException();

    public bool OnDown => throw new System.NotImplementedException();

    public bool onRight => throw new System.NotImplementedException();

    public bool onLeft => throw new System.NotImplementedException();

    public bool OnDownNext => throw new System.NotImplementedException();

    public bool OnUpNext => throw new System.NotImplementedException();

    public bool onLeftNext => throw new System.NotImplementedException();

    public bool onRightNext => throw new System.NotImplementedException();

    public bool OnDownObjectDedect => throw new System.NotImplementedException();

    public bool OnUpObjectDedect => throw new System.NotImplementedException();

    public bool onLeftObjectDedect => throw new System.NotImplementedException();

    public bool onRightObjectDedect => throw new System.NotImplementedException();

    public bool gridEnter { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PowerUpBuffs()
    {

    }

    public void GridIntegration()
    {
        throw new System.NotImplementedException();
    }

    public void RegisterYourself()
    {
        throw new System.NotImplementedException();
    }

    public void Consume()
    {
        throw new System.NotImplementedException();
    }

    public void MoveObjectStarting()
    {
        throw new System.NotImplementedException();
    }
}
