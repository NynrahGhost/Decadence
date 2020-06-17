using System;
using System.Collections.Generic;
using System.Text;

public interface IEvent
{
    public bool IsActive();

    public void Process(float delta);
}