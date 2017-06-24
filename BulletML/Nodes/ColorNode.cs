using BulletML.Enums;
using Microsoft.Xna.Framework;
using System.Xml;

namespace BulletML.Nodes
{
    public class ColorNode : BulletMLNode
    {
        private short _red = 255;
        private short _green = 255;
        private short _blue = 255;
        private short _alpha = 255;

        public Color Color;

        public ColorNode() : base(NodeName.color)
        {
        }

        public override void Parse(XmlNode bulletNodeElement, BulletMLNode parentNode)
        {
            base.Parse(bulletNodeElement, parentNode);

            XmlNamedNodeMap mapAttributes = bulletNodeElement.Attributes;

            if (mapAttributes != null)
            {
                for (var i = 0; i < mapAttributes.Count; i++)
                {
                    var strName = mapAttributes.Item(i).Name;
                    var strValue = mapAttributes.Item(i).Value;

                    // Get colors values
                    if (strName == AttributeName.red.ToString())
                        _red = short.Parse(strValue);
                    else if (strName == AttributeName.green.ToString())
                        _green = short.Parse(strValue);
                    else if (strName == AttributeName.blue.ToString())
                        _blue = short.Parse(strValue);
                    else if (strName == AttributeName.alpha.ToString())
                        _alpha = short.Parse(strValue);
                }
            }

            Color = new Color(_red, _green, _blue, 255) * (_alpha / 255f);
        }
    }
}
