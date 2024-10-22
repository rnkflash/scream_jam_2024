using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.utils;
using Doublsb.Dialog;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.dialog
{
    public class DialogSystem : Singleton<DialogSystem>
    {
        [SerializeField] private DialogManager dialogManager;

        public void Say(string whatToSay)
        {
            Say(new DialogData(whatToSay, "Li"));
        }
        
        public void Say(params string[] values)
        {
            var dialogTexts = new List<DialogData>();

            values.ToList().ForEach(s =>
                {
                    var dialogData = new DialogData(s, "Li");
                    dialogTexts.Add(dialogData);
                }
            ); 
            Say(dialogTexts);
        }
        
        public void Say(params DialogData[] values)
        {
            Say(values.ToList());
        }
        
        [Button]
        public void Say(List<DialogData> whatToSay)
        {
            dialogManager.Hide();
            
            
            dialogManager.Show(whatToSay);
        }
    }
}
