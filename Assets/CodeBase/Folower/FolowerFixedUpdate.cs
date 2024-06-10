using UnityEngine;

public class FolowerFixedUpdate : Folower
{
    private void FixedUpdate()
    {
        Move(Time.fixedDeltaTime);
    }
}
