using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class CatRuleSet : MonoBehaviour
{
    [SerializeField] new private Rigidbody rigidbody;
    [SerializeField] float speed = 10;
    private List<CatRuleSet> catsInMoveSight = new List<CatRuleSet>();

    private void OnTriggerEnter(Collider other)
    {
        CatRuleSet otherCat = other.gameObject.GetComponent<CatRuleSet>();
        if (otherCat != null)
        {
            if (!catsInMoveSight.Contains(otherCat))
            {
                catsInMoveSight.Add(otherCat);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CatRuleSet otherCat = other.gameObject.GetComponent<CatRuleSet>();
        if (otherCat != null)
        {
            catsInMoveSight.Remove(otherCat);
        }
    }

    [SerializeField] Collider MoveInwardsCollider;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 center = GetCenterOfCatsInsight();
        if (center != Vector3.zero)
        {
            Vector3 direction = (center - transform.position) * speed * Time.deltaTime;
            rigidbody.MovePosition(transform.position + direction);
        }
    }

    Vector3 GetCenterOfCatsInsight()
    {
        int num = 0;
        Vector3 center = new Vector3();

        foreach (CatRuleSet cat in catsInMoveSight)
        {
            center += cat.transform.position;
            num += 1;
        }

        if (num > 0)
        {
            center /= num;
        }

        return center;
    }
}
