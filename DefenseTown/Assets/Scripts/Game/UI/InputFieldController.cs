using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System;
namespace My.UI
{

    public class InputFieldController : MonoBehaviour
    {
        [SerializeField] InputField _inputField;
        [SerializeField] Text _originText;
        [SerializeField] Text _hookText;

        [SerializeField] int _charLimit;

        public event Func<string, int> EventGetMaxLimit;

        public int CharLimit
        {
            get { return _charLimit; }
            set
            {
                _charLimit = value;
                _inputField.characterLimit = value;
            }
        }

        private void Start()
        {
            _inputField.characterLimit = _charLimit;
        }

        public void OnChangeInputFieldValue(string value)
        {
            bool isContainKorean = Regex.IsMatch(value, @"[¤¡-¤¾°¡-ÆR]");

            int maxLength = isContainKorean ? 5 : 5;
            if(EventGetMaxLimit != null)
            {
                maxLength = EventGetMaxLimit(value);
            }
            _inputField.characterLimit = maxLength;

            bool isLimit = value.Length == maxLength;
            ShowHookText(isLimit);

            if(isLimit)
            {
                SetHookText(value);
            }
        }

        void ShowHookText(bool bShow)
        {
            _hookText.enabled = bShow;
            _originText.enabled = !bShow;
        }

        void SetHookText(string text)
        {
            _hookText.text = text;   
        }
    }
}
