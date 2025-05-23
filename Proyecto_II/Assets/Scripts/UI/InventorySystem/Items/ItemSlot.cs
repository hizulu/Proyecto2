#region Bibliotecas
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System;
#endregion

/* NOMBRE CLASE: Item Slot
 * AUTOR: Luc�a Garc�a L�pez
 * FECHA: 13/03/2025
 * DESCRIPCI�N: Script que se encarga de gestionar los slots de los �tems en el inventario.
 * VERSI�N: 1.0 SetWeapon, IsEmpty, UpdateQuantity, GetItemData, HasItem, SelectWeapon, OnPointerClick.
 * 1.1 Cambios en UpdateQuantity, + OnValidate.
 */

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    #region Variables
    [SerializeField] private Image itemIconImage;
    [SerializeField] private TMP_Text itemQuantityText;

    private ItemData itemData;
    [SerializeField] private int itemQuantity;

    public bool itemSelected = false;
    #endregion

    private void Start()
    {
        if (IsEmpty())
        {
            gameObject.SetActive(false);
        }
    }

    //Solo para pruebas
    // M�todo llamado cuando un valor cambia en el Inspector
    //public void OnValidate()
    //{
    //    if (itemData != null && InventoryManager.Instance.inventory.ContainsKey(itemData))
    //    {
    //        // Actualizar la cantidad en el diccionario cuando cambie el valor en el Inspector
    //        InventoryManager.Instance.inventory[itemData] = itemQuantity;

    //        // Aseg�rate de actualizar la visibilidad del slot
    //        InventoryManager.Instance.UpdateItemSlotVisibility(itemData);
    //    }
    //}

    // M�todo para asignar un �tem a un slot
    public void SetItem(ItemData newItemData, int quantity)
    {
        // Verificaci�n de seguridad
        if (itemIconImage == null || itemQuantityText == null)
        {
            Debug.LogError("Referencias faltantes en ItemSlot");
            return;
        }

        itemData = newItemData;
        itemQuantity = quantity;

        // Actualizar UI solo si tenemos item
        if (itemData != null)
        {
            itemIconImage.sprite = itemData.itemIcon;
            itemQuantityText.text = quantity.ToString();
            itemIconImage.enabled = true;
            itemQuantityText.enabled = quantity > 0;
            gameObject.SetActive(true);
        }
        else // Limpiar slot
        {
            itemIconImage.sprite = null;
            itemQuantityText.text = "";
            itemIconImage.enabled = false;
            itemQuantityText.enabled = false;
            gameObject.SetActive(false);
        }
    }

    public bool IsEmpty()
    {
        return itemData == null;
    }

    // M�todo para actualizar la cantidad de un �tem en un slot
    public void UpdateQuantity(int newQuantity)
    {
        itemQuantity = newQuantity;
        itemQuantityText.text = newQuantity.ToString();

        // Si la cantidad es 0 o menor, ocultamos el texto
        if (newQuantity <= 0)
        {            
            itemQuantityText.enabled = false;
            itemSelected = false;
        }
        else
        {
            itemQuantityText.enabled = true; // Se muestra el texto si la cantidad es mayor que 0
        }
    }

    // M�todo para obtener la informaci�n de un �tem
    public ItemData GetItemData()
    {
        return itemData;
    }

    // M�todo para comprobar si un slot tiene un �tem
    public bool HasItem()
    {
        return itemData != null;
    }

    // M�todo para seleccionar un �tem
    private void SelectItem()
    {
        itemSelected = !itemSelected;

        if (itemSelected && itemData != null)
        {
            ObjectsImageAndDescription.Instance?.ItemSetImageAndDescription(itemData);
        }
        else
        {
            ObjectsImageAndDescription.Instance?.ClearDisplay(); //?. sirve para evitar el error de referencia nula
        }
    }

    // M�todo para deseleccionar un �tem
    public void DeselectItem()
    {
        itemSelected = false;
        ObjectsImageAndDescription.Instance?.ClearDisplay(); // Limpiar la descripci�n
    }


    // M�todo para detectar el clic en un slot
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && !IsEmpty())
        {
            SelectItem();
        }
    }
}
