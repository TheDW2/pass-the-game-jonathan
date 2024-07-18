using UnityEngine;

public class PickupScript : MonoBehaviour
{
    // Modified from PickUpScript by JonDevTutorials
    // https://github.com/JonDevTutorial/PickUpTutorial/blob/main/PickUpScript.cs

    [SerializeField]
    private GameObject player; // Player object

    [SerializeField]
    private Transform holdPos; // Position at which object is held

    [SerializeField]
    private float throwForce = 700.0f; //force at which the object is thrown at

    [SerializeField]
    private float pickupDistance = 3.0f;

    [SerializeField]
    private AudioSource SoundPlayer;

    [SerializeField]
    private AudioClip throwSound;

    private GameObject heldObj;
    private Rigidbody heldObjRB;
    private Outline heldObjOutline;

    private HoldableObject targetHoldableObject;

    private void Update()
    {
        // This is terribly inefficient, feel free to change it :)
        SelectTarget();

        if (Input.GetKeyDown(KeyCode.E)) //change E to whichever key you want to press to pick up
        {
            if (heldObj == null) //if currently not holding anything
            {
                if (targetHoldableObject != null)
                {
                    PickUpObject(targetHoldableObject.gameObject);
                    targetHoldableObject.PickUp();
                }
            }
            else
            {
                DropObject();
            }
        }

        if (heldObj != null) //if player is holding object
        {
            MoveObject(); //keep object position at holdPos
            if (Input.GetKeyDown(KeyCode
                    .Mouse0)) //Mouse 0 (leftclick) is used to throw, change this if you want another button to be used)
            {
                ThrowObject();
            }
        }
    }

    private void SelectTarget()
    {
        targetHoldableObject = null;
        var closestDistance = float.MaxValue;
        HoldableObject closestObject = null;
        foreach (HoldableObject obj in HoldableObject.Objects)
        {
            float d = (player.transform.position - obj.transform.position).sqrMagnitude;
            obj.GetComponent<Outline>().enabled = false;
            if (d < pickupDistance * pickupDistance)
            {
                if (d < closestDistance)
                {
                    closestObject = obj;
                    closestDistance = d;
                }
            }
        }

        if (closestObject != null)
        {
            closestObject.GetComponent<Outline>().enabled = true;
            targetHoldableObject = closestObject;
        }
    }

    private void PickUpObject(GameObject pickup)
    {
        if (pickup.GetComponent<Rigidbody>())
        {
            heldObj = pickup;
            heldObjRB = pickup.GetComponent<Rigidbody>();
            heldObjOutline = pickup.GetComponent<Outline>();
            heldObjOutline.enabled = false;
            heldObjRB.isKinematic = true;
            heldObjRB.transform.parent = holdPos.transform; //parent object to holdposition
            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
        }
    }

    private void DropObject()
    {
        //re-enable collision with player
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObjRB.isKinematic = false;
        heldObj.transform.parent = null; //unparent object
        heldObj = null; //undefine game object
    }

    private void MoveObject()
    {
        //keep object position the same as the holdPosition position
        heldObj.transform.position = holdPos.transform.position;
    }

    private void ThrowObject()
    {
        //same as drop function, but add force to object before undefining it
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObjRB.isKinematic = false;
        heldObj.transform.parent = null;
        heldObjRB.AddForce(holdPos.transform.forward * throwForce);
        heldObj = null;

        float pitchMod = Random.Range(-0.25f, 0.25f);
        SoundPlayer.pitch = 0.5f + pitchMod; // slightly lower than norml pitch
        SoundPlayer.PlayOneShot(throwSound, 0.3F);
    }
}