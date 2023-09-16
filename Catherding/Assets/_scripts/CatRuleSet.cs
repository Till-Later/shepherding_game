using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class CatRuleSet : MonoBehaviour
{
    [SerializeField] new private Rigidbody rigidbody;
    [SerializeField] float rotationSpeed = 10;
    [SerializeField] float speed = 10;

    [SerializeField][Range(1,100)] float CoherenceWeight = 5;
    [SerializeField][Range(1,100)] float CoherenceDistance = 10;
    [SerializeField][Range(1, 100)] float SeparationWeight = 5;
    [SerializeField][Range(1, 100)] float SeparationDistance = 5;
    [SerializeField][Range(1, 100)] float AlignmentWeight = 5;
    [SerializeField][Range(1, 100)] float AlignmentDistance = 5;
    [SerializeField][Range(1, 100)] float LaserpointerWeight = 5;
    [SerializeField][Range(1, 100)] float LaserpointerDistance = 5;

    GameObject laserpointer;

    // Start is called before the first frame update
    void Start()
    {
laserpointer = GameObject.FindAnyObjectByType<LaserController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        CatRuleSet[] cats = GameObject.FindObjectsOfType<CatRuleSet>();

        List<CatRuleSet> coherenceCats = FilterCatsByDistance(cats, CoherenceDistance);
        Vector3 center = GetCenterOfCats(coherenceCats);


        Vector3 coherenceDirection = center - transform.position;
        coherenceDirection.Normalize();

        List<CatRuleSet> separationCats = FilterCatsByDistance(cats, SeparationDistance);
        Vector3 separationDirection = GetAwayVectorOfCats(separationCats);
        separationDirection.Normalize();

        List<CatRuleSet> alignmentCats = FilterCatsByDistance(cats, AlignmentDistance);
        Vector3 alignmentDirection = GetAlignmentDirection(alignmentCats);
        alignmentDirection.Normalize();

        Vector3 laserpointerDirection = new Vector3();
        if (Vector3.Distance(laserpointer.transform.position, transform.position) < LaserpointerDistance)
        {
        laserpointerDirection = laserpointer.transform.position - transform.position;
        laserpointerDirection.Normalize();
        }

        Vector3 desiredDirection = (coherenceDirection * CoherenceWeight) + (separationDirection * SeparationWeight) + (alignmentDirection * AlignmentWeight) + (laserpointerDirection * LaserpointerWeight);
        desiredDirection = desiredDirection / (CoherenceWeight + SeparationWeight + AlignmentWeight + LaserpointerWeight);

        desiredDirection.Normalize();

        gizmosPos = transform.position + desiredDirection;

        if (desiredDirection != Vector3.zero)
        {
            rigidbody.MoveRotation(Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredDirection), Time.deltaTime * rotationSpeed));
        }

        Vector3 actualDirection = rigidbody.rotation * Vector3.forward;
        actualDirection.Normalize();
        
        rigidbody.velocity = (desiredDirection + actualDirection) * Time.deltaTime * speed;
    }

    private Vector3 gizmosPos;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        //Gizmos.DrawSphere(gizmosPos, 1);
    }

    List<CatRuleSet> FilterCatsByDistance(CatRuleSet[] cats, float distance)
    {
        List<CatRuleSet> o = new List<CatRuleSet>();

        foreach (CatRuleSet c in cats)
        {
            if (Vector3.Distance(c.transform.position, transform.position) < distance)
            {
                o.Add(c);
            }
        }

        return o;
    }

    Vector3 GetCenterOfCats(List<CatRuleSet> cats)
    {
        int num = 0;
        Vector3 center = new Vector3();

        foreach (CatRuleSet cat in cats)
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

    Vector3 GetAwayVectorOfCats(List<CatRuleSet> cats)
    {
        int num = 0;
        Vector3 away = new Vector3();

        foreach (CatRuleSet cat in cats)
        {
            away -= cat.transform.position - transform.position;
            num += 1;
        }

        if (num > 0)
        {
            away /= num;
        }

        return away;
    }

    Vector3 GetAlignmentDirection(List<CatRuleSet> cats)
    {
        int num = 0;
        Vector3 direction = new Vector3();

        foreach (CatRuleSet cat in cats)
        {
        Vector3 dir = cat.transform.rotation * Vector3.forward;
            direction += dir;
            num += 1;
        }

        if (num > 0)
        {
            direction /= num;
        }

        return direction;
    }
}
