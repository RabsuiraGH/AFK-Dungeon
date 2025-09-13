using UnityEngine;

namespace LA.UI
{
    public class TemplateUI : MonoBehaviour
    {
        [SerializeField] protected Transform _parentTransform;


        public virtual void Show()
        {
            _parentTransform.gameObject.SetActive(true);
        }


        public virtual void Hide()
        {
            _parentTransform.gameObject.SetActive(false);
        }
    }
}