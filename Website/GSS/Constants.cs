using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GSS
{
    public class Constants
    {
        public class ItemsIds
        {
            public const string GSS = "{AE3DB2A2-EF0C-4094-AE70-4190DA293986}";
            public class SafeLevel
            {
                public const string Off = "{AC91D664-5F56-4B8B-B0DA-216733CFB33E}";
                public const string Medium = "{DEB87E22-31E7-45D7-B722-F53928A03FC0}";
                public const string High = "{F3BD9313-57A4-4284-96BB-F2B7061E424A}";
            }
        }

        public class TemplateIds
        {
            public const string GSSModule = "{A9518C35-0C78-40C9-83B0-F10FCBC0EA5E}";
            public const string GSSSite = "{D88AD169-B3E6-4A5A-8C94-063EF597908C}";
        }

        public class FieldIds
        {
            public const string SiteName = "{9A06769A-6BEA-4A7A-A6D8-468F39A0AE27}";
            public const string WebsiteProtocolAndHostname = "{2532CFFA-C39A-4F52-B929-ACDCF4775AB9}";
            public const string GoogleSearchIdCx = "{1B80181E-6521-412A-962D-88CE790D937A}";
            public const string GoogleSearchApiKey = "{61FA86BC-D759-4C63-956D-9D24C1A4CAE5}";
            public const string GoogleSearchSafeLevel = "{46DA0F81-4102-4AC7-82DD-AF3C09F62D63}";
        }

        public class Google
        {
            public const string CORRECTED_LABEL = "<b>Corrected</b> ";
        }
    }
}