using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using TMPro;
using UnityEngine.UI;

public class WeaponStats : MonoBehaviour
{
    public Weapon weapon;
    public Text AttackValue;
    public Text DurabilityValue;

    private BoxCollider[] weaponColliders;
    private MeshRenderer _meshRenderer;
    private bool _isCollected = false;
    private Collider _player;
    public Transform transformOffset;

    private void Start()
    {
        weaponColliders = GetComponents<BoxCollider>();
        weaponColliders[1].enabled = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player" && !_isCollected)
        {
            _player = other;
            weaponColliders[0].enabled = false;
            _meshRenderer.enabled = false;
            _isCollected = true;
            /*
             * add item to inventory
             */
            /*AttackValue.text = "Attack: " + weapon.attack.ToString();
            DurabilityValue.text = "Durability: " + weapon.durability.ToString();*/
        }
    }

    public void EquipWeapon()
    {
        weaponColliders[1].enabled = true;
        _meshRenderer.enabled = true;
        UpdateTransform();
    }

    private void UpdateTransform()
    {
        transform.position = _player.gameObject.GetComponent<PlayerScript>().weaponTransform.position +
                             transformOffset.position;
        transform.rotation = _player.gameObject.GetComponent<PlayerScript>().weaponTransform.rotation *
                             transformOffset.rotation;
    }
}
