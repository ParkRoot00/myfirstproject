using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    //������ ���� ����
    public GameObject[] prefabs;
    //Ǯ ��� ����Ʈ
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
        //������ pool�� ��Ȱ��ȭ ������Ʈ ����
          
        foreach(GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                //ã���� select�� �Ҵ�
                select = item;
                select.SetActive(true);
                break;
            }
        }

        //�� ã���� ���Ӱ� ������ select�� �Ҵ�
        if (!select)
        {
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }

        return select;
    }
}
