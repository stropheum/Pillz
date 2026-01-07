using DG.Tweening;
using UnityEngine;

namespace Pillz
{
    public class ScaleUpOnSpawn : MonoBehaviour
    {
        private void Awake()
        {
            transform.localScale = Vector3.zero;
        }

        private void Start()
        {
            transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.InSine);
        }
    }
}