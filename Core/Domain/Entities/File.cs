using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Common;

namespace Domain.Entities;

public class File : BaseEntity
{
    public string FileName { get; set; }

    public string Path { get; set; }

    public string StorageType { get; set; }
    
    [NotMapped ]
    public override DateTime UpdatedDate
    {
        get => base.UpdatedDate;
        set => base.UpdatedDate = value;
    }
}