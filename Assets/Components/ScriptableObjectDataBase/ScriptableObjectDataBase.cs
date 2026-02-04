// Watch out about call order of using !
// Using Components.Data before System.Collections.Generic can create bug here.
using System;
using System.Collections.Generic;
using Components.Data;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Components.SODB
{
    public static class ScriptableObjectDataBase
    {
        private static readonly Dictionary<Type, Dictionary<string, Object>> SO_DATABASE = new();

        // Syntax for calling the following method just before the game start.
        // It's necessary because we have desactivated the domain reload in Unity settings.
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        // Static constructors dont have accessor (public, private, etc).
        // Static constructors can't be directly called.
        // But if a member of a static construcor is called, the constructor is called too before return a value.
        private static void Initialize()
        {
            Debug.Log("Initializing ScriptableObjectDataBase...");

            SO_DATABASE.Clear();
            RegisterLevelParameters<SOLevelParameters>();
            RegisterSlopeParameters<SOSlopeParameters>();
        }

        private static void RegisterLevelParameters<T>() where T : Object
        {
            var type = typeof(T);

            if (SO_DATABASE.ContainsKey(type))
            {
                Debug.LogWarning($"ScriptableObject with name {type.Name} already exists in database.");
                return;
            }

            SO_DATABASE[type] = new Dictionary<string, Object>();

            T[] templates = Resources.LoadAll<T>("");
            foreach (var template in templates)
            {
                SO_DATABASE[type][template.name] = template;
            }

            Debug.Log($"[DATABASE] Loaded {templates.Length} {type.Name}(s)");
        }

        private static void RegisterSlopeParameters<T>() where T : Object
        {
            var type = typeof(T);

            if (SO_DATABASE.ContainsKey(type))
            {
                Debug.LogWarning($"ScriptableObject with name {type.Name} already exists in database.");
                return;
            }

            SO_DATABASE[type] = new Dictionary<string, Object>();

            T[] templates = Resources.LoadAll<T>("");
            foreach (var template in templates)
            {
                SO_DATABASE[type][template.name] = template;
            }

            Debug.Log($"[DATABASE] Loaded {templates.Length} {type.Name}(s)");
        }

        // Method that get a SOLevelParamters.
        // Security if a SOLevelParamter's name not found.
        public static T Get<T>(string name) where T : Object
        {
            var type = typeof(T);

            if (SO_DATABASE.TryGetValue(type, out var typeDictionary))
            {
                if (typeDictionary.TryGetValue(name, out var scriptableObject))
                {
                    return scriptableObject as T;
                }
            }

            Debug.LogError("Unable to find a scriptable object with name: " + name + " of type " + type);
            return null;
        }
    }
}
