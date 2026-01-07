using Pillz.Data;
using UnityEngine;

namespace Pillz.Service
{
    public class ToastService : Service
    {
        [SerializeField] private Toast _toastPrefab;
        [SerializeField] private ToastConfiguration _toastConfiguration;

        private readonly Color _defaultColor = Color.white;

        private void Awake()
        {
            Debug.Assert(_toastPrefab != null, nameof(_toastPrefab) + " != null");
            Debug.Assert(_toastConfiguration != null, nameof(_toastConfiguration) + " != null");

            ServiceLocator.ToastService = this;
        }

        public void MakeToast(string message, Transform t, Color color, Vector3 baseScale, Vector3 punchScale,
            bool isParented = false, float heightOffset = 0f)
        {
            Vector3 directionToCamera = t.position - Camera.main.transform.position;
            Toast toast = Instantiate(
                _toastPrefab,
                t.position + Vector3.up * heightOffset,
                Quaternion.LookRotation(directionToCamera, Vector3.up));
            toast.MessageColor = color;
            toast.Message = message;
            toast.BaseScale = baseScale;
            toast.PunchScale = punchScale;
            if (isParented)
            {
                toast.transform.SetParent(t);
            }

            // toast.InitialImpulse =
            //     Vector3.up * _toastConfiguration.UpwardImpulseForce
            //     + Vector3.right * _toastConfiguration.LateralImpulseForce;
        }

        public void MakeToast(string message, Transform t, Color color, bool isParented = false)
        {
            MakeToast(message, t, color, Vector3.one, Vector3.one, isParented);
        }

        public void MakeToast(string message, Transform t, Vector3 scale, bool isParented = false)
        {
            MakeToast(message, t, Color.white, scale, Vector3.one, isParented);
        }

        public void MakeToast(string message, Transform t, bool isParented = false)
        {
            MakeToast(message, t, Color.white, Vector3.one, Vector3.one, isParented);
        }
    }

    public partial class ServiceLocator
    {
        public static ToastService ToastService { get; set; }
    }
}