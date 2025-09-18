using System.Collections.Generic;
using UnityEngine;

namespace LA
{
    [CreateAssetMenu(fileName = "ClassesDatabase", menuName = "Game/ClassesDatabase")]
    public class ClassesDatabase : ScriptableObject
    {
        public IReadOnlyList<ClassSO> Classes => _classes;
        [SerializeField] private List<ClassSO> _classes = new List<ClassSO>();
    }
}