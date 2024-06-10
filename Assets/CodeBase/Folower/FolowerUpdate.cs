using UnityEngine;

public class FolowerUpdate : Folower
{
    private void Update()
    {
        Move(Time.deltaTime);
    }
}
