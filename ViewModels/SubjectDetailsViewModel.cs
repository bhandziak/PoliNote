using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;
using PoliNote.DTOs.Subjects.Responses;
using PoliNote.Services;

namespace PoliNote.ViewModels;

[QueryProperty(nameof(SubjectId), "id")]
public class SubjectDetailsViewModel : BaseViewModel
{
    private readonly SubjectService _subjectService = new();
    private readonly EnrollmentService _enrollmentService = new();

    private Guid _subjectId;
    public Guid SubjectId
    {
        get => _subjectId;
        set
        {
            _subjectId = value;
            _ = LoadAsync();
        }
    }

    public SubjectWithGroupsDto Subject { get; private set; }

    public ObservableCollection<SubjectGroupDto> Groups { get; } = new();

    public Command<SubjectGroupDto> EnrollCommand { get; }
    public Command<SubjectGroupDto> UnEnrollCommand { get; }

    public SubjectDetailsViewModel()
    {
        EnrollCommand = new Command<SubjectGroupDto>(async g => await EnrollAsync(g));
        UnEnrollCommand = new Command<SubjectGroupDto>(async g => await UnEnrollAsync(g));
    }

    private async Task LoadAsync()
    {
        if (IsBusy) return;
        IsBusy = true;

        try
        {
            Groups.Clear();

            Subject = await _subjectService.GetSubjectByIdAsync(SubjectId);

            foreach (var group in Subject.Groups)
                Groups.Add(group);

            OnPropertyChanged(nameof(Subject));
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async Task EnrollAsync(SubjectGroupDto group)
    {
        if (group == null || group.IsEnrolled) return;

        await _enrollmentService.EnrollAsync(group.Id);
        await LoadAsync();
    }

    private async Task UnEnrollAsync(SubjectGroupDto group)
    {
        if (group == null || !group.IsEnrolled) return;

        await _enrollmentService.UnEnrollAsync(group.Id);
        await LoadAsync();
    }
}
