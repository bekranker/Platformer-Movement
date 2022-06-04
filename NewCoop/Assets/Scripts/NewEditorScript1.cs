using UnityEngine;

public class Test : Librariy
{
    private void Start()
    {
        var a_ = Wait(() => Debug.Log("sa"), 2000);
    }
}