using UnityEngine;

public class Sword : MonoBehaviour
{
    public SwordItem item;
    private bool isCooldown = false;
    private bool isAtak = false;
    private float NextAttack = 0;
    private float NextAnim = 0;
    private double Cooldown = 0.4;
    private double AnimationT = 0.002;
    public double realDamage { get; private set; }

    public void SetRealDamage(double damage)
    {
        realDamage = damage;
    }
    public void ChangetWearableImage()
    {
        SpriteRenderer render = gameObject.GetComponent<SpriteRenderer>();
        if (render.sprite == null)
            render.sprite = item.WearableIcon;
        else if (render.sprite == item.WearableIcon)
            render.sprite = item.WearableIcon;
        else
            render.sprite = null;
    }
    public void Animation()
    {
        SpriteRenderer render = gameObject.GetComponent<SpriteRenderer>();
        if (!isAtak)
            render.sprite = item.WearableIcon;
        else
            render.sprite = item.AnimationIcon;
    }
    public void EquipSword(ItemV2 item)
    {
        if (item is SwordItem)
        {
            this.item = (SwordItem)item;
            ChangetWearableImage();
        }
    }
    public void UnequipSword()
    {
        ChangetWearableImage();
        this.item = null;
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("ItemDestroy") && !isCooldown)
        {
            if (Input.GetMouseButton(0))
            {
                isCooldown = true;
                isAtak = true;
                Destroy(col.gameObject);
                col.gameObject.GetComponent<Destroyinfo>().DestroyObject();
            }
        }
        else if (col.gameObject.CompareTag("Enemy"))
        {
            if (Input.GetMouseButton(0) && !isCooldown)
            {
                isCooldown = true;
                isAtak = true;
                col.gameObject.GetComponentInParent<EnemyController>().TakeDamage((float)realDamage);
                col.gameObject.GetComponentInParent<EnemyController>().EF.GetHit = true;
            }
        }

    }
    private void Update()
    {
        if (NextAttack < Time.time && isCooldown)
        {
            NextAttack = Time.time + (float)Cooldown;
            isCooldown = false;

        }
        if (!isCooldown)
        {
            NextAttack = Time.time + (float)Cooldown;
        }
        if (NextAnim < Time.time && isAtak)
        {
            NextAnim = Time.time + (float)AnimationT;
            isAtak = false;

        }
        if (!isAtak)
        {
            NextAnim = Time.time + (float)Cooldown;
        }
        if (Input.GetMouseButton(0) && !isCooldown)
        {
            isAtak = true;
        }
        Animation();
    }
}
