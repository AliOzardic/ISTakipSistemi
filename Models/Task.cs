﻿namespace IsTakipSistemi.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? DueDate { get; set; }
        public TaskStatus Status { get; set; }
        public int ProjectId { get; set; }
        public Project? Project { get; set; }
        public string? AssignedUserId { get; set; }
        public ApplicationUser? AssignedUser { get; set; }
        public Task()
        {
                
        }
    }

    public enum TaskStatus
    {
        ToDo,
        InProgress,
        Completed
    }

}
