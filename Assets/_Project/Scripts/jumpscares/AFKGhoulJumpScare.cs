using System;
using _Project.Scripts.player;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.jumpscares
{
    public class AFKGhoulJumpScare: MonoBehaviour
    {
        [SerializeField] private Camera camera;
        [SerializeField] private GameObject model;
        [SerializeField] private AudioClip scareSound;

        private bool isScaring;
        
        private void Start()
        {
            model.SetActive(false);
        }


        [Button]
        public void Scare()
        {
            if (isScaring) return;
            isScaring = true;
            var initialPos = camera.transform.position;
            var initialRot = camera.transform.rotation;
            model.SetActive(true);
            PlayerGlobal.Instance.PlaySound(scareSound);
            camera.DOShakePosition(1.0f).OnComplete(() =>
            {
                isScaring = false;
                model.SetActive(false);
                camera.transform.position = initialPos;
                camera.transform.rotation = initialRot;
            });
        }

    }
}