using UnityEngine;

public class CircleItem : CollectibleItem
{
    protected override void OnCollect()
    {
        base.OnCollect();

        Destroy(gameObject);
    }
}
