
    using System;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Text;

    public static class ResourceLoader
    {
        /// <summary>
        /// Gets the resource embedded in an assembly as a string, using Default encoding.
        /// </summary>
        /// <param name="resourceName">The full name of the resource.</param>
        /// <param name="assemblyWithResource">The assembly in which the resource is expected to be found.</param>
        /// <returns>The resource represented as a string.</returns>
        public static string LoadResourceAsString(string resourceName, Assembly assemblyWithResource)
        {
            return LoadResourceAsString(resourceName, assemblyWithResource, Encoding.Default);
        }

        /// <summary>
        /// Gets the resource embedded in an assembly as a string.
        /// </summary>
        /// <param name="resourceName">The full name of the resource.</param>
        /// <param name="assemblyWithResource">The assembly in which the resource is expected to be found.</param>
        /// <param name="encoding">The encoding applied on the stream.</param>
        /// <returns>The resource represented as a string.</returns>
        public static string LoadResourceAsString(string resourceName, Assembly assemblyWithResource, Encoding encoding)
        {
            Stream stream = GetStream(resourceName, assemblyWithResource);

            var sr = new StreamReader(stream, encoding);
            return sr.ReadToEnd();
        }

        /// <summary>
        /// RGets the resource embedded in an assembly as a Stream.
        /// </summary>
        /// <param name="resourceName">The full name of the resource.</param>
        /// <param name="assemblyWithResource">The assembly in which the resource is expected to be found.</param>
        /// <returns>The resource represented as a Stream.</returns>
        public static Stream LoadResourceAsStream(string resourceName, Assembly assemblyWithResource)
        {
            return GetStream(resourceName, assemblyWithResource);
        }

        private static Stream GetStream(string resourceName, Assembly assemblyWithResource)
        {
            if (resourceName == null)
            {
                throw new ArgumentNullException("resourceName");
            }

            if (assemblyWithResource == null)
            {
                throw new ArgumentNullException("assemblyWithResource");
            }

            Stream stream =
                assemblyWithResource.GetManifestResourceStream(resourceName);

            if (stream == null)
            {
                throw new ArgumentException(
                    String.Format(
                        CultureInfo.InvariantCulture,
                        "Unable to find assembly resource {0}",
                        resourceName),
                    "resourceName");
            }

            return stream;
        }
    }
    
  /**
  USAGE:
   var listOfIgnoredDlls = ResourceLoader.LoadResourceAsString("TestDIPS.Core.Integration.Config.IgnoreAutoConfigCheck.txt", Assembly.GetExecutingAssembly());
  **/
