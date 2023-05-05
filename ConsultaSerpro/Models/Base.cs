using System.ComponentModel.DataAnnotations.Schema;

namespace ConsultaSerpro.Models
{
    public abstract class Base
    {
        [Column("id")]
        public int Id { get; set; }
        
        [Column("data_lancamento")]
        public DateTime DataLancamento { get; set; }

        public Base()
        {
            DataLancamento = (DateTime.Now - TimeSpan.FromDays(1)).ToUniversalTime();
        }
    }
}
