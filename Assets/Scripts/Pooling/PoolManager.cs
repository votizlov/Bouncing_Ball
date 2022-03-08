using UnityEngine;

namespace Core.Pooling
{
    public class PoolManager
    {
        private static PoolPart[] pools;
        private static GameObject objectsParent;

        [System.Serializable]
        public struct PoolPart {
            public string name; //имя префаба
            public PoolObject prefab; //сам префаб, как образец
            public int count; //количество объектов при инициализации пула
            public ObjectPooling poop; //сам пул
        }
    
        public static void Initialize(PoolPart[] newPools) {
            pools = newPools; //заполняем информацию
            objectsParent = new GameObject ();
            objectsParent.name = "Pool"; //создаем на сцене объект Pool, чтобы не заслонять иерархию
            for (int i=0; i<pools.Length; i++) {
                if(pools[i].prefab!=null) {  
                    pools[i].poop = new ObjectPooling(); //создаем свой пул для каждого префаба
                    pools[i].poop.Initialize(pools[i].count, pools[i].prefab, objectsParent.transform); 
//инициализируем пул заданным количество объектов
                }
            }
        }
        public static GameObject GetObject (string name, Vector3 position, Quaternion rotation) {
            GameObject result = null;
            if (pools != null) {
                for (int i = 0; i < pools.Length; i++) {
                    if (string.Compare (pools [i].name, name) == 0) { //если имя совпало с именем префаба пула
                        result = pools[i].poop.GetObject ().gameObject; //дергаем объект из пула
                        result.transform.position = position;
                        result.transform.rotation = rotation; 
                        result.SetActive (true); //выставляем координаты и активируем
                        return result;
                    }
                }
            } 
            return result; //если такого объекта нет в пулах, вернет null
        }
    }
}
