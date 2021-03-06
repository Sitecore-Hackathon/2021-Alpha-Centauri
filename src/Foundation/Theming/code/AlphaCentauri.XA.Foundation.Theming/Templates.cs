using Sitecore.Data;

namespace AlphaCentauri.XA.Foundation.Theming
{
    /// <summary>
    /// Class to manage Guids as constants and made them available/reusable from the project.
    /// </summary>
    public class Templates
    {
        public struct Design
        {
            public static ID Presentation { get; } = new ID("{26BF5BE6-815D-4078-98BF-AB66B82D2171}");

            public static ID PageDesign { get; } = new ID("{3A68A4E5-22A8-42C8-8258-CBDE5F1A8A6A}");

            public struct Fields
            {
                public static ID PageDesignField { get; } = new ID("{24171BF1-C0E1-480E-BE76-4C0A1876F916}");

                public static ID TemplatesMapping { get; } = new ID("{BA1F60D6-3DEB-40CC-BB61-EEC772279EE1}");
            }
        }

        public struct Theme
        {
            public struct Fields
            {
                public static ID BaseLayout { get; } = new ID("{E5D4967C-3C26-464A-A044-C83B5024B2E9}");

                public static ID OverrideBaseLayout { get; } = new ID("{7CB38905-732D-4F3E-BE6B-5B41CD169270}");
            }
        }

    }
}