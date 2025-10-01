using System.Collections;
using UnityEngine;

namespace LA.UI.Description
{
    public class DescriptionController : MonoBehaviour
    {
        [SerializeField] private DescriptionUI _descriptionUI;


        private void Start()
        {
            IconWithInfo.OnHoverEnter += OnIconHoverEnter;
            IconWithInfo.OnHoverExit += OnIconHoverExit;
        }


        private void OnIconHoverEnter(IconWithInfo iconWithInfo)
        {
            _descriptionUI.SetData(iconWithInfo.Sprite, iconWithInfo.Name, iconWithInfo.Description);
            _descriptionUI.SetPosition(iconWithInfo.transform.position);
            _descriptionUI.Show();

            StartCoroutine(CheckForInactive(iconWithInfo));
        }


        private void OnIconHoverExit(IconWithInfo iconWithInfo) => _descriptionUI.Hide();


        private void OnDestroy()
        {
            IconWithInfo.OnHoverEnter -= OnIconHoverEnter;
            IconWithInfo.OnHoverExit -= OnIconHoverExit;
        }


        private IEnumerator CheckForInactive(IconWithInfo iconWithInfo)
        {
            yield return new WaitUntil(() => iconWithInfo.gameObject.activeInHierarchy == false);
            _descriptionUI.Hide();
        }
    }
}