using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameObjectTestRoot : MonoBehaviour
{
    [SerializeField] private GameObjectTester m_instanceOnActiveNodePrefab;
    [SerializeField] private GameObjectTester m_instanceOnDeactiveNodePrefab;
    [SerializeField] private GameObjectTester m_instanceOnActiveNodeDisabledPrefab;
    [SerializeField] private GameObjectTester m_instanceOnDeactiveNodeDisabledPrefab;
    [SerializeField] private Transform m_activeNode;
    [SerializeField] private Transform m_deactiveNode;

    [SerializeField] private GameObjectTester m_enabledOnActiveNode;
    [SerializeField] private GameObjectTester m_disabledOnActiveNode;
    [SerializeField] private GameObjectTester m_disabledOnActiveNodeToEnable;
    [SerializeField] private GameObjectTester m_enabledOnActiveNodeToDisable;
    private GameObjectTester m_instanceOnActiveNode;
    private GameObjectTester m_instanceDisabledOnActiveNode;
    [SerializeField] private GameObjectTester m_enabledOnDeactiveNode;
    [SerializeField] private GameObjectTester m_disabledOnDeactiveNode;
    [SerializeField] private GameObjectTester m_disabledOnDeactiveNodeToEnable;
    [SerializeField] private GameObjectTester m_enabledOnDeactiveNodeToDisable;
    private GameObjectTester m_instanceOnDeactiveNode;
    private GameObjectTester m_instanceDisabledOnDeactiveNode;

    private void Start()
    {
        StartCoroutine(Test());
    }

    private IEnumerator Test()
    {
        InstancePrefab();
        yield return new WaitForSeconds(1f);
        ChangeAbility();
        yield return new WaitForSeconds(1f);
        DestroyGameObjects();
        yield return new WaitForSeconds(1f);
        TraceLog();
    }

    private void InstancePrefab()
    {
        m_instanceOnActiveNode = Instantiate(m_instanceOnActiveNodePrefab);
        Log(m_instanceOnActiveNode.name, "! Instantiate");
        m_instanceOnActiveNode.gameObject.SetActive(false);
        m_instanceOnActiveNode.transform.SetParent(m_activeNode);

        m_instanceDisabledOnActiveNode = Instantiate(m_instanceOnActiveNodeDisabledPrefab);
        Log(m_instanceDisabledOnActiveNode.name, "! Instantiate");
        m_instanceDisabledOnActiveNode.transform.SetParent(m_activeNode);

        m_instanceOnDeactiveNode = Instantiate(m_instanceOnDeactiveNodePrefab);
        Log(m_instanceOnDeactiveNode.name, "! Instantiate");
        m_instanceOnDeactiveNode.transform.SetParent(m_deactiveNode);

        m_instanceDisabledOnDeactiveNode = Instantiate(m_instanceOnDeactiveNodeDisabledPrefab);
        Log(m_instanceDisabledOnDeactiveNode.name, "! Instantiate");
        m_instanceDisabledOnDeactiveNode.transform.SetParent(m_deactiveNode);
    }

    private void ChangeAbility()
    {
        Log(m_disabledOnActiveNodeToEnable.name, "+ SetActive(true)");
        m_disabledOnActiveNodeToEnable.gameObject.SetActive(true);

        Log(m_disabledOnDeactiveNodeToEnable.name, "+ SetActive(true)");
        m_disabledOnDeactiveNodeToEnable.gameObject.SetActive(true);

        Log(m_enabledOnActiveNodeToDisable.name, "- SetActive(false)");
        m_enabledOnActiveNodeToDisable.gameObject.SetActive(false);

        Log(m_enabledOnDeactiveNodeToDisable.name, "- SetActive(false)");
        m_enabledOnDeactiveNodeToDisable.gameObject.SetActive(false);
    }

    private void DestroyGameObjects()
    {
        Log(m_enabledOnActiveNode.name, "# Destroy");
        Log(m_disabledOnActiveNode.name, "# Destroy");
        Log(m_disabledOnActiveNodeToEnable.name, "# Destroy");
        Log(m_enabledOnActiveNodeToDisable.name, "# Destroy");
        Log(m_instanceOnActiveNode.name, "# Destroy");
        Log(m_instanceDisabledOnActiveNode.name, "# Destroy");

        Destroy(m_activeNode.gameObject);

        Log(m_enabledOnDeactiveNode.name, "# Destroy");
        Log(m_disabledOnDeactiveNode.name, "# Destroy");
        Log(m_disabledOnDeactiveNodeToEnable.name, "# Destroy");
        Log(m_enabledOnDeactiveNodeToDisable.name, "# Destroy");
        Log(m_instanceOnDeactiveNode.name, "# Destroy");
        Log(m_instanceDisabledOnDeactiveNode.name, "# Destroy");

        Destroy(m_deactiveNode.gameObject);
    }

    private static IDictionary<string, IList<string>> m_logs = new Dictionary<string, IList<string>>();
    public static void Log(string name, string method)
    {
        IList<string> list;
        if (m_logs.TryGetValue(name, out list))
        {
            list.Add(method);
        }
        else
        {
            list = new List<string>()
            {
                method
            };
            m_logs.Add(name, list);
        }
    }

    private static void TraceLog()
    {
        foreach (var log in m_logs)
        {
            var message = $"======================\n{log.Key}\n";
            foreach (var item in log.Value)
            {
                message += $"    {item}\n";
            }

            Debug.Log(message);
        }
    }
}
