using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    //프리팹 저장 변수
    public GameObject[] prefabs;
    //풀 담당 리스트
    List<GameObject>[] pools;

    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;
        //선택한 pool의 비활성화 오브젝트 접근
          
        foreach(GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                //찾으면 select에 할당
                select = item;
                select.SetActive(true);
                break;
            }
        }

        //못 찾으면 새롭게 생성해 select에 할당
        if (!select)
        {
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }

        return select;
    }
}
