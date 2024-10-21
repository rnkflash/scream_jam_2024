using System.Collections.Generic;
using _Project.Scripts.utils;
using Doublsb.Dialog;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.dialog
{
    public class DialogSystem : Singleton<DialogSystem>
    {
        [SerializeField] private DialogManager dialogManager;

        [Button]
        public void Say(string whatToSay)
        {
            var dialogTexts = new List<DialogData>();

            dialogTexts.Add(new DialogData(whatToSay, "Li"));

            dialogManager.Show(dialogTexts);
        }
    }
}
