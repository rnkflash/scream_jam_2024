using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Scripts;
using _Project.Scripts.interaction;
using UnityEngine;

public class TrailerConnectorControl : MonoBehaviour, IInteractable
{
    [SerializeField] private string itemName = "trailer connector";
    [SerializeField] private string connectedHint = "press E to deattach trailer";
    [SerializeField] private string disconnectedHint = "press E to attach trailer";
    private CarController carController;

    private void Awake()
    {
        carController = GetComponentInParent<CarController>();
    }

    public void Interact()
    {
        carController.AttachDetachTrailer();
    }

    public string GetName()
    {
        return itemName;
    }

    public string GetHint()
    {
        return carController.trailer == null ? disconnectedHint : connectedHint;
    }
}
