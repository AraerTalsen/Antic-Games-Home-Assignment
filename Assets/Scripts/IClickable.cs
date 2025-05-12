using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IClickable
{
    public abstract void OnHover();
    public abstract void OnHoverExit();

    public abstract void OnClick();
}
