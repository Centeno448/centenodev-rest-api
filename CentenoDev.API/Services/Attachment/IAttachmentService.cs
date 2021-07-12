using CentenoDev.API.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CentenoDev.API.Services.Attachment
{
    public interface IAttachmentService
    {
        Task<IEnumerable<AttachmentEntity>> GetAllAttachments(Guid projectGuid);
        Task<AttachmentEntity> GetAttachmentByGuid(Guid projectGuid, Guid attachmentGuid);

        Task<bool> AttachmentExists(Guid projectGuid, Guid attachmentGuid);
    }
}