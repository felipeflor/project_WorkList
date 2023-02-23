using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkListAPI.Src.Models
{
    /// <summary>
    /// <para>summary: Class responsable to represent tb_tasks on the database. </para>
    /// <para>Created by: FelipeFlor</para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 06/02/2023</para>
    /// </summary>

    [Table("tb_works")]
    public class Work
    {
        #region Attributes
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        [ForeignKey("fk_users")]
        public User Creator { get; set; }

        #endregion

    }
}
