using TMPro;
using UnityEngine;

namespace SceneTestingScripts
{
    [RequireComponent(typeof(Collider2D))]
    public class DamageTester : MonoBehaviour, IDamageable
    {
        private GameObject _ui;
        private TextMeshProUGUI _text;

        private float _sumDamage = 0;

        private void Awake()
        {
            _ui = new GameObject("Tester UI");
            _ui.transform.SetParent(transform);

            var canvas = _ui.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.WorldSpace;
            canvas.sortingOrder = 999;

            var rectTransform = _ui.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(1, 1);
            rectTransform.anchoredPosition3D = Vector3.zero;

            var textObject = new GameObject("Text");
            textObject.transform.SetParent(canvas.transform);

            _text = textObject.AddComponent<TextMeshProUGUI>();
            _text.color = Color.red;
            _text.fontSize = 0.5f;
            _text.text = "Test";

            var textRectTransform = textObject.GetComponent<RectTransform>();
            textRectTransform.anchoredPosition3D = Vector3.zero;
            textRectTransform.sizeDelta = Vector2.zero;
            textRectTransform.anchorMin = Vector2.zero;
            textRectTransform.anchorMax = Vector2.one;
            textRectTransform.pivot = new Vector2(0.5f, 0.5f);

            gameObject.tag = "Enemy";
        }
        void IDamageable.TakeDamage(float damage)
        {
            _sumDamage += damage;
            _text.text = _sumDamage.ToString();
        }
    }
}