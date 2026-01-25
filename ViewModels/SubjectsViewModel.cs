using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;
using PoliNote.DTOs.Subjects.Responses;
using PoliNote.Services;

namespace PoliNote.ViewModels;

public class SubjectsViewModel : BaseViewModel
{
    private readonly SubjectService _subjectService = new();

    public ObservableCollection<SubjectGroupDto> EnrolledSubjects { get; } = new();
    public ObservableCollection<SubjectGroupDto> AvailableSubjects { get; } = new();

    public Command LoadCommand { get; }
    public Command<SubjectGroupDto> OpenDetailsCommand { get; }

    public SubjectsViewModel()
    {
        LoadCommand = new Command(async () => await LoadAsync());

        OpenDetailsCommand = new Command<SubjectGroupDto>(async group =>
        {
            if (group == null) return;

            await Shell.Current.GoToAsync(
                $"subject-details?id={group.Id}"
            );
        });
    }

    private async Task LoadAsync()
    {
        if (IsBusy) return;
        IsBusy = true;

        try
        {
            EnrolledSubjects.Clear();
            AvailableSubjects.Clear();

            var groups = await _subjectService.GetSubjectGroupsAsync();

            foreach (var group in groups)
            {
                if (group.IsEnrolled)
                    EnrolledSubjects.Add(group);
                else
                    AvailableSubjects.Add(group);
            }
        }
        finally
        {
            IsBusy = false;
        }
    }
}
