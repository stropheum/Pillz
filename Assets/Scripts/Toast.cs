using System.Collections;
using DG.Tweening;
using Pillz.Data;
using TMPro;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Pillz
{
    [RequireComponent(typeof(TextMeshPro))]
    public class Toast : MonoBehaviour
    {
        [SerializeField] private ToastConfiguration _config;
        
        public string Message { get; set; }
        public Color MessageColor { get; set; }
        public Vector3 BaseScale { get; set; } = Vector3.one;
        public Vector3 PunchScale { get; set; } = Vector3.one;
        
        private TextMeshPro _textMeshPro;

        private void Awake()
        {
            Debug.Assert(_config != null, nameof(_config) + " != null");
            
            _textMeshPro = GetComponent<TextMeshPro>();
            
            _textMeshPro.enabled = false;
        }

        private void FixedUpdate()
        {
            Camera playerCamera = Camera.main;
            Debug.Assert(playerCamera != null);
            Vector3 directionToCamera = transform.position - playerCamera.transform.position;
            transform.rotation = Quaternion.LookRotation(directionToCamera);
        }

        public IEnumerator Start()
        {
            // set initial values
            _textMeshPro.text = Message;
            _textMeshPro.rectTransform.localScale = BaseScale;
            _textMeshPro.faceColor = MessageColor;
            _textMeshPro.enabled = true;
            
            // construct the tween sequence
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_textMeshPro.rectTransform.DOPunchScale(PunchScale, _config.TweenDuration, vibrato: 0));
            sequence.Join(_textMeshPro.transform.DOMoveY(transform.position.y + 0.25f, _config.TweenDuration));
            // sequence.Join(_textMeshPro.DOFaceColor(_config.FaceColor, _config.TweenDuration).SetEase(_config.Ease));

            // Let the animation complete and then die
            yield return sequence.WaitForCompletion();
            Destroy(gameObject);
        }
    }
}