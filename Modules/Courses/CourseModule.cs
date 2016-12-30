using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pitstop64.Modules.Courses
{
    public class CourseModule : IModule
    {
        //In the future, we'll need to keep track of all the course data,
        // but for now a viewer will suffice

        public void UpdateRomData()
        {
            _control.UpdateControl();
        }

        public System.Windows.Forms.Control Control
        {
            get
            {
                if (_control == null)
                {
                    _control = new CourseControl();
                }
                return _control;
            }
        }
        private CourseControl _control;
    }
}
