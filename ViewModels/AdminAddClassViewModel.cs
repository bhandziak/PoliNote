using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PoliNote.DTOs.Subjects.Requests;
using PoliNote.Services;

namespace PoliNote.ViewModels;

public class AdminAddClassViewModel : BaseViewModel
{
    private readonly SubjectService _subjectService = new();

    // Subject
    public string SubjectName { get; set; } = "";
    public int Etcs { get; set; }
    public string LecturerName { get; set; } = "";
    public string SyllabusUrl { get; set; } = "";

    // Group
    public string GroupName { get; set; } = "";
    public DayOfWeek DayOfWeek { get; set; } = DayOfWeek.Monday;
    public TimeOnly StartTime { get; set; } = new(8, 0);

    public Command SaveCommand { get; }
    public Command CancelCommand { get; }

    public AdminAddClassViewModel()
    {
        SaveCommand = new Command(async () => await SaveAsync());
        CancelCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
    }

    private async Task SaveAsync()
    {
        // 1️⃣ Create subject
        var subjectId = await _subjectService.CreateSubjectAsync(
            new SubjectRequestDto
            {
                Name = SubjectName,
                Etcs = Etcs,
                LecturerName = LecturerName,
                SyllabusUrl = SyllabusUrl
            });

        // 2️⃣ Create group
        await _subjectService.CreateSubjectGroupAsync(
            subjectId,
            new SubjectGroupRequestDto
            {
                GroupName = GroupName,
                DayOfWeek = DayOfWeek,
                StartTime = StartTime
            });

        await Shell.Current.GoToAsync("..");
    }
}
