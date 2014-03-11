using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;

namespace Drinks.Entities
{
    [Table("Log")]
    public class LogItem
    {
        public LogItem(string message)
        {
            Message = message;
        }

        [UsedImplicitly]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }
        [UsedImplicitly]
        public string Message { get; private set; }
    }
}
