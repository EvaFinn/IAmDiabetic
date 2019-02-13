using System;
using Realms;

namespace ImDiabetic.Models.Logbook
{
    public interface ILogs
    {
        string Id { get; set; }
        string UserId { get; set; }
        DateTimeOffset LogDate { get; set; }
    }
}
