using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.Common.Utils.Encoding;
using Cereal64.Common.Rom;
using Cereal64.Common.DataElements.Encoding;

namespace MK64Pitstop.Data.Courses
{
    public class CourseData
    {
        //In the future, store outside of the MIOBlocks, then move into them when re
        public MIO0Block ItemsBlock { get; private set; }
        public MIO0Block VertexBlock { get; private set; }
        public DmaAddressBlock TextureReferences { get; private set; }
        public uint VertexBank { get; private set; }
        public uint Unknown1 { get; private set; }
        public DmaAddress PackedDL { get; private set; }
        public uint Seg7Length { get; private set; }
        public uint TableSeg { get; private set; }
        public uint Unknown2 { get; private set; }

        public string CourseName { get; set; }
        public bool IsOriginalCourse { get; private set; }

        public CourseData()
        {

        }

        public CourseDataReferenceEntry GetCourseDataReference()
        {
            throw new NotImplementedException();
            return null;
        }

    }
}
