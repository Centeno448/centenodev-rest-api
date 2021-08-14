using CentenoDev.API.Data;
using CentenoDev.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentenoDev.API.Services.Attachment
{
    public class AttachmentService : IAttachmentService
    {
        private readonly CentenoDevDBContext _db;

        public AttachmentService(CentenoDevDBContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<AttachmentEntity>> GetAllAttachments(Guid projectGuid)
        {
            return await _db.Attachment.Where(l => l.ProjectGuid == projectGuid).ToListAsync();
        }

        public async Task<AttachmentEntity> GetAttachmentByGuid(Guid projectGuid, Guid attachmentGuid)
        {
            return await _db.Attachment.Where(l => l.Guid == attachmentGuid && l.ProjectGuid == projectGuid).FirstOrDefaultAsync();
        }

        public async Task<bool> AttachmentExists(Guid projectGuid, Guid attachmentGuid)
        {
            var attachment = await _db.Attachment.FindAsync(attachmentGuid);

            return attachment != null && attachment.ProjectGuid == projectGuid;
        }
    }
}
