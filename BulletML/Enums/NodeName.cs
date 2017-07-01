namespace BulletML.Enums
{
    // To make the conversion between enum and string easier
    // please keep the actual case, the one used in the XML file.

    /// <summary>
    /// Each enum correspond to an available tag in the XML file.
    /// </summary>
    public enum NodeName
    {
        bullet,
        action,
        fire,
        changeDirection,
        changeSpeed,
        accel,
        wait,
        repeat,
        bulletRef,
        actionRef,
        fireRef,
        vanish,
        horizontal,
        vertical,
        term,
        times,
        direction,
        speed,
        param,
        bulletml,
        color,
        red,
        green,
        blue,
        alpha,
        opacity,
        changeColor,
        scale,
        changeScale,
        sprite
    }
}