namespace BulletML.Enums
{
    // To make the conversion between enum and string easier
    // please keep the actual case, the one used in the XML file.

    /// <summary>
    /// Type of node.
    /// Used to parse the string in the "type" attribute.
    /// </summary>
    public enum NodeType
    {
        unknown,
        aim,
        absolute,
        relative,
        sequence
    }
}