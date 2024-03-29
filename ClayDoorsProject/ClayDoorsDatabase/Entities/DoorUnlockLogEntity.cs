﻿using ClayDoorsModel.Models;
using ClayDoorsModel.Models.Definitions;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClayDoorsDatabase.Entities
{
    [Table("door_unlock_log")]
    internal class DoorUnlockLogEntity
    {
        [Column("log_id")]
        public int? Id { get; set; }

        [Column("action_time")]
        public DateTime ActionTime { get; set; }

        [Column("action_result")]
        public string ActionResult { get; set; }

        public string? Username { get; set; }

        [Column("door_id")]
        public int? DoorId { get; set; }

        internal IDoorUnlockLog MapToModel()
        {
            return new DoorUnlockLog(
                this.Id,
                this.ActionTime,
                Enum.Parse<DoorUnlockResult>(this.ActionResult),
                this.DoorId,
                this.Username);
        }
    }
}
