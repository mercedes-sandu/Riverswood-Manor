public static class GameEvent
{
    /// <summary>
    /// Handles the texture of the mouse cursor.
    /// </summary>
    public delegate void CursorHandler(bool interacting);

    /// <summary>
    /// Handles whether the painting has been clicked
    /// </summary>
    public delegate void PaintingHandler();

    /// <summary>
    /// Detects when the mouse cursor should be changed.
    /// </summary>
    public static event CursorHandler OnCursorChange;

    /// <summary>
    /// Detects when the painting has been clicked
    /// </summary>
    public static event PaintingHandler OnPaintingClick;

    /// <summary>
    /// Changes the mouse cursor to the specified texture.
    /// </summary>
    /// <param name="interacting">True if we swap to the interacting cursor, false otherwise.</param>
    public static void ChangeCursor(bool interacting) => OnCursorChange?.Invoke(interacting);

    /// <summary>
    /// Activates the painting portal to the next scene.
    /// </summary>
    public static void ActivatePaintingPortal() => OnPaintingClick?.Invoke();
}