using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Components
{
    [Serializable]
    public class TemporaryHealthView
    {
        [SerializeField] private GameObject temporaryView;
        [SerializeField] private TMP_Text temporaryLabel;

        public GameObject TemporaryView => temporaryView;
        public TMP_Text TemporaryLabel => temporaryLabel;
    }
}