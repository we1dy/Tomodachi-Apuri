using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static VariableStore;

namespace TESTING
{
    public class VariableStoreTesting : MonoBehaviour
    {
        public int var_int = 0;
        public float var_flt = 0;
        public bool var_bool = false;
        public string var_str = "";

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            VariableStore.CreateDatabase("DB_Links");

            VariableStore.CreateVariable("DB_Links.L_int", var_int, () => var_int, value => var_int = value);
            VariableStore.CreateVariable("DB_Links.L_flt", var_flt, () => var_flt, value => var_flt = value);
            VariableStore.CreateVariable("DB_Links.L_bool", var_bool, () => var_bool, value => var_bool = value);
            VariableStore.CreateVariable("DB_Links.L_str", var_str, () => var_str, value => var_str = value);

            VariableStore.CreateDatabase("DB_Numbers");
            VariableStore.CreateDatabase("DB_Booleans");

            VariableStore.CreateVariable("DB_Numbers.num1", 1);
            VariableStore.CreateVariable("DB_Numbers.num6", 6);
            VariableStore.CreateVariable("DB_Booleans.lightIsOn", true);
            VariableStore.CreateVariable("DB_Numbers.float1", 7.5f);
            VariableStore.CreateVariable("str1", "WTF");
            VariableStore.CreateVariable("str2", "NO");

            VariableStore.PrintAllDatabases();
            VariableStore.PrintAllVariables();

        }

        private void Update()
        {
            if (Keyboard.current.qKey.wasPressedThisFrame)
            {
                VariableStore.PrintAllVariables();
                Debug.Log($"Viktor");
            }
            
            if (Keyboard.current.aKey.wasPressedThisFrame)
            {
                string variable = "DB_Numbers.num1";
                VariableStore.TryGetValue(variable, out object v);
                VariableStore.TrySetValue(variable, (int)v + 5);
                Debug.Log($"Iwaizumi");
            }
            
            if (Keyboard.current.sKey.wasPressedThisFrame)
            {
                VariableStore.TryGetValue("DB_Numbers.num1", out object num1);
                VariableStore.TryGetValue("DB_Numbers.num6", out object num2);

                Debug.Log($"num1 + num6 = {(int)num1 + (int)num2}");
                Debug.Log($"Damien");
            }

            if (Keyboard.current.dKey.wasPressedThisFrame)
            {
                if (VariableStore.TryGetValue("DB_Booleans.lightIsOn", out object lightIsOn) && lightIsOn is bool)
                    VariableStore.TrySetValue("DB_Booleans.lightIsOn", !(bool)lightIsOn);
            }
            
            if (Keyboard.current.fKey.wasPressedThisFrame)
            {
                VariableStore.TryGetValue("str1", out object WTF);
                VariableStore.TryGetValue("str2", out object NO);

                VariableStore.TrySetValue("str1", (string)WTF + NO);
            }

            if (Keyboard.current.zKey.wasPressedThisFrame)
            {
                VariableStore.TryGetValue("DB_Links.L_int", out object linked_integers);
                VariableStore.TrySetValue("DB_Links.L_int", (int)linked_integers + 5);
            }
            
            if (Keyboard.current.gKey.wasPressedThisFrame)
            {
                VariableStore.RemoveVariable("DB_Links.L_int");
                VariableStore.RemoveVariable("DB_Booleans.lightIsOn");
            } 
            
            if (Keyboard.current.xKey.wasPressedThisFrame)
            {
                VariableStore.TryGetValue("DB_Links.L_flt", out object v);
                VariableStore.TrySetValue("DB_Links.L_flt", (float)v * 1.75f);
            }  
            
            if (Keyboard.current.vKey.wasPressedThisFrame)
            {
                VariableStore.TryGetValue("DB_Links.L_str", out object v);
                VariableStore.TrySetValue("DB_Links.L_str", (string)v + Random.Range(1000, 2000));
            }  
            
            if (Keyboard.current.cKey.wasPressedThisFrame)
            {
                VariableStore.TryGetValue("DB_Links.L_bool", out object v);
                VariableStore.TrySetValue("DB_Links.L_bool", !(bool)v);
            } 
            
            if (Keyboard.current.hKey.wasPressedThisFrame)
            {
                VariableStore.RemoveAllVariables();
            }
        }
    }
}