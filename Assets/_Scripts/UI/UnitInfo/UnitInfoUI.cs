using System.Collections.Generic;
using DG.Tweening;
using LA.Gameplay.AbilitySystem;
using LA.Gameplay.Stat;
using LA.Gameplay.WeaponSystem;
using LA.UI.FillBar;
using LA.UI.Popup;
using SW.Utilities.LoadAsset;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LA.UI.UnitInfo
{
    public class UnitInfoUI : TemplateUI
    {
        [SerializeField] private Image _unitImage;
        [SerializeField] private Vector3 _swingDirection;
        [SerializeField] private TextMeshProUGUI _unitName;
        [SerializeField] private FillBarUI _healthBar;
        [SerializeField] private StatCard _agilityCard;
        [SerializeField] private StatCard _strengthCard;
        [SerializeField] private StatCard _enduranceCard;
        [SerializeField] private IconWithInfo _weaponIcon;
        [SerializeField] private List<IconWithInfo> _abilityIcons;

        [SerializeField] private float _attackSwingPercentage = 0.3f;


        public void CreatePopup(string popupPath)
        {
            PopupUI popup = PopupUI.CreatePopup(LoadAssetUtility.Load<PopupUI>(popupPath), _unitImage.transform.position,
                                _unitImage.transform.parent);

            popup.SetText("DODGE").MovePopup(Vector3.up * 50, 0.3f).FadePopup(0, 0.2f).DestroyOnComplete();
        }


        public void AnimateAttack(float duration)
        {
            Vector3 startPosition = _unitImage.transform.position;
            Vector3 endPosition = startPosition + _swingDirection;

            AnimateSwing(duration, startPosition, endPosition);
        }


        public void AnimateDodge(float duration)
        {
            Vector3 startPosition = _unitImage.transform.position;
            Vector3 endPosition = startPosition - _swingDirection * 0.5f;

            AnimateSwing(duration, startPosition, endPosition);
        }


        private void AnimateSwing(float duration, Vector3 startPosition, Vector3 endPosition)
        {
            Sequence seq = DOTween.Sequence().SetLink(this.gameObject);

            seq.Append(_unitImage.transform.DOMove(endPosition, duration * _attackSwingPercentage)
                                 .SetEase(Ease.OutQuad));

            seq.Append(_unitImage.transform.DOMove(startPosition, duration * (1 - _attackSwingPercentage))
                                 .SetEase(Ease.InOutSine));
        }


        public void SetUnitImage(Sprite unitImage) => _unitImage.sprite = unitImage;

        public void SetUnitName(string unitName) => _unitName.text = unitName;

        public void SetHealth(float currentValue, float maxValue) => _healthBar.Fill(currentValue, maxValue);


        public void SetStats(Dictionary<StatType, int> stats)
        {
            _agilityCard.UpdateData(stats[StatType.Agility].ToString());
            _strengthCard.UpdateData(stats[StatType.Strength].ToString());
            _enduranceCard.UpdateData(stats[StatType.Endurance].ToString());
        }


        public void SetAbilities(List<AbilitySO> newAbilities)
        {
            for (int i = 0; i < _abilityIcons.Count; i++)
            {
                if (i < newAbilities.Count)
                {
                    _abilityIcons[i].SetData(newAbilities[i].Icon, newAbilities[i].Name, newAbilities[i].Description);
                    _abilityIcons[i].Show();
                }
                else
                {
                    _abilityIcons[i].Hide();
                }
            }
        }


        public void SetWeaponData(WeaponSO weaponSource)
        {
            _weaponIcon.SetData(weaponSource.Sprite, weaponSource.Name, weaponSource.Description);
        }


        public override void Show()
        {
            base.Show();
            _unitImage.gameObject.SetActive(true);
        }


        public override void Hide()
        {
            base.Hide();
            _unitImage.gameObject.SetActive(false);
        }
    }
}