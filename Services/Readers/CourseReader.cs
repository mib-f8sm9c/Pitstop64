using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.Common.DataElements;
using System.ComponentModel;
using Cereal64.Common.Rom;
using MK64Pitstop.Services.Hub;
using MK64Pitstop.Data.Courses;

namespace MK64Pitstop.Services.Readers
{
    public static class CourseReader
    {
        public static void ReadRom(BackgroundWorker worker, byte[] rawData, MarioKart64ReaderResults finalResults)
        {
            CourseReaderResults results = new CourseReaderResults();
            
            CourseDataReferenceBlock courseBlock;
            if (!RomProject.Instance.Files[0].HasElementExactlyAt(MarioKartRomInfo.CourseReferenceDataTableLocation))
            {
                ProgressService.SetMessage("Loading Course Resources");
                byte[] refBlock = new byte[0x13 * 0x30];
                Array.Copy(rawData, MarioKartRomInfo.CourseReferenceDataTableLocation, refBlock, 0, 0x13 * 0x30);

                courseBlock = new CourseDataReferenceBlock(MarioKartRomInfo.CourseReferenceDataTableLocation, refBlock);
                //RomProject.Instance.Files[0].AddElement(block);
                results.NewElements.Add(courseBlock);
            }
            else
            {
                courseBlock = (CourseDataReferenceBlock)RomProject.Instance.Files[0].GetElementAt(MarioKartRomInfo.CourseReferenceDataTableLocation);
            }

            results.CourseDataBlock = courseBlock;
            //MarioKart64ElementHub.Instance.CourseDataBlock = courseBlock;

            finalResults.CourseResults = results;
        }

        public static void ApplyResults(CourseReaderResults results)
        {
            foreach (N64DataElement element in results.NewElements)
                RomProject.Instance.Files[0].AddElement(element);

            MarioKart64ElementHub.Instance.CourseDataBlock = results.CourseDataBlock;
        }
    }

    public class CourseReaderResults
    {
        public List<N64DataElement> NewElements;
        public CourseDataReferenceBlock CourseDataBlock;

        public CourseReaderResults()
        {
            NewElements = new List<N64DataElement>();
        }
    }
}
