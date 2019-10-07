using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace AINamesGenerator
{
    public static class Utils
    {
        [Serializable]
        private class NamesList
        {
            public List<string> names;
        }

        static NamesList namesList;
        static NamesList CurrentNamesList
        {
            get
            {
                if (namesList == null)
                {
                    TextAsset textAsset = Resources.Load("Texts/NamesList") as TextAsset;
                    namesList = JsonUtility.FromJson<NamesList>(textAsset.text);
                }
                return namesList;
            }
        }

        public static string GetRandomName()
        {
            return CurrentNamesList.names[UnityEngine.Random.Range(0, CurrentNamesList.names.Count)];
        }

        public static List<string> GetRandomNames(int nbNames)
        {
            if (nbNames > CurrentNamesList.names.Count)
                throw new Exception("Asking for more random names than there actually are!");
            
            NamesList copy = new NamesList();
            copy.names = new List<string>(CurrentNamesList.names);

            List<string> result = new List<string>();

            for (int i = 0; i < nbNames; i++)
            {
                int rnd = UnityEngine.Random.Range(0, copy.names.Count);
                result.Add(copy.names[rnd]);
                copy.names.RemoveAt(rnd);
            }

            return result;
        }
    }
}