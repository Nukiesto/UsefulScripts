using System;
using UnityEngine;

namespace UsefulScripts.UnityScripts.UsefulBehaviours
{
    public class SetMaterialUtility : MonoBehaviour
    {
        [SerializeField] private Material materialToSet;
        [SerializeField] private Renderer rendererToSet;

        private void Start()
        {
            rendererToSet.material = (Material) Activator.CreateInstance(materialToSet.GetType());
        }
    }
}