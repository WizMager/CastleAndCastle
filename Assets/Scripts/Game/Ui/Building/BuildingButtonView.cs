using Db.Buildings;
using SimpleUi.Abstracts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Ui.Building
{
    public class BuildingButtonView : UiView
    {
        public EBuildingType Id;
        public Image Icon;
        public TextMeshProUGUI NameText;
        public TextMeshProUGUI PriceText;
        public Button Btn;

        public void Setup(
            EBuildingType id, 
            Sprite icon, 
            string title, 
            string price
        )
        {
            Id = id;
            Icon.sprite = icon;
            NameText.text = title;
            PriceText.text = price;
        }

        public void SetIntractable(bool value)
        {
            Btn.interactable = value;
        }
    }
}