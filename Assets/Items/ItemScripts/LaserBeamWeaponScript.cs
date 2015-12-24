using UnityEngine;
using System.Collections;

public class LaserBeamWeaponScript : Weapon
{

    public float maxDist = 40f;    
    public float damageTickRate = .2f;
    [SerializeField] LayerMask layers;
    // just set the transform of one item...
    [ SerializeField] GameObject laserGameObject;
    LaserBeamDamageScript laserBeam;
    // Use this for initialization
    void Start()
    {
        laserBeam = laserGameObject.GetComponent<LaserBeamDamageScript>();
        itemType = ItemType.WEAPON;
    }



    public override void Init(SaveGameInfo sgi, bool playerOwned)
    {
        base.Init(sgi, playerOwned);

        if (playerOwned)
        {
            layers = LayerMask.GetMask(new string[] { "enemy", "Neutral" });
        }
        else
        {
            layers = LayerMask.GetMask(new string[] { "Neutral", "player" });
        }
    }


    protected override bool CanFire(float energy)
    {
       if(energy >= energyCost * Time.fixedDeltaTime)
        {
            return true;
        }
        return false;
    }

    public override bool TryToFire(ref float energy)
    {
        if (CanFire(energy))
        {
            laserGameObject.SetActive(true);
            createLaser();
            energy -= energyCost * Time.fixedDeltaTime;
            return true;
        }
        laserGameObject.SetActive(false);
        return false;
    }
    public override void MouseUp()
    {
        laserGameObject.SetActive(false);
    }

    void createLaser()
    {

        Vector3 laserPos = laserGameObject.transform.localPosition;
        Vector3 scale = laserGameObject.transform.localScale;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up,maxDist,layers);
        if (hit.collider != null)
        {                       
            laserPos.y = hit.distance / 2;
            scale.y = hit.distance * 4;
            laserBeam.SetTarget(hit.collider.gameObject);
        }else
        {            
            laserPos.y  = maxDist/2 ;
            scale.y = maxDist *4;
            laserBeam.SetTarget(null);
        }
            laserGameObject.transform.localPosition = laserPos;
            laserGameObject.transform.localScale = scale;
    }


}
