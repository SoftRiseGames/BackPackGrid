using UnityEngine;
using System.Threading.Tasks;
using System;
public class EventManagerCode : MonoBehaviour
{

    public static Action OnTourEnd;

    public void TourEndVoid() => OnTourEnd.Invoke();
}
