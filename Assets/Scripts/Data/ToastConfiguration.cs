using DG.Tweening;
using UnityEngine;

namespace Pillz.Data
{
    [CreateAssetMenu(fileName = "ToastConfig", menuName = "Config/Toast", order = 0)]
    public class ToastConfiguration : ScriptableObject
    {
        [field: SerializeField] public float TweenDuration {get; private set; } = 0.25f;
        [field: SerializeField] public Ease Ease { get; private set; } = Ease.Linear;
        [field: SerializeField] public Color FaceColor { get; private set; } = Color.blueViolet;
        [field: SerializeField] public float UpwardImpulseForce  { get; private set; } = 50f;
        [field: SerializeField] public float LateralImpulseForce { get; private set; } = 50f;
    }
}