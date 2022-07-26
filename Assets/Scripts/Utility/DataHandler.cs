using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Utility
{
    /// <summary>
    /// 未実装　Fix: nullチェックとロジックの見直し、データが格納されていない問題あり
    /// </summary>
    public sealed class DataHandler
    {
        /*
        private string dataPath = null;

        public DataHandler(){ }

        public DataHandler(string dataPath)
        {
            this.dataPath = dataPath;
        }

        public IEnumerable<T> GetCollectionData<T>()
        {
            return GetCollectionData<T>(dataPath);
        }

        public IEnumerable<T> GetCollectionData<T>(string dataPath)
        {
            IEnumerable<T> tmpCollection = null;
            //JsonからLevel情報を受け取り、mazeLevelListに格納。
            try
            {
                using (StreamReader file = new StreamReader(dataPath))
                {
                    string dataString = file.ReadToEnd();
                    var wrapper = JsonUtility.FromJson<Wrapper<T>>(dataString);
                    Debug.Log(wrapper.tmpArray);
                    tmpCollection = wrapper.tmpArray;

                }

            }
            catch(Exception e)
            {
                Debug.LogError(e);
            }

            return tmpCollection;
        }
        
        private class Wrapper<T>
        {
            public T[] tmpArray;
        }
        */
    }
}