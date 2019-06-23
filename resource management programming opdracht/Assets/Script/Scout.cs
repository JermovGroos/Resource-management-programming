using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Scout : MonoBehaviour {
    public enum Actions { Idle, Walk, Action}
    public Actions action;
    public NavMeshAgent agent;
    public Transform target;
    public Transform home;
    public int stoneAmount;
    public int woodAmount;
    public Text stoneText;
    public Text woodText;
	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
    }    // Update is called once per frame
    void Update()
    {
        stoneText.text = stoneAmount.ToString();
        woodText.text = woodAmount.ToString();
        //hier zorgt hij ervoor dat de int hetzelfde word als de text zodat hij de ingame ui kan veranderen
    }
    public void Exploring()
    {
        agent.SetDestination(target.position);
        action = Actions.Walk;
        // hier krijgt de player een position waar hij heen moet lopen en als hij die heeft veranderd de enum naar Walk
    }
    public void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "stone"|| c.gameObject.tag == "wood")
        {
            Destroy(c);
        }
        //hij kijkt hier of hij collision heeft met iets wat stone of wood heet en dan gaat hij daaruit verder naar de Destroy void
    }
    public void Destroy(Collision c)
    {
        if (c.gameObject.tag == "stone")
        {
            stoneAmount += 1;
        }
        else if (c.gameObject.tag == "wood")
        {
            woodAmount += 1;
        }
        StartCoroutine(WaitAttack());
        Destroy(c.gameObject);
        agent.SetDestination(home.position);
        StartCoroutine(IdleCheck());
        // Binnen de collision checkt hij of het object van stone of van wood is gemaakt en als hij dat heeft gecheckt, destroyed hij het object en doet hij de score van die type omhoog
    }
    IEnumerator WaitAttack()
    {
        action = Actions.Action;
        yield return new WaitForSeconds(3);
        action = Actions.Walk;
        //Hij zorgt er hier voor dat hij vanaf Action (wanneer hij de base attacked) naar Walk gaat.
    }
    IEnumerator IdleCheck()
    {
        while (Vector3.Distance(transform.position, home.transform.position) >= 5)
        {
            yield return null;
        }
        action = Actions.Idle;
        // Hij checkt hier of hij in de idle state zit door de positie van home te pakken en die te vergelijken met de player.
    }
}
