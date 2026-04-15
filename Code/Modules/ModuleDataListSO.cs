using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.Modules
{
    [CreateAssetMenu(fileName = "ModuleController", menuName = "SO/Module/Controller", order = 0)]
    public class ModuleDataListSO : ScriptableObject
    {
        public List<ModuleDataSO> moduleData = new List<ModuleDataSO>();
    }
}