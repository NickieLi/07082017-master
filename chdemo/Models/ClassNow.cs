using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace chdemo.Models
{
    [MetadataType(typeof(ClassNowMetadata))]
    public partial class ClassNow
    {
        private class ClassNowMetadata
        {
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
            public DateTime Ondate { get; set; }
        }
    }
}