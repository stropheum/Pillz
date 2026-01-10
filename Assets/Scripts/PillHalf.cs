using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

namespace Pillz
{
    [RequireComponent(typeof(ProBuilderMesh))]
    [RequireComponent(typeof(MeshRenderer))]
    public class PillHalf : MonoBehaviour
    {
        private ProBuilderMesh _probuilderMesh;
        private MeshRenderer _meshRenderer;

        private void Awake()
        {
            _probuilderMesh = GetComponent<ProBuilderMesh>();
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        public void SetColor(Color color)
        {
            IList<Color> colors = _probuilderMesh.colors;
            foreach (SharedVertex sharedVertex in _probuilderMesh.sharedVertices)
            {
                foreach (int index in sharedVertex)
                {
                    colors[index] = color;
                }
            }
        }
    }
}