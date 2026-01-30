// Watch out about call order of using !
// Using Components.Data before System.Collections.Generic can create bug here.
using System.Collections.Generic;
using Components.Data;
using UnityEngine;

namespace Components.SODB
{
    public static class ScriptableObjectDataBase
    {
        private static readonly Dictionary<string, SOLevelParameters> DATABASE = new();
        
        // Static constructors dont have accessor (public, private, etc).
        // Static constructors can't be directly called.
        // But if a member of a static construcor is called, the constructor is called too before return a value.
        static ScriptableObjectDataBase()
        {
            // Loading assets from a path in all Unity's project.
            // Here, script look in all paths that contain "Data".
            var scriptableObjects = Resources.LoadAll<SOLevelParameters>("Data");
            
            foreach (var scriptableObject in scriptableObjects)
            {
                DATABASE.Add(scriptableObject.name, scriptableObject);
            }
        }

        // Method that get a SOLevelParamters.
        // Security if a SOLevelParamter's name not found.
        public static SOLevelParameters GetByName(string name)
        {
            if (DATABASE.TryGetValue(name, out SOLevelParameters levelParameters))
            {
                return levelParameters;
            }

            Debug.LogWarning($"ScriptableObject with name {name} not found in database.");
            return null;
        }
    }
}
