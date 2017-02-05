using BulletML.Enums;
using BulletML.Nodes;
using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;

namespace BulletML
{
    /// <summary>
    /// This is a complete document that describes a bullet pattern.
    /// </summary>
    public class BulletPattern
    {
        #region Members

        /// <summary>
        /// The root node of a tree structure that describes the bullet pattern.
        /// </summary>
        public BulletMLNode RootNode { get; private set; }

        /// <summary>
        /// Gets the filename.
        /// This property is only set by calling the parse method.
        /// </summary>
        /// <value>The filename.</value>
        public string Filename { get; private set; }

        /// <summary>
        /// The orientation of this bullet pattern: horizontal or veritcal
        /// This is read in from the xml.
        /// </summary>
        /// <value>The orientation.</value>
        public PatternType Orientation { get; private set; }

        #endregion Members

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="BulletML.BulletPattern"/> class.
        /// </summary>
        public BulletPattern()
        {
            RootNode = null;
        }

        /// <summary>
        /// Convert a string to a pattern type enum.
        /// </summary>
        /// <returns>The type to name.</returns>
        /// <param name="enumString">Enum string.</param>
        private static PatternType StringToPatternType(string enumString)
        {
            return (PatternType)Enum.Parse(typeof(PatternType), enumString);
        }

        /// <summary>
        /// Parses a BulletML document into this bullet pattern.
        /// </summary>
        /// <param name="xmlFilename">XML file name.</param>
        public void Parse(string xmlFilename)
        {
            var settings = new XmlReaderSettings
            {
                ValidationType = ValidationType.DTD,
                DtdProcessing = DtdProcessing.Parse,
                // Used to load the same DTD file, no matters
                // where is the BulletML file that we parse
                XmlResolver = new XmlDtdResolver("bulletml.dtd")
            };

            settings.ValidationEventHandler += PatternValidationEventHandler;

            using (var reader = XmlReader.Create(xmlFilename, settings))
            {
                try
                {
                    ParsePattern(reader, xmlFilename);
                }
                catch (Exception ex)
                {
                    throw new InvalidDataException("Error parsing \"" + xmlFilename + "\": " + ex.Message);
                }
            }

            // Grab that filename
            Filename = xmlFilename;

            // Validate that the bullet nodes are all valid
            try
            {
                RootNode.ValidateNode();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ParseStream(string filename, Stream fileStream)
        {
            var settings = new XmlReaderSettings
            {
                ValidationType = ValidationType.None,
                DtdProcessing = DtdProcessing.Ignore,
                // Used to load the same DTD file, no matters
                // where is the BulletML file that we parse
                XmlResolver = new XmlDtdResolver("bulletml.dtd")
            };

            settings.ValidationEventHandler += PatternValidationEventHandler;

            using (var reader = XmlReader.Create(fileStream, settings))
            {
                try
                {
                    ParsePattern(reader, filename);
                }
                catch (Exception ex)
                {
                    throw new InvalidDataException("Error parsing \"" + filename + "\": " + ex.Message);
                }
            }

            // Seek back to the beginning of the stream
            if (fileStream.CanSeek)
                fileStream.Seek(0, SeekOrigin.Begin);

            // Grab that filename
            Filename = filename;

            // Validate that the bullet nodes are all valid
            try
            {
                RootNode.ValidateNode();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ParsePattern(XmlReader reader, string filename)
        {
            // Open XML the file
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(reader);

            XmlNode rootXmlNode = xmlDoc.DocumentElement;

            // Make sure it's actually an XML node
            if (rootXmlNode != null && rootXmlNode.NodeType == XmlNodeType.Element)
            {
                // Eat up the name of that XML node
                var strElementName = rootXmlNode.Name;

                if (strElementName != NodeName.bulletml.ToString())
                {
                    // The first node HAS to be a bulletml node
                    throw new Exception("Error reading \"" + filename + "\": XML root node needs to be a <"
                        + NodeName.bulletml + "> tag, found a <" + strElementName + "> tag instead.");
                }

                // Find what kind of pattern this is: horizontal or vertical
                XmlNamedNodeMap mapAttributes = rootXmlNode.Attributes;

                if (mapAttributes != null)
                {
                    for (var i = 0; i < mapAttributes.Count; i++)
                    {
                        // Will only have the name attribute
                        var strName = mapAttributes.Item(i).Name;
                        var strValue = mapAttributes.Item(i).Value;

                        if (strName == AttributeName.type.ToString())
                        {
                            // If this is a top level node, "type" will be vertical or horizontal
                            Orientation = StringToPatternType(strValue);
                        }
                    }
                }

                // Create the root node of the bulletml tree
                RootNode = new BulletMLNode(NodeName.bulletml);

                // Read in the whole BulletML tree
                RootNode.Parse(rootXmlNode, null);
            }
        }

        /// <summary>
        /// Delegate method that gets called when a validation error occurs.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="args">Arguments.</param>
        private static void PatternValidationEventHandler(object sender, ValidationEventArgs args)
        {
            throw new XmlSchemaException("Error validating BulletML document: " + args.Message,
                                         args.Exception,
                                         args.Exception.LineNumber,
                                         args.Exception.LinePosition);
        }

#endregion Methods
    }
}