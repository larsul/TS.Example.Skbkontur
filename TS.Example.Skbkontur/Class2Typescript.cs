using System.ComponentModel.DataAnnotations;

namespace TS.Example.Skbkontur
{
    public class Class2Typescript
    {
        public int Id { get; set; }
        [Display(Name = "Specify a name for your hub entity", Description = "This is just 4 fun and we are having soooo much fun :)")]
        [MaxLength(50, ErrorMessage = "Your 'Name' should be max 50 characters long.")]
        [MinLength(0, ErrorMessage = "Your 'Name' donst not have a to be filled.")]
        [Required(AllowEmptyStrings = true)]
        public string Name { get; set; }
    }
}
