using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WorkListAPI.Src.Utilities;

namespace WorkListAPI.Src.Models
{
    /// <summary>
    /// <para>summary: Class responsable to represent tb_users on the database. </para>
    /// <para>Created by: FelipeFlor</para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 06/02/2023</para>
    /// </summary>
    [Table("tb_users")]
    public class User
    {
        #region Attributes
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [Required]
        public UserType Type { get; set; }

        [JsonIgnore, InverseProperty("Creator")]
        public List<Work> MyWorks { get; set; }

        #endregion
    }
}
