using System;
using System.Collections.Generic;
using System.Text;

namespace mav_incident_processor
{
    class IncidentData
    {

        public string Title = null;
        public string Timestamps = null;
        public string Content = null;
        public int? CreateTimestamp = null;
        public int? UpdateTimestamp = null;

        public string Hash()
        {
            return string.Format("title={0};description={1};creation_timestamp={2};update_timestamp={3}",
                Title,
                Content,
                CreateTimestamp,
                UpdateTimestamp).HashMD5();
        }

        public bool SomethingNull()
        {
            return ((Title == null) || (Content == null) || (CreateTimestamp == null) || (UpdateTimestamp == null));
        }

    }
}
