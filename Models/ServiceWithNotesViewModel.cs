using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bikevision.Models
{
    public class ServiceWithNotesViewModel
    {
        public Service serviceOrder;
        public List<NoteToService> notesToOrder;
    }
}