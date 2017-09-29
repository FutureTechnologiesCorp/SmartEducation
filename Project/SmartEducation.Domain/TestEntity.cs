using System;
using System.ComponentModel.DataAnnotations;

namespace SmartEducation.Domain
{
    public class TestEntity
    {
        [Key]
        ///public Guid GuidId { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
