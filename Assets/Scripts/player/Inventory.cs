using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public List<GarbageItem> PaperItems = new List<GarbageItem>();
    public List<GarbageItem> PlasticItems = new List<GarbageItem>();
    public List<GarbageItem> AluminiumItems = new List<GarbageItem>();

    public int totalGarbage => PaperItems.Count + PlasticItems.Count + AluminiumItems.Count;

    public Text inventoryText;

    public AudioClip dropItemSound;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    public bool AddGarbage(GarbageItem garbage)
    {
        switch (garbage.garbagetype)
        {
            case GarbageTypes.Plastic:
                if (PlasticItems.Count >= 3) return false;
                PlasticItems.Add(garbage);
                break;
            case GarbageTypes.Aluminium:
                if (AluminiumItems.Count >= 3) return false;
                AluminiumItems.Add(garbage);
                break;
            case GarbageTypes.Paper:
                if (PaperItems.Count >= 3) return false;
                PaperItems.Add(garbage);
                break;
        }

        return true;
    }

    public bool RemoveGarbage(GarbageTypes containerType)
    {
        GameObject player = GameObject.Find("Player");
        switch (containerType)
        {
            case GarbageTypes.Plastic:
                if (PlasticItems.Count <= 0) return false;
                PlasticItems = new List<GarbageItem>();
                break;
            case GarbageTypes.Aluminium:
                if (AluminiumItems.Count <= 0) return false;
                AluminiumItems = new List<GarbageItem>();
                break;
            case GarbageTypes.Paper:
                if (PaperItems.Count <= 0) return false;
                PaperItems = new List<GarbageItem>();
                break;
        }

        player.GetComponent<Player>().AddPoints(10 * PlasticItems.Count);
        player.GetComponent<GetDropGarbage>().container.GetComponent<Container>().garbageEffect.Play();
        PlaySound();
        return true;
    }

    public void PlaySound()
    {
        GameManager.instance.GetComponent<AudioSource>().clip = dropItemSound;
        GameManager.instance.GetComponent<AudioSource>().Play();
    }

    private void Update()
    {
        string output = "Paper:    " + PaperItems.Count + "/3\n\n" + "Plastic:    " + PlasticItems.Count + "/3\n\n" +
                        "Aluminium:    " + AluminiumItems.Count + "/3";
        inventoryText.text = output;
    }
}