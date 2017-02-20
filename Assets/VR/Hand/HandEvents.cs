using System.Collections;

public static class HandEvents
{
	public static System.Action<HandController.types> Idle = delegate { };
	public static System.Action<HandController.types> Grab = delegate { };
	public static System.Action<HandController.types> Pointer = delegate { };
}

