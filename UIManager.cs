using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject toggleText;
    public GameObject forgettingSomeoneText;
    public GameObject discoveryText;
    public GameObject acknowledgeText;
    public GameObject comeAlongText;
    public GameObject shadowSurpriseText;
    public GameObject aquariumReminderText;
    public GameObject shadowAquariumReply;
    public GameObject aquariumConclusionText;
    public GameObject jellyDangerText;
    public GameObject shadowIdeaReply;
    public GameObject proIdeaReply;
    public GameObject activeFloatText;

    GameObject shadow;
    GameObject protagonist;

    public float secondsToWait;

    private void Start()
    {
        shadow = GameObject.Find("Shadow");
        protagonist = GameObject.Find("Protagonist");
    }

    public IEnumerator ManageToggleText()
    {
        toggleText.GetComponent<Text>().enabled = true;
        yield return new WaitForSeconds(secondsToWait);
        toggleText.GetComponent<Text>().enabled = false;
    }

    public IEnumerator ManageForgettingSomeoneText()
    {
        forgettingSomeoneText.GetComponent<Text>().enabled = true;
        yield return new WaitForSeconds(8);
        forgettingSomeoneText.GetComponent<Text>().enabled = false;
    }

    public IEnumerator ManageDiscoveryText()
    {
        yield return new WaitForSeconds(1.85f);
        discoveryText.GetComponent<Text>().enabled = true;
        DisableCharacterMovement();
        yield return new WaitForSeconds(secondsToWait);
        discoveryText.GetComponent<Text>().enabled = false;
        EnableCharacterMovement();
    }

    public IEnumerator ManageShadowMeetText()
    {
        acknowledgeText.GetComponent<Text>().enabled = true;
        DisableCharacterMovement();
        yield return new WaitForSeconds(2f);
        acknowledgeText.GetComponent<Text>().enabled = false;
        shadowSurpriseText.GetComponent<Text>().enabled = true;
        yield return new WaitForSeconds(2f);
        shadowSurpriseText.GetComponent<Text>().enabled = false;
        yield return new WaitForSeconds(1.5f);
        comeAlongText.GetComponent<Text>().enabled = true;
        yield return new WaitForSeconds(2f);
        comeAlongText.GetComponent<Text>().enabled = false;
        EnableCharacterMovement();
    }

    public IEnumerator ManageAquariumText()
    {
        aquariumReminderText.GetComponent<Text>().enabled = true;
        DisableCharacterMovement();
        yield return new WaitForSeconds(3f);
        aquariumReminderText.GetComponent<Text>().enabled = false;
        yield return new WaitForSeconds(2f);
        shadowAquariumReply.GetComponent<Text>().enabled = true;
        yield return new WaitForSeconds(2f);
        shadowAquariumReply.GetComponent<Text>().enabled = false;
        yield return new WaitForSeconds(2f);
        aquariumConclusionText.GetComponent<Text>().enabled = true;
        yield return new WaitForSeconds(3f);
        aquariumConclusionText.GetComponent<Text>().enabled = false;
        shadow.GetComponent<ShadowMover>().canFloat = true;
        EnableCharacterMovement();
    }

    public IEnumerator ManageFloatDangerSequence()
    {
        jellyDangerText.GetComponent<Text>().enabled = true;
        DisableCharacterMovement();
        yield return new WaitForSeconds(3f);
        jellyDangerText.GetComponent<Text>().enabled = false;
        yield return new WaitForSeconds(1.75f);
        shadowIdeaReply.GetComponent<Text>().enabled = true;
        yield return new WaitForSeconds(2f);
        shadowIdeaReply.GetComponent<Text>().enabled = false;
        yield return new WaitForSeconds(2f);
        proIdeaReply.GetComponent<Text>().enabled = true;
        yield return new WaitForSeconds(2f);
        proIdeaReply.GetComponent<Text>().enabled = false;
        yield return new WaitForSeconds(2f);
        activeFloatText.GetComponent<Text>().enabled = true;
        yield return new WaitForSeconds(2f);
        activeFloatText.GetComponent<Text>().enabled = false;
        EnableCharacterMovement();
        shadow.GetComponent<ShadowMover>().canFloat = true;
    }

    public void EnableCharacterMovement()
    {
        shadow.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        protagonist.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        shadow.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        protagonist.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public void DisableCharacterMovement()
    {
        shadow.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
        protagonist.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
    }
}


