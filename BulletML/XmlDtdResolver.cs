using System;
using System.IO;
using System.Reflection;
using System.Xml;

namespace BulletML
{
    /// <summary>
    /// This class is used to load the DTD file copied from this library,
    /// no matter the BulletML file's location during parsing.
    /// </summary>
    internal class XmlDtdResolver : XmlUrlResolver
    {
        private readonly string _dtdName;

        public XmlDtdResolver(string dtdName)
        {
            _dtdName = dtdName;
        }

        public override Uri ResolveUri(Uri baseUri, string relativeUri)
        {
            if (relativeUri.Equals(_dtdName))
            {
                var path = new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase);
                var projectPath = Path.GetDirectoryName(path.LocalPath);

                if (projectPath != null)
                    return new Uri(Path.Combine(projectPath, relativeUri));
            }

            return base.ResolveUri(baseUri, relativeUri);
        }
    }
}